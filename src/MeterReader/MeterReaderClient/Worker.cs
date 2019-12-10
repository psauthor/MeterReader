using Grpc.Net.Client;
using MeterReaderWeb.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MeterReaderClient
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private readonly ReadingFactory factory;
        private MeterReadingService.MeterReadingServiceClient _client;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, ReadingFactory factory)
        {
            _logger = logger;
            this._config = configuration;
            this.factory = factory;
        }

        private MeterReadingService.MeterReadingServiceClient Client
        {
            get
            {
                if (_client == null)
                {
                    var channel = GrpcChannel.ForAddress(_config.GetValue<string>("Service:ServerUrl"));
                    _client = new MeterReadingService.MeterReadingServiceClient(channel);
                }
                return _client;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var customerId = _config.GetValue<int>("Service:CustomerId");
                var packet = new ReadingPacket
                {
                    Successful = ReadingStatus.Success,
                    Notes = "This is our test",
                };

                for (int i = 0; i < 5; i++)
                {
                    packet.Readings.Add(await factory.Generate(customerId));
                }

                var result = await Client.AddReadingAsync(packet);

                if (result.Successful == ReadingStatus.Success)
                {
                    _logger.LogInformation("Successfully sent");
                }
                else
                {
                    _logger.LogInformation("Failed to send");
                }

                await Task.Delay(_config.GetValue<int>("Service:DelayInterval"), stoppingToken);
            }
        }
    }
}