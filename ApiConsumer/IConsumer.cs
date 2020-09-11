using System.Collections.Generic;
using System.Threading;

namespace ApiConsumer
{
    public interface IConsumer
    {

        Task ExecuteAsync(CancellationToken stopingToken,string topic);
        List<string> GetMessages();
    }
}
