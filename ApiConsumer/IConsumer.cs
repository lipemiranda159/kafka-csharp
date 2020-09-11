using System.Collections.Generic;

namespace ApiConsumer
{
    public interface IConsumer
    {
        void StartServer(string topic);
        List<string> GetMessages();
    }
}
