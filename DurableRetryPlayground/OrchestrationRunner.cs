using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;

namespace DurableRetryPlayground
{
    public class OrchestrationRunner
    {
        private readonly ILogger _logger;

        public OrchestrationRunner(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<OrchestrationRunner>();
        }

        [Function("OrchestrationRunner")]
        public async Task Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "runners")] HttpRequestData req,
            [DurableClient] DurableTaskClient client)
        {
            var payload = new BackgroundOptions
            {
                Priority = "Critical"
            };

            await client.ScheduleNewOrchestrationInstanceAsync(nameof(BackgroundOrchestration), payload);
        }
    }
}
