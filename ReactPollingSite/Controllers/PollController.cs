using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ReactPollingSite.Controllers
{
    [Route("api/[controller]")]
    public class PollController : Controller
    {
        [HttpGet("/api/Polls")]
        public List<Poll> GetCreatedPolls()
        {
            return DatabaseController.GetPolls();
        }

        [HttpGet("{id}")]
        public Poll GetPoll(string id)
        {
            // Ensure that poll ID id a valid guid format and exists in the database
            Guid id_filter;
            if (Guid.TryParse(id, out id_filter))
            {
                Poll poll = DatabaseController.GetPoll(id);
                if (poll.id == new Guid())
                {
                    throw new ArgumentException(nameof(id), "The Poll requested does not exist");
                }
                return poll;
            }
            throw new ArgumentException(nameof(id), "The entered Poll ID is not valid");
        }

        [HttpPost]
        public Poll CreatePoll(Poll poll)
        {
            if (DatabaseController.InsertNewPoll(poll))
            {
                return DatabaseController.GetPoll(poll.id.ToString());
            }
            else
            {
                return new Poll();
            }
        }

        [HttpGet("/api/Questions/{poll_id}")]
        public List<Question> GetQuestions(string poll_id)
        {
            // Ensure that poll ID id a valid guid format and exists in the database
            Guid id_filter;
            if (Guid.TryParse(poll_id, out id_filter))
            {
                List<Question> questions = DatabaseController.GetQuestions(poll_id);
                return questions;
            }
            throw new ArgumentException(nameof(poll_id), "The Poll ID is not valid");
        }

        [HttpPost("/api/Question")]
        public List<Question> CreateQuestion(Question question, string poll_id)
        {
            if (DatabaseController.InsertNewQuestion(question, poll_id))
            {
                return DatabaseController.GetQuestions(question.id.ToString());
            }
            else
            {
                return new List<Question>();
            }
        }

        [HttpGet("/api/Options/{question_id}")]
        public List<Option> GetOptions(string question_id)
        {
            // Ensure that poll ID id a valid guid format and exists in the database
            Guid id_filter;
            if (Guid.TryParse(question_id, out id_filter))
            {
                List<Option> options = DatabaseController.GetOptions(question_id);
                return options;
            }
            throw new ArgumentException(nameof(question_id), "The Question ID is not valid");
        }

        [HttpPost("/api/Option")]
        public List<Option> CreateOption(Option option, string question_id)
        {
            if (DatabaseController.InsertNewOption(option, question_id))
            {
                return DatabaseController.GetOptions(option.id.ToString());
            }
            else
            {
                return new List<Option>();
            }
        }

        public class Poll
        {
            public Guid id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Creator { get; set; }
            //public List<Question> Questions { get; set; }
            public DateTime CreateDT { get; set; }
            public DateTime UpdateDT { get; set; }
        }

        public class Question
        {
            public Guid id { get; set; }
            public string Contents { get; set; }
            public int Order { get; set; }
            //public List<Option> Options { get; set; }
        }

        public class Option
        {
            public Guid id { get; set; }
            public string Contents { get; set; }
            public int Order { get; set; }
        }
    }
}
