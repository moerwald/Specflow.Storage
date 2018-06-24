using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace Moerwald.Specflow.Storage
{
    public class TableParser
    {
        /// <summary>
        /// used if value and value name are given in one line. E.g. "TestMessage =>$MessageType$"
        /// </summary>
        private readonly Regex _valueIsGivenStoreIt = new Regex(@"(?<value>.*)\s*=>\s*\$(?<name>[A-Za-z0-9]+)\$", RegexOptions.Compiled);
        private Storage _storage;
        private Table _table;

        private const string Value = "value";
        private const string Name = "name";

        public TableParser StoreValues { get { return this; } }

        public TableParser From(Table table)
        {
            _table = table ?? throw new System.ArgumentNullException(nameof(table));
            return this;
        }

        public TableParser In(Storage storage)
        {
            _storage = storage;
            return this;
        }


        public void Parse()
        {
            foreach (var tableRow in _table.Rows)
            {
                var field = tableRow[ColumnNames.Field];
                var value = tableRow[ColumnNames.Value];

                foreach (Match match in _valueIsGivenStoreIt.Matches(value))
                {
                    var parsedValue = match.Groups[Value]?.Value ?? throw new TableParsingException($"Could not find {nameof(Value)} in {tableRow}");
                    var parsedName = match.Groups[Name]?.Value ?? throw new TableParsingException($"Could not find {nameof(Name)} in {tableRow}");

                    _storage[parsedName] = parsedValue.Trim();
                }
            }
        }
    }
}
