using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MeterReaderWeb.Data;
using MeterReaderWeb.Data.Entities;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using MeterReaderLib;
using MeterReaderLib.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace MeterReaderWeb.Services
{
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MeterService : MeterReadingService.MeterReadingServiceBase
    {
        private readonly ILogger<MeterService> _logger;
        private readonly IReadingRepository repository;
        private readonly JwtTokenValidationService _tokenValidationService;

        public MeterService(ILogger<MeterService> logger, IReadingRepository repository, JwtTokenValidationService tokenValidationService)
        {
            this._logger = logger;
            this.repository = repository;
            _tokenValidationService = tokenValidationService;
        }


        [AllowAnonymous] public override async Task<TokenResponse> CreateToken(TokenRequest request, ServerCallContext context)
        {
            var creds=new CredentialModel
            {
                UserName = request.Username,
                Passcode = request.Password
            };
            var response = await _tokenValidationService.GenerateTokenModelAsync(creds);

            if (response.Success)
            {
                return new TokenResponse
                {
                    Token = response.Token,
                    Expiration = Timestamp.FromDateTime(response.Expiration),
                    Success = true
                };
            }
            return new TokenResponse
            {
                Success = false
            };
        }

        public override async Task<Empty> SendDiagnostics(IAsyncStreamReader<ReadingMessage> requestStream, ServerCallContext context)
        {
            _logger.LogError($"Stream Readings....");

            var theTask = Task.Run(async () =>
            {
                await foreach (var item in requestStream.ReadAllAsync())
                {
                    _logger.LogInformation($"Received a reading {item}");
                }
            });

            await theTask;
            return new Empty();
        }

       

        public override async Task<StatusMessage> AddReading(ReadingPacket request, ServerCallContext context)
        {
            _logger.LogInformation($"AddReading function executed....");

            var result = new StatusMessage
            {
                Successful = ReadingStatus.Failure
            };

            if (request.Successful == ReadingStatus.Success)
            {
                try
                {

                    foreach (var item in request.Readings)
                    {
                        if (item.ReadingValue < 1000)
                        {
                            var trailer=new Metadata
                            {
                                {"bad_value",item.ReadingValue.ToString().ToLower() },
                                {"message","values are too low" }
                            };
                            _logger.LogDebug("reading value below acceptable level");
                            throw new RpcException(new Status(StatusCode.OutOfRange,"Value too low"),trailer);
                        }


                        //save to the database
                        var reading = new MeterReading()
                        {
                            Value = item.ReadingValue,
                            ReadingDate = item.ReadingTime.ToDateTime(),
                            CustomerId = item.CustomerId,
                        };
                        repository.AddEntity(reading);
                        _logger.LogInformation($"Adding Entity....{reading}");
                    }

                    if (await repository.SaveAllAsync())
                    {
                        _logger.LogInformation($"Stored {request.Readings.Count} New Readings....");
                        result.Successful = ReadingStatus.Success;
                    }
                }
                catch (RpcException)
                {
                    throw;
                }
                catch (System.Exception exception)
                {
                    _logger.LogError($"Exception thrown during saving of readings: {exception}");
                    throw new RpcException(Status.DefaultCancelled, $"exception thrown during process {exception}");
                }
            }

            return result;
        }
    }
}