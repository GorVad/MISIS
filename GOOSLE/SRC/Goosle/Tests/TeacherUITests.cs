using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xunit;

namespace Tests
{
    public class TeacherUITests : BaseDataWorkTests
    {
        [Fact]
        public void CheckUserIsAdministrator_Should_True()
        {
            var id_person = 4;
            var cmd = new MySqlCommand($"SELECT * FROM Administration WHERE id_person={id_person} LIMIT 1", Connection);
            var adapter = new MySqlDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);
            Assert.True(table.Rows.Count != 0);
        }
    }
}
