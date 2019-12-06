using Grpc.Core;
using MeterReaderWeb.Data;
using MeterReaderWeb.Data.Entities;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MeterReaderWeb.Services
{
    public class MeterService : MeterReadingService.MeterReadingServiceBase
    {
        private readonly ILogger<MeterService> logger;
        private readonly IReadingRepository repository;

        public MeterService(ILogger<MeterService> logger, IReadingRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public override async Task<StatusMessage> AddReading(ReadingPacket request, ServerCallContext context)
        {
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
                        //save to the database
                        var reading = new MeterReading()
                        {
                            Value = item.ReadingValue,
                            ReadingDate = item.ReadingTime.ToDateTime()
                        };
                        repository.AddEntity(reading);
                    }
                    if (await repository.SaveAllAsync())
                    {
                        result.Successful = ReadingStatus.Success;
                    }
                }
                catch (System.Exception exception)
                {
                    result.Message = exception.Message;
                    logger.LogError($"Exception thrown during saving of readings: {exception}");
                }
            }

            return result;
        }
    }
}