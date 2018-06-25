namespace SpecflowExtension.Storage
{
    public class TableParser
    {
        public TablePersistor StoreValues { get; } = new TablePersistor();

        public TableInjector InjectValues { get;  } = new TableInjector();
    }
}
