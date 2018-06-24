using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Moerwald.Specflow.Storage.Test
{
    [Binding]
    public class Steps
    {
        readonly Storage _storage = new Storage();
        readonly TableParser _tableParser = new TableParser();


        [Given(@"the following message is generated")]
        public void GivenTheFollowingMessageIsGenerated(Table table)
        {
            _tableParser.StoreValues.From(table).In(_storage).Parse();
        }

        [Then(@"the storage has the following entries")]
        public void ThenTheStorageHasTheFollowingEntries(Table table)
        {
            foreach(var row in table.Rows)
            {
                StringAssert.AreEqualIgnoringCase(row["Value"], _storage[row["Field"]]);
            }
        }
    }
}
