namespace MSDTC.Sample.Api.Interfaces.Repositories
{
    public interface IAuditClientRepository
    {
        int AddAuditClient(string message);
    }
}