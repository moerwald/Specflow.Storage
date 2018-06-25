using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Moerwald.Specflow.Storage.Test
{
    [Binding]
    public class Steps
    {
        readonly Storage _storage = new Storage();
        readonly TableParser _tableParser = new TableParser();
        private Message _message;
        

        [Given(@"the following message is generated")]
        [When(@"the following message is generated")]
        public void GivenTheFollowingMessageIsGenerated(Table table)
        {
            _tableParser.StoreValues.From(table).In(_storage).Store();
            _tableParser.InjectValues.From(_storage).To(table).Inject();
            _message = new Message();
            foreach(var row in table.Rows)
            {
                _message.Parameters[row[ColumnNames.Field]] = row[ColumnNames.Value];
            }
        }

        [Then(@"the storage has the following entries")]
        public void ThenTheStorageHasTheFollowingEntries(Table table)
        {
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
