namespace Infrastructure
{
    public interface ITaskService
    {
        Task<int> TakeBackup();        
    }
}
