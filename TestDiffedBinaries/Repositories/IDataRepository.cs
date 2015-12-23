namespace TestDiffedBinaries.Api.Repositories
{
    public interface IDataRepository
    {
        byte[] Get();
        void Create(byte[] bytes);
        void Delete();
        void Update(byte[] bytes);
    }
}