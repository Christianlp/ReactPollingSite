using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ReactPollingSite.Controllers
{
    [Route("api/[controller]")]
    public class ResultsController : Controller
    {
        [HttpGet("/api/Results/{poll_id}")]
        public List<QuestionResults> GetQuestionResults(string poll_id)
        {
            List<PollController.Question> Questions = DatabaseController.GetQuestions(poll_id);
            List<QuestionResults> Results = new List<QuestionResults>();
            Random rand = new Random();
            foreach (PollController.Question question in Questions)
            {
                QuestionResults result = new QuestionResults();
                result.question_id = question.id;
                result.Content = question.Contents;
                result.Options = new List<OptionResults>();
                List<PollController.Option> Options = DatabaseController.GetOptions(question.id.ToString());
                foreach (PollController.Option Option in Options)
                {
                    OptionResults OptionResult = new OptionResults();
                    OptionResult.option_id = Option.id;
                    OptionResult.Content = Option.Contents;
                    OptionResult.VoteCount = rand.Next(1000);
                    result.Options.Add(OptionResult);
                }
                Results.Add(result);
            }
            return Results;
        }
    }

    public class QuestionResults
    {
        public Guid question_id { get; set; }
        public string Content { get; set; }
        public List<OptionResults> Options { get; set; }
    }

    public class OptionResults
    {
        public Guid option_id { get; set; }
        public string Content { get; set; }
        public int VoteCount { get; set; }
    }
}