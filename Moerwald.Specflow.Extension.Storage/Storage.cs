using System.Collections.Concurrent;

namespace Moerwald.Specflow.Storage
{
    public class Storage
    {

        ConcurrentDictionary<string, string> _storage = new ConcurrentDictionary<string, string>();

        public string this[string index]
        {
            get => _storage[index];

            set => _storage[index] = value;
        }

        public bool ContainsName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new System.ArgumentException("message", nameof(name));
            }
            return _storage.ContainsKey(name);
        }
            
    }
}
