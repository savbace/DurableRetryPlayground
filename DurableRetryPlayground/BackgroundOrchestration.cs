using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;

namespace DurableRetryPlayground
{
    public class BackgroundOrchestration
    {
        [Function(nameof(BackgroundOrchestration))]
        public async Task Run([OrchestrationTrigger] TaskOrchestrationContext context)
        {
            var input = context.GetInput<BackgroundOptions>();
            TaskOptions options = TaskOptions.FromRetryHandler(context =>
            {
                if (!context.LastFailure.IsCausedBy<ApplicationException>())
                {
                    return false;
                }

                return context.LastAttemptNumber < 3;
            });

            // bug/confusing: LastFailure is AggregateException
            await context.CallActivityAsync(nameof(RiskyActivity), input, options);

            // correct: LastFailure is ApplicationException
            //await context.CallActivityAsync("RiskyActivitySync", input, options);
        }
    }
}