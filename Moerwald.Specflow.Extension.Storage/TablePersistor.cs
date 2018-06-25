using System;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace SpecflowExtension.Storage
{
    public class TablePersistor : IRowHasTablePersistorOnlyValue
    {
        /// <summary>
        /// used if value and value name are given in one line. E.g. "TestMessage =>$MessageType$"
        /// </summary>
        private static readonly Regex _valueIsGivenStoreItRegex = new Regex(@"^\s*(?<value>.*)\s*=>\s*\$(?<name>[A-Za-z0-9]+)\$", RegexOptions.Compiled);
        private const string _RegexGroupValue = "value";
        private const string _RegexGroupName = "name";
        private Storage _storage;
        private Table _table;

        public TablePersistor From(Table table)
        {
            _table = table ?? throw new ArgumentNullException(nameof(table));
            return this;
        }

        public TablePersistor In(Storage storage)
        {
            _storage = storage;
            return this;
        }

        public ObjectPersistor<TObject> From<TObject>(TObject objectToFetchDataFrom) => new ObjectPersistor<TObject>(objectToFetchDataFrom);

        public void Store()
        {
            foreach (var tableRow in _table.Rows)
            {
                var field = tableRow[ColumnNames.Field];
                var value = tableRow[ColumnNames.Value];

                foreach (Match match in _valueIsGivenStoreItRegex.Matches(value))
                {
                    var parsedValue = match.Groups[_RegexGroupValue]?.Value ?? throw new TableParsingException($"Could not find {nameof(_RegexGroupValue)} in {tableRow}");
                    var parsedName = match.Groups[_RegexGroupName]?.Value ?? throw new TableParsingException($"Could not find {nameof(_RegexGroupName)} in {tableRow}");
                    var valueToStore = parsedValue.Trim();
                    _storage[parsedName] = valueToStore;
                    tableRow[ColumnNames.Value] = valueToStore;
                    continue;
                }

            }
        }

        public bool RawHasTablePersistorOnlyData(TableRow row) => new ObjectPersistor<string>(string.Empty /* Something to feed the CTOR*/).RawHasTablePersistorOnlyData(row);
    }
}
