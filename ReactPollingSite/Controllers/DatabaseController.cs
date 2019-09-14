using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;

namespace ReactPollingSite.Controllers
{
    public class DatabaseController : Controller
    {
        public static List<PollController.Poll> GetPolls()
        {
            return new List<PollController.Poll>();
        }

        public static PollController.Poll GetPoll(string id)
        {
            return new PollController.Poll();
        }

        public static bool InsertNewPoll(PollController.Poll Poll)
        {
            return true;
        }

        public static List<PollController.Question> GetQuestions(string poll_id)
        {
            return new List<PollController.Question>();
        }

        public static bool InsertNewQuestion(PollController.Question question, string poll_id)
        {
            return true;
        }

        public static List<PollController.Option> GetOptions(string question_id)
        {
            return new List<PollController.Option>();
        }

        public static bool InsertNewOption(PollController.Option option, string question_id)
        {
            return true;
        }

        private SqlConnection GetSqlDataConnection()
        {
            SqlConnection connect = new SqlConnection(Startup.ConnectionString);
            connect.Open();
            return connect;
        }

        private string GetSqlDatabaseString(SqlDataReader reader, string columnName)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
            {
                return reader.GetString(reader.GetOrdinal(columnName));
            }
            return "";
        }

        private int GetSqlDatabaseInt(SqlDataReader reader, string columnName)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
            {
                return reader.GetInt32(reader.GetOrdinal(columnName));
            }
            return 0;
        }

        private double GetSqlDatabaseDouble(SqlDataReader reader, string columnName)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
            {
                return reader.GetDouble(reader.GetOrdinal(columnName));
            }
            return 0.0;
        }

        private DateTime GetSqlDatabaseDateTime(SqlDataReader reader, string columnName)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
            {
                return reader.GetDateTime(reader.GetOrdinal(columnName));
            }
            return new DateTime(DateTime.MinValue.Ticks);
        }

        private bool GetSqlDatabaseBoolean(SqlDataReader reader, string columnName)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
            {
                return reader.GetBoolean(reader.GetOrdinal(columnName));
            }
            return new Boolean();
        }

        private Guid GetSqlDatabaseGuid(SqlDataReader reader, string columnName)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
            {
                return Guid.Parse(reader.GetString(reader.GetOrdinal(columnName)));
            }
            return Guid.Parse("00000000-0000-0000-0000-000000000000");
        }
    }
}