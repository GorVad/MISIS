using Xunit;
using MySql.Data.MySqlClient;
using System.Data;

namespace Tests
{
    public class StudentsUITests : BaseDataWorkTests
    {
        [Fact]
        public void GetUserShedule_Should_ReturnScheduleData()
        {
            var cmd =  new MySqlCommand(
                "SELECT * " +
                "FROM Schedule " +
                    "JOIN `Group` ON Schedule.id_group = `Group`.id_group " +
                    "JOIN Teachers ON Teachers.id_teacher = Schedule.id_teacher " +
                    "JOIN People ON Teachers.id_teacher = People.id_person " +
                    "JOIN Lesson_duration ON Lesson_duration.id_lesson = Schedule.id_lesson " +
                    "JOIN Subjects ON Schedule.id_subject = Subjects.id_subject " +
                    "JOIN Offices ON Schedule.id_office = Offices.id_office " +
                $"WHERE People.id_person = {1}",
                Connection);
            var da = new MySqlDataAdapter(cmd);
            var CacheData = new DataTable();
            da.Fill(CacheData);
            Assert.True(CacheData.Rows.Count != 0);
        }

        [Fact]
        public void GetGroupShedule_Should_ReturnScheduleData()
        {
            var groupId = 1;
            var groupSchedule = new MySqlCommand(
                "SELECT * " +
                "FROM Schedule " +
                    "JOIN `Group` ON Schedule.id_group = `Group`.id_group " +
                    "JOIN Teachers ON Teachers.id_teacher = Schedule.id_teacher " +
                    "JOIN People ON Teachers.id_teacher = People.id_person " +
                    "JOIN Lesson_duration ON Lesson_duration.id_lesson = Schedule.id_lesson " +
                    "JOIN Subjects ON Schedule.id_subject = Subjects.id_subject " +
                    "JOIN Offices ON Schedule.id_office = Offices.id_office " +
                $"WHERE Schedule.id_group = {groupId}",
                Connection);
            var da = new MySqlDataAdapter(groupSchedule);
            var CacheData = new DataTable();
            da.Fill(CacheData);
            Assert.True(CacheData.Rows.Count != 0);
        }
    }
}
