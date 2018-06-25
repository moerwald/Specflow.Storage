using TechTalk.SpecFlow;

namespace SpecflowExtension.Storage
{
    public class TableParser : IRowHasTablePersistorOnlyValue
    {
        public TablePersistor StoreValues { get; private set; } = new TablePersistor();

        public TableInjector InjectValues { get; private set; } = new TableInjector();

        public void Reset()
        {
            StoreValues = new TablePersistor();
            InjectValues = new TableInjector();
        }

        public bool RawHasTablePersistorOnlyData(TableRow row) => new TablePersistor().RawHasTablePersistorOnlyData(row);
    }
}
