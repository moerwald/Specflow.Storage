using System;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace SpecflowExtension.Storage
{
    /// <summary>
    /// Gets the property value from given <see cref="_objectToFetchDataFrom"/>, based on the value in <see cref="_table"/> and stores it in <see cref="_storage"/>.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public class ObjectPersistor<TObject> : IRowHasDataToPersist
    {
        private const string _RegexGroupName = "name";
        private static readonly Regex _fetchValueRegex = new Regex(@"^\s*=>\s*\$(?<name>[A-Za-z0-9]+)\$", RegexOptions.Compiled);
        private Storage _storage;
        private TObject _objectToFetchDataFrom;
        private Table _table;

        public ObjectPersistor(TObject objectToFetchDataFrom, Table table)
        {
            _objectToFetchDataFrom = objectToFetchDataFrom;
            _table = table ?? throw new ArgumentNullException(nameof(table));
        }

        /// <summary>
        /// Todo: At the time we'll  only need this CTOR to call HasDataToPersis from outside (without
        /// a given object or table). Change this!
        /// </summary>
        public ObjectPersistor()
        {
        }

        /// <summary>
        /// Checks if given <see cref="value"/> shall be persited to <see cref="_table"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool HasDataToPersist(string value) => _fetchValueRegex.IsMatch(value);

        public ObjectPersistor<TObject> In(Storage storage)
        {
            _storage = storage;
            return this;
        }

        /// <summary>
        /// Walktrough table rows. If the store syntax (<see cref="_fetchValueRegex"/>) is found in the value of the table row, the property-name
        /// (in table row value) is extracted, looked up in <see cref="_objectToFetchDataFrom"/>, and stored in <see cref="_storage"/>.
        /// </summary>
        public void Store()
        {
            if (_table is null)
            {
                throw new ArgumentNullException($"{nameof(_table)} is null. Don't call default CTOR!");
            }

            if (_objectToFetchDataFrom.Equals(default(TObject)))
            {
                throw new ArgumentNullException($"{nameof(_objectToFetchDataFrom)} is {default(TObject)}. Don't call default CTOR!");
            }

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
    }
}
