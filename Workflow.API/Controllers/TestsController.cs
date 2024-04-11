using Microsoft.AspNetCore.Mvc;
using Workflow.API.Workflow;
using WorkflowCore.Interface;

namespace Workflow.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestsController : ControllerBase
    {
        private readonly IWorkflowHost workflowHost;

        public TestsController(IWorkflowHost workflowHost)
        {
            this.workflowHost = workflowHost;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var @event = new CheckoutOrderCompleted
            {
                OrderId = 1
            };

            var result = await workflowHost.StartWorkflow("PostCheckoutOrderProcess", @event);

            return Ok();
        }
    }
}
