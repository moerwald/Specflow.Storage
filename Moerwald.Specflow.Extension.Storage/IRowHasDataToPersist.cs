using TechTalk.SpecFlow;

namespace SpecflowExtension.Storage
{
    public interface IRowHasDataToPersist
    {
        bool HasDataToPersist(string value);
    }
}
