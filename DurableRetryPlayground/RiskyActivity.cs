using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace DurableRetryPlayground;

public class RiskyActivity
{
    private readonly ILogger<RiskyActivity> logger;

    public RiskyActivity(ILogger<RiskyActivity> logger)
    {
        this.logger = logger;
    }

    [Function(nameof(RiskyActivity))]
    public async Task Run([ActivityTrigger] BackgroundOptions options)
    {
        this.logger.LogInformation("Running with priority {priority}.", options.Priority);
        await Task.Delay(3000);
        throw new ApplicationException("Failed to run!");
    }

    [Function("RiskyActivitySync")]
    public void RunSync([ActivityTrigger] BackgroundOptions options)
    {
        this.logger.LogInformation("Sync: Running with priority {priority}.", options.Priority);
        throw new ApplicationException("Sync: Failed to run!");
    }
}