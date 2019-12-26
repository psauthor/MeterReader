using Grpc.Net.Client;
using MeterReaderWeb.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;

namespace MeterReaderClient
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private readonly ReadingFactory _factory;
        private readonly ILoggerFactory _loggerFactory;
        private MeterReadingService.MeterReadingServiceClient _client;
        private string _token;
        private DateTime _expiration=DateTime.MinValue;
        public Worker(ILogger<Worker> logger, IConfiguration configuration, ReadingFactory factory,ILoggerFactory loggerFactory)
        {
            _logger = logger;
            this._config = configuration;
            this._factory = factory;
            _loggerFactory = loggerFactory;
        }

        protected bool NeedsLogin() => string.IsNullOrWhiteSpace(_token) || _expiration > DateTime.UtcNow;
        private MeterReadingService.MeterReadingServiceClient Client
        {
            get
            {
                if (_client == null)
                {
                    var options=new GrpcChannelOptions
                    {
                         LoggerFactory = _loggerFactory,

                    };
                    var channel = GrpcChannel.ForAddress(_config.GetValue<string>("Service:ServerUrl"));
                    _client = new MeterReadingService.MeterReadingServiceClient(channel);
                }
                return _client;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var customerId = _config.GetValue<int>("Service:CustomerId");
            var counter = 0;
            while (!stoppingToken.IsCancellationRequested)
            {
                counter++;

                //if (counter % 10 == 0)
                //{
                //    Console.WriteLine("sending diagnostics");
                //    var stream = Client.SendDiagnostics();
                //    for (int i = 0; i < 5; i++)
                //    {
                //        var reading = await _factory.Generate(customerId);
                //        await stream.RequestStream.WriteAsync(reading);
                //    }
                //    await stream.RequestStream.CompleteAsync();
                //}
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var packet = new ReadingPacket
                {
                    Successful = ReadingStatus.Success,
                    Notes = "This is our test",
                };

                for (int i = 0; i < 5; i++)
                {
                    packet.Readings.Add(await _factory.Generate(customerId));
                }

                try
                {
                    if (!NeedsLogin()|| await GenerateToken())
                    {
                        var headers = new Metadata {{"Authorization", $"Bearer {_token}"}};
                        var result = await Client.AddReadingAsync(packet,headers:headers);

                        _logger.LogInformation(result.Successful == ReadingStatus.Success
                            ? "Successfully sent"
                            : "Failed to send");
                    }

                }
                catch (RpcException e)
                {
                    _logger.LogError($"Exception thrown {e}");
                    Console.WriteLine(e);
                    throw;
                }
                await Task.Delay(_config.GetValue<int>("Service:DelayInterval"), stoppingToken);


            }
        }

        private async Task<bool> GenerateToken()
        {
            var request=new TokenRequest
            {
                Username = _config["Service:username"],
                Password = _config["Service:password"]
            };
            var response = await Client.CreateTokenAsync(request);
            if (response.Success)
            {
                _token = response.Token;
                _expiration = response.Expiration.ToDateTime();

                return true;
            }

            return false;
        }
    }
}