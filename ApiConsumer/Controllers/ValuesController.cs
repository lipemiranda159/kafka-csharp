using Microsoft.AspNetCore.Mvc;
using System.Threading;

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
        public async Task<ActionResult<bool>> StartConsumer()
        {
            _consumer.ExecuteAsync(default);
            return Ok(true);
        }
    }
}
