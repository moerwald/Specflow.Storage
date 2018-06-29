using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace SpecflowExtension.Storage
{
    public class TableInjector
    {
        /// <summary>
        /// Used to check if we should inject a value from the storage to the table. E.g.  | IntValue    | <=$IntValue$     |
        /// </summary>
        private readonly Regex _injectValue = new Regex(@"\s*<=\s*\$(?<name>[A-Za-z0-9]+)\$", RegexOptions.Compiled);
        private Table _table;
        private Storage _storage;

        public TableInjector To(Table table)
        {
            _table = table;
            return this;
        }

        public void Inject()
        {
            foreach (var row in _table.Rows)
            {
                foreach (Match match in _injectValue.Matches(row[ColumnNames.Value]))
                {
                    var name = match.Groups["name"]?.Value ?? "No value in regex matc value in regex matchh";
                    if (_storage.ContainsName(name) == false)
                    {
                        throw new TableInjectException($"Storage doesn't contain {name}");
                    }

                    var storedValue = _storage[name];
                    row[ColumnNames.Value] = storedValue;
                }
            }
        }

        public TableInjector  From(Storage storage)
        {
            _storage = storage;
            return this;
        }

    }
}
