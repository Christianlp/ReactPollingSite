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
            SqlConnection connect = GetSqlDataConnection();
            SqlCommand command = new SqlCommand("SELECT * FROM Polls", connect);
            SqlDataReader reader = command.ExecuteReader();

            DatabaseController db = new DatabaseController();
            List<PollController.Poll> Polls = new List<PollController.Poll>();
            while (reader.Read())
            {
                PollController.Poll poll = new PollController.Poll();
                poll.id = db.GetSqlDatabaseGuid(reader, "id");
                poll.Name = db.GetSqlDatabaseString(reader, "Name");
                poll.Description = db.GetSqlDatabaseString(reader, "Description");
                poll.Creator = db.GetSqlDatabaseString(reader, "Creator");
                poll.CreateDT = db.GetSqlDatabaseDateTime(reader, "CreateDT");
                poll.UpdateDT = db.GetSqlDatabaseDateTime(reader, "UpdateDT");
                Polls.Add(poll);
            }

            connect.Close();
            db.Dispose();
            return Polls;
        }

        public static PollController.Poll GetPoll(string poll_id)
        {
            SqlConnection connect = GetSqlDataConnection();
            SqlCommand command = new SqlCommand("SELECT * FROM Polls WHERE id = @poll_id", connect);
            command.Parameters.Add("@poll_id", System.Data.SqlDbType.NChar);
            command.Parameters["@poll_id"].Value = poll_id;
            SqlDataReader reader = command.ExecuteReader();

            DatabaseController db = new DatabaseController();
            PollController.Poll Poll = new PollController.Poll();
            if (reader.Read())
            {
                Poll.id = db.GetSqlDatabaseGuid(reader, "id");
                Poll.Name = db.GetSqlDatabaseString(reader, "Name");
                Poll.Description = db.GetSqlDatabaseString(reader, "Description");
                Poll.Creator = db.GetSqlDatabaseString(reader, "Creator");
                Poll.CreateDT = db.GetSqlDatabaseDateTime(reader, "CreateDT");
                Poll.UpdateDT = db.GetSqlDatabaseDateTime(reader, "UpdateDT");
            }

            connect.Close();
            db.Dispose();
            return Poll;
        }

        public static bool InsertNewPoll(PollController.Poll Poll)
        {
            SqlConnection connect = GetSqlDataConnection();
            SqlCommand PollInsert = new SqlCommand("INSERT INTO Polls " +
                "(id,Name,Description,Creator,CreateDT,UpdateDT) " +
                "VALUES(@id,@Name,@Description,@Creator,@CreateDT,@UpdateDT)",
                connect);

            PollInsert.Parameters.Add("@id", System.Data.SqlDbType.NChar);
            PollInsert.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar);
            PollInsert.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar);
            PollInsert.Parameters.Add("@Creator", System.Data.SqlDbType.NVarChar);
            PollInsert.Parameters.Add("@CreateDT", System.Data.SqlDbType.DateTime);
            PollInsert.Parameters.Add("@UpdateDT", System.Data.SqlDbType.DateTime);

            PollInsert.Parameters["@id"].Value = Convert.ToString(Poll.id);
            PollInsert.Parameters["@Name"].Value = Poll.Name;
            PollInsert.Parameters["@Description"].Value = Poll.Description;
            PollInsert.Parameters["@Creator"].Value = Poll.Creator;
            PollInsert.Parameters["@CreateDT"].Value = DateTime.Now;
            PollInsert.Parameters["@UpdateDT"].Value = DBNull.Value;

            bool returnVal = Convert.ToBoolean(PollInsert.ExecuteNonQuery());

            connect.Close();
            return returnVal;
        }

        public static List<PollController.Question> GetQuestions(string poll_id)
        {
            SqlConnection connect = GetSqlDataConnection();
            SqlCommand command = new SqlCommand("SELECT * FROM Questions WHERE PollID = @poll_id", connect);
            command.Parameters.Add("@poll_id", System.Data.SqlDbType.NChar);
            command.Parameters["@poll_id"].Value = poll_id;
            SqlDataReader reader = command.ExecuteReader();

            DatabaseController db = new DatabaseController();
            List<PollController.Question> Questions = new List<PollController.Question>();
            while (reader.Read())
            {
                PollController.Question Question = new PollController.Question();
                Question.id = db.GetSqlDatabaseGuid(reader, "id");
                Question.Contents = db.GetSqlDatabaseString(reader, "Contents");
                Question.Order = db.GetSqlDatabaseInt(reader, "OrderNum");
                Questions.Add(Question);
            }

            connect.Close();
            db.Dispose();
            return Questions;
        }

        public static bool InsertNewQuestion(PollController.Question Question, string poll_id)
        {
            SqlConnection connect = GetSqlDataConnection();
            SqlCommand QuestionInsert = new SqlCommand("INSERT INTO Questions " +
                "(id,PollID,Contents,OrderNum) " +
                "VALUES(@id,@poll_id,@Contents,@OrderNum)",
                connect);

            QuestionInsert.Parameters.Add("@id", System.Data.SqlDbType.NChar);
            QuestionInsert.Parameters.Add("@poll_id", System.Data.SqlDbType.NChar);
            QuestionInsert.Parameters.Add("@Contents", System.Data.SqlDbType.NVarChar);
            QuestionInsert.Parameters.Add("@OrderNum", System.Data.SqlDbType.Int);

            QuestionInsert.Parameters["@id"].Value = Convert.ToString(Question.id);
            QuestionInsert.Parameters["@poll_id"].Value = poll_id;
            QuestionInsert.Parameters["@Contents"].Value = Question.Contents;
            QuestionInsert.Parameters["@OrderNum"].Value = Question.Order;

            bool returnVal = Convert.ToBoolean(QuestionInsert.ExecuteNonQuery());

            connect.Close();
            return returnVal;
        }

        public static List<PollController.Option> GetOptions(string question_id)
        {
            SqlConnection connect = GetSqlDataConnection();
            SqlCommand command = new SqlCommand("SELECT * FROM Options WHERE PollID = @question_id", connect);
            command.Parameters.Add("@question_id", System.Data.SqlDbType.NChar);
            command.Parameters["@question_id"].Value = question_id;
            SqlDataReader reader = command.ExecuteReader();

            DatabaseController db = new DatabaseController();
            List<PollController.Option> Options = new List<PollController.Option>();
            while (reader.Read())
            {
                PollController.Option Option = new PollController.Option();
                Option.id = db.GetSqlDatabaseGuid(reader, "id");
                Option.Contents = db.GetSqlDatabaseString(reader, "Contents");
                Option.Order = db.GetSqlDatabaseInt(reader, "OrderNum");
                Options.Add(Option);
            }

            connect.Close();
            db.Dispose();
            return Options;
        }

        public static bool InsertNewOption(PollController.Option Option, string question_id)
        {
            SqlConnection connect = GetSqlDataConnection();
            SqlCommand OptionInsert = new SqlCommand("INSERT INTO Options " +
                "(id,QuestionID,Contents,OrderNum) " +
                "VALUES(@id,@question_id,@Contents,@OrderNum)",
                connect);

            OptionInsert.Parameters.Add("@id", System.Data.SqlDbType.NChar);
            OptionInsert.Parameters.Add("@question_id", System.Data.SqlDbType.NChar);
            OptionInsert.Parameters.Add("@Contents", System.Data.SqlDbType.NVarChar);
            OptionInsert.Parameters.Add("@OrderNum", System.Data.SqlDbType.Int);

            OptionInsert.Parameters["@id"].Value = Convert.ToString(Option.id);
            OptionInsert.Parameters["@question_id"].Value = question_id;
            OptionInsert.Parameters["@Contents"].Value = Option.Contents;
            OptionInsert.Parameters["@OrderNum"].Value = Option.Order;

            bool returnVal = Convert.ToBoolean(OptionInsert.ExecuteNonQuery());

            connect.Close();
            return returnVal;
        }

        private static SqlConnection GetSqlDataConnection()
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