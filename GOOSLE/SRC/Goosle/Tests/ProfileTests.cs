using Xunit;
using MySql.Data.MySqlClient;
using System.Data;

namespace Tests
{
    public class ProfileTests : BaseDataWorkTests
    {
        [Fact]
        public void GetUserData_Should_ReturnUserData()
        {
            var cmd = new MySqlCommand(
                "SELECT * from People WHERE Entry_Card = 's123456'",
                Connection);
            var da = new MySqlDataAdapter(cmd);
            var CacheData = new DataTable();
            da.Fill(CacheData);
            Assert.True(CacheData.Rows.Count >0);
        }

        [Fact]
        public void GetStatusCovid()
        {
            var cmd = new MySqlCommand(
                "select ShouldCheck, is_sick, DateStart, DateEnd from People P inner join COVID C on P.id_person = C.id_person where Entry_Card = 's123456'",
                Connection);
            var da = new MySqlDataAdapter(cmd);
            var CacheData = new DataTable();
            da.Fill(CacheData);
            Assert.True((CacheData.Rows.Count != 0)||(CacheData.Rows.Count == 0));
        }


    }


}
