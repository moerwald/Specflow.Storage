using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SpecflowExtension.Storage.Test
{
    [Binding]
    public class Steps
    {
        readonly Storage _storage = new Storage();
        private Message _message;

        [Given(@"the following message is generated")]
        [When(@"the following message is generated")]
        public void GivenTheFollowingMessageIsGenerated(Table table)
        {
            var tableParser = new TableParser();

            // Inject value in table
            tableParser.InjectValues.From(_storage).To(table).Inject();
            // Store values from table
            tableParser.StoreValues.From(table).In(_storage).Store();
            _message = new Message();
            foreach(var row in table.Rows)
            {
                if (!tableParser.HasDataToPersist(row[ColumnNames.Value]))
                {
                    _message.Parameters[row[ColumnNames.Field]] = row[ColumnNames.Value];
                }
            }

            // Store values from object
            tableParser.StoreValues.From(_message, table).In(_storage).Store();
        }

        [Then(@"the storage has the following entries")]
        public void ThenTheStorageHasTheFollowingEntries(Table table)
        {
            var tableParser = new TableParser();
            tableParser.InjectValues.From(_storage).To(table).Inject();
            foreach(var row in table.Rows)
            {
                StringAssert.AreEqualIgnoringCase(row[ColumnNames.Value], _storage[row[ColumnNames.Field]]);
            }
        }

        [Then(@"the message contains")]
        public void ThenTheMessageContains(Table table)
        {
            foreach (var row in table.Rows)
            {
                var name = row[ColumnNames.Field];
                var value = row[ColumnNames.Value];

                Assert.IsTrue(_message.Parameters.ContainsKey(name));
                StringAssert.AreEqualIgnoringCase(value, _message.Parameters[name]);
            }
        }
    }
}
