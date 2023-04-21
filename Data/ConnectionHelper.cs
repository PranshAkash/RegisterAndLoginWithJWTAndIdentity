namespace Data
{
    public interface IConnectionHelper
    {
        string ConnectionString { get; set; }
        string ConnectionString_bk { get; set; }
    }

    public class ConnectionHelper : IConnectionHelper
    {
        public string ConnectionString { get; set; }
        public string ConnectionString_bk { get; set; }
    }
}
