using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace DurableRetryPlayground
{
    public class RiskyActivity
    {
        private readonly ILogger<RiskyActivity> logger;

        public RiskyActivity(ILogger<RiskyActivity> logger)
        {
            this.logger = logger;
        }

        [Function(nameof(RiskyActivity))]
        public void Run([ActivityTrigger] BackgroundOptions options)
        {
            this.logger.LogInformation("Running with priority {priority}.", options.Priority);
            throw new ApplicationException("Failed to run!");
        }
    }
}