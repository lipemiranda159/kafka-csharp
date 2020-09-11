using Microsoft.AspNetCore.Mvc;

namespace ApiConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConsumer _consumer;
        public ValuesController(IConsumer consumer)
        {
            _consumer = consumer;
        }

        [HttpGet]
        public ActionResult<bool> StartConsumer()
        {
            _consumer.StartServer();
            return Ok(true);
        }
    }
}
