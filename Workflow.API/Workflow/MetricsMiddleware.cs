using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.API.Workflow
{
    public class PostWorkflowMetricsMiddleware : IWorkflowMiddleware
    {
        public WorkflowMiddlewarePhase Phase => WorkflowMiddlewarePhase.PostWorkflow;

        public Task HandleAsync(WorkflowInstance workflow, WorkflowDelegate next)
        {
            // TODO POST METRICS
            
            if (workflow.Status == WorkflowStatus.Complete)
            {
                // Done Inc
            }

            else if (workflow.ExecutionPointers.Any(s => s.Status == PointerStatus.Failed))
            {
                // Failed Inc
            }
            return next();
        }
    }


    public class PreWorkflowMetricsMiddleware : IWorkflowMiddleware
    {
        public WorkflowMiddlewarePhase Phase => WorkflowMiddlewarePhase.PreWorkflow;

        public Task HandleAsync(WorkflowInstance workflow, WorkflowDelegate next)
        {
            // Started Inc

            return next();
        }
    }
}
