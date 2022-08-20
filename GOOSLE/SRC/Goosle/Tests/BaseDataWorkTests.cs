using MySql.Data.MySqlClient;

namespace Tests
{
    public class BaseDataWorkTests
    {
        static readonly string connString = "Server=db-goosle.ck2ojbqqmm8s.eu-central-1.rds.amazonaws.com;Database=goosleDB;port=3306;User Id=a100001;password=12345";
        protected MySqlConnection Connection { get; } = new MySqlConnection(connString);
    }
}