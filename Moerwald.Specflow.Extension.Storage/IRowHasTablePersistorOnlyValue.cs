using TechTalk.SpecFlow;

namespace SpecflowExtension.Storage
{
    public interface IRowHasTablePersistorOnlyValue
    {
        bool RawHasTablePersistorOnlyData(TableRow row);
    }
}
