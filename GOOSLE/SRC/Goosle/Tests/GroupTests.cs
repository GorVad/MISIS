using Xunit;
using MySql.Data.MySqlClient;
using System.Data;

namespace Tests
{
    public class GroupTests : BaseDataWorkTests
    {
        [Fact]
        public void GetGroupList_Should_ReturnGroupList()
        {
            var cmd = new MySqlCommand(
                "SELECT Name, Surname, Patronymic, Birthday, Age from (SELECT Name, Surname, Patronymic, Birthday, Age, id_group from Students S Inner JOIN People P on S.id_person = P.id_person) as T where(T.id_group IN(SELECT T1.id_group from(SELECT id_group, Entry_Card from Students S Inner JOIN People P on S.id_person = P.id_person) as T1 where T1.Entry_Card = 's123456'))",
                Connection);
            var da = new MySqlDataAdapter(cmd);
            var CacheData = new DataTable();
            da.Fill(CacheData);
            Assert.True(CacheData.Rows.Count != 0);
        }
    }
}
