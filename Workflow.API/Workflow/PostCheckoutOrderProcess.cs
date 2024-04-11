using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.API.Workflow
{
    public class PostCheckoutOrderProcess : IWorkflow<CheckoutOrderCompleted>
    {
        public string Id => "PostCheckoutOrderProcess";

        public int Version => 1;

        public void Build(IWorkflowBuilder<CheckoutOrderCompleted> builder)
        {
            builder
                .StartWith<InitStep>().Input(step => step.Event, data => data)
                .Parallel()
                    .Do(then => then.StartWith<SendEmailStep>().Input(step => step.Event, data => data).OnError(WorkflowErrorHandling.Retry, TimeSpan.FromSeconds(10)))
                    .Do(then => then.StartWith<NotificationStep>().Input(step => step.Event, data => data).OnError(WorkflowErrorHandling.Retry, TimeSpan.FromSeconds(10)))
                .Join()
                .Then<FinalizeStep>().Input(step => step.Event, data => data);
        }
    }


    //Event
    public record CheckoutOrderCompleted
    {
        public int OrderId { get; init; }
    }


    // Init Step
    public class InitStep : StepBodyAsync
    {
        public CheckoutOrderCompleted Event { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            Console.WriteLine($"{nameof(InitStep)} - {DateTime.Now}");
            Console.WriteLine(Event.ToString());

            return ExecutionResult.Next();
        }
    }

    // Finalize Step
    public class FinalizeStep : StepBodyAsync
    {
        public CheckoutOrderCompleted Event { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            Console.WriteLine($"{nameof(FinalizeStep)} - {DateTime.Now}");
            Console.WriteLine(Event.ToString());
            

            return ExecutionResult.Next();
        }
    }

    // Send Email Step
    public class SendEmailStep : StepBodyAsync
    {
        public CheckoutOrderCompleted Event { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            Console.WriteLine($"{nameof(SendEmailStep)} - {DateTime.Now}");
            Console.WriteLine(Event.ToString());

            return ExecutionResult.Next();
        }
    }

    // Notification Step
    public class NotificationStep : StepBodyAsync
    {
        public CheckoutOrderCompleted Event { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            Console.WriteLine($"{nameof(NotificationStep)} - {DateTime.Now}");
            Console.WriteLine(Event.ToString());

            return ExecutionResult.Next();
        }
    }
}
