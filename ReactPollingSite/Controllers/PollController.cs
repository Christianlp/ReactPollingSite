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
        [HttpGet]
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
                    throw new ArgumentException(nameof(id), "The entered Poll ID does not exist");
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

        public class Poll
        {
            public Guid id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Creator { get; set; }
            public List<Question> Questions { get; set; }
            public DateTime CreateDT { get; set; }
            public DateTime UpdateDT { get; set; }
        }

        public class Question
        {
            public Guid id { get; set; }
            public string Contents { get; set; }
            public int Order { get; set; }
            public List<Option> Options { get; set; }
        }

        public class Option
        {
            public Guid id { get; set; }
            public string Contents { get; set; }
            public int Order { get; set; }
        }
    }
}
