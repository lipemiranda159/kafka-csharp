using Confluent.Kafka;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiConsumer
{
    public class Consumer : IConsumer
    {
        private readonly List<string> _messages;
        public Consumer()
        {
            _messages = new List<string>();
        }
        public List<string> GetMessages()
        {
            return _messages;
        }

        public Task ExecuteAsync(CancellationToken stopingToken)
        {
            Task.Run(() => StartServer(stoppingToken));  
            return Task.CompletedTask;  
        }

        private async Task StartServer(CancellationToken stopingToken)
        {
            var logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
            logger.Information("Testando o consumo de mensagens com Kafka");


            string bootstrapServers = "kafka:29092";
            string nomeTopic = "teste";

            logger.Information($"BootstrapServers = {bootstrapServers}");
            logger.Information($"Topic = {nomeTopic}");

            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = $"{nomeTopic}-group-0",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
                {
                    consumer.Subscribe(nomeTopic);

                    try
                    {
                        while (true)
                        {
                            var cr = consumer.Consume(cts.Token);
                            logger.Information(cr.Message.Value);
                            _messages.Add(cr.Message.Value);
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        consumer.Close();
                        logger.Warning("Cancelada a execução do Consumer...");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Exceção: {ex.GetType().FullName} | " +
                             $"Mensagem: {ex.Message}");
            }
        }

        
    }
}
