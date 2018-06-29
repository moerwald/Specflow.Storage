using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace SpecflowExtension.Storage
{
    public class ObjectPersistor<TObject> : IRowHasTablePersistorOnlyValue
    {
        private const string _RegexGroupName = "name";
        private static readonly Regex _fetchValueRegex = new Regex(@"^\s*=>\s*\$(?<name>[A-Za-z0-9]+)\$", RegexOptions.Compiled);
        private Storage _storage;
        private TObject _objectToFetchDataFrom;
        private Table _table;

        public ObjectPersistor(TObject objectToFetchDataFrom) => _objectToFetchDataFrom = objectToFetchDataFrom;

        public ObjectPersistor<TObject> In(Storage storage)
        {
            _storage = storage;
            return this;
        }

        public ObjectPersistor<TObject> And(Table table)
        {
            _table = table;
            return this;
        }

        public void Store()
        {
            foreach (var tableRow in _table.Rows)
            {
                var field = tableRow[ColumnNames.Field];
                var value = tableRow[ColumnNames.Value];

                foreach (Match match in _fetchValueRegex.Matches(value))
                {
                    var parsedName = match.Groups[_RegexGroupName]?.Value ?? throw new TableParsingException($"Could not find {nameof(_RegexGroupName)} in {tableRow}");
                    var valueFromObject = _objectToFetchDataFrom.GetType().GetProperty(field).GetValue(_objectToFetchDataFrom);
                    _storage[parsedName] = valueFromObject.ToString();
                }
            }
        }

        public bool RawHasTablePersistorOnlyData(TableRow row) => _fetchValueRegex.IsMatch(row[ColumnNames.Value]);
    }
}
