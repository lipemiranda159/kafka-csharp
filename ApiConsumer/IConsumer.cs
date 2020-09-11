using System.Collections.Generic;

namespace ApiConsumer
{
    public interface IConsumer
    {
        void StartServer();
        List<string> GetMessages();
    }
}
