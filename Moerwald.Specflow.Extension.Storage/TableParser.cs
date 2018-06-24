using TechTalk.SpecFlow;

namespace Moerwald.Specflow.Storage
{
    public class TableParser
    {
        private Table table;

        public TableParser StoreValues { get { return this; } }

        public TableParser From(Table table)
        {
            this.table = table;
            return this;
        }

        public void In (Storage storage)
        {

        }
    }
}
