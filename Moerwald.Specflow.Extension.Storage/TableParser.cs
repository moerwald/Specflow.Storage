using TechTalk.SpecFlow;

namespace SpecflowExtension.Storage
{
    public class TableParser : IRowHasDataToPersist
    {
        public TablePersistor StoreValues { get; private set; } = new TablePersistor();

        public TableInjector InjectValues { get; private set; } = new TableInjector();

        public void Reset()
        {
            StoreValues = new TablePersistor();
            InjectValues = new TableInjector();
        }

        public bool HasDataToPersist(string value) => new TablePersistor().HasDataToPersist(value);
    }
}
