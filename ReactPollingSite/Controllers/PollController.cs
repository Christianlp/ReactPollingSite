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

        [HttpPost("/api/Poll")]
        public void CreatePoll(string Name, string Description, int Count)
        {
            //Create Poll
            Poll newPoll = new Poll();
            newPoll.id = Guid.NewGuid();
            newPoll.Name = Name;
            newPoll.Description = Description;
            newPoll.Creator = "localhost";
            newPoll.CreateDT = DateTime.Now;
            DatabaseController.InsertNewPoll(newPoll);

            //Create Questions for Poll
            for(int i = 0; i < Count; i++)
            {
                Question question = new Question();
                question.id = Guid.NewGuid();
                question.Contents = GenerateText(7, 20);
                question.Order = i;
                DatabaseController.InsertNewQuestion(question, newPoll.id.ToString());

                //Create Options for Question
                Random rand = new Random();
                for (int j = 0; j < rand.Next(2, 6); j++)
                {
                    Option option = new Option();
                    option.id = Guid.NewGuid();
                    option.Contents = GenerateText(2, 8);
                    option.Order = j;
                    DatabaseController.InsertNewOption(option, question.id.ToString());
                }
            }

            Response.Redirect("/poll/" + newPoll.id, true);
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

        private string GenerateText(int minWords, int maxWords)
        {
            const string FullText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Neque vitae tempus quam pellentesque. Nunc congue nisi vitae suscipit tellus mauris. Commodo ullamcorper a lacus vestibulum sed arcu non odio. Orci nulla pellentesque dignissim enim sit amet. Vulputate eu scelerisque felis imperdiet proin fermentum leo vel. Ut diam quam nulla porttitor massa. Natoque penatibus et magnis dis. Rhoncus est pellentesque elit ullamcorper dignissim cras tincidunt lobortis feugiat. Ut sem viverra aliquet eget sit. Imperdiet proin fermentum leo vel orci porta non pulvinar. Vulputate eu scelerisque felis imperdiet proin. Quis commodo odio aenean sed adipiscing. Turpis egestas sed tempus urna et.";
            Random rand = new Random();
            return String.Join(" ", FullText.Split(" ").Take(rand.Next(minWords,maxWords)));
        }

        public class Poll
        {
            public Guid id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Creator { get; set; }
            public DateTime CreateDT { get; set; }
            public DateTime UpdateDT { get; set; }
        }

        public class Question
        {
            public Guid id { get; set; }
            public string Contents { get; set; }
            public int Order { get; set; }
        }

        public class Option
        {
            public Guid id { get; set; }
            public string Contents { get; set; }
            public int Order { get; set; }
        }
    }
}
