using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SALON_HAIR_CORE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SALON_HAIR_API.BackGround
{
    internal class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        public IServiceProvider Services { get; }
        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceProvider services)
        {
            Services = services;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, new TimeSpan(1, 0, 0),
                TimeSpan.FromDays(1));
            return Task.CompletedTask;

        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Do work.");
            using (var scope = Services.CreateScope())
            {
                var _invoice =
                    scope.ServiceProvider
                        .GetRequiredService<IInvoice>();

                var listInvoice = _invoice.GetAll().Where(e => e.Created.Value.Date != DateTime.Now.Date).ToList();
                listInvoice.ForEach(e =>
                {
                    e.IsDisplay = false;
                });

                _invoice.EditRangeAsync(listInvoice);
                _logger.LogInformation($"Change {listInvoice.Count} item(s).");
            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        static public void WriteFile(string tempalte, string nameEntity, string nameFile, string folder)
        {
            string intanceName = Char.ToLowerInvariant(nameEntity[0]) + nameEntity.Substring(1);
            System.IO.File.WriteAllText($@"{folder}\{nameFile}", tempalte.Replace("{ClassName}", nameEntity).Replace("{InstanceName}", intanceName));
        }


    }
}
