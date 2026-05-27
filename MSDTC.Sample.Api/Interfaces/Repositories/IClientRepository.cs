namespace MSDTC.Sample.Api.Interfaces.Repositories
{
    public interface IClientRepository
    {
        int AddClient(string name);
        int GetClientByName(string name);
    }
}