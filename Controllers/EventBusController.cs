using Microsoft.AspNetCore.Mvc;
using Jiangyi.EventBus.Abstractions;
namespace multi_hosp_demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventBusController : ControllerBase
    {
        IEventBus _eventBus;
        static int i = 0;
        public EventBusController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent()
        {
            _eventBus.Publish(new Jiangyi.Test.OrderIntegrationEvent(i++));
            return Ok();
        }
    }
}