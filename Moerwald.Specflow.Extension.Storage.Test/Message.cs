using System.Collections.Concurrent;

namespace SpecflowExtension.Storage.Test
{
    public class Message
    {
        public ConcurrentDictionary<string, string> Parameters = new ConcurrentDictionary<string, string>();
    }
}
