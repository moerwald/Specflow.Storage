using System.Collections.Concurrent;

namespace Moerwald.Specflow.Storage.Test
{
    public class Message
    {
        public ConcurrentDictionary<string, string> Parameters = new ConcurrentDictionary<string, string>();
    }
}
