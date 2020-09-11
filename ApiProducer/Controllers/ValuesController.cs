using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiProducer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> SendData([FromQuery] string data)
        {
            var bootstrapServers = "kafka:29092";
            var nomeTopic = "teste";


            var config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var result = await producer.ProduceAsync(
                    nomeTopic,
                    new Message<Null, string>
                    { Value = data });

                return Ok(result);
            }

        }

    }

}

