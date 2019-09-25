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
        [HttpGet("/api/Results/{question_id}")]
        public List<VoteResult> GetQuestionResults(string question_id)
        {
            List<PollController.Option> Options = DatabaseController.GetOptions(question_id);
            List<VoteResult> Results = new List<VoteResult>();
            Random rand = new Random();
            foreach (PollController.Option option in Options)
            {
                VoteResult result = new VoteResult();
                result.question_id = Guid.Parse(question_id);
                result.option_id = option.id;
                result.VoteCount = rand.Next(1000);
                Results.Add(result);
            }
            return Results;
        }
    }

    public class VoteResult
    {
        public Guid question_id { get; set; }
        public Guid option_id { get; set; }
        public int VoteCount { get; set; }
    }
}