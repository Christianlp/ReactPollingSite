using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ReactPollingSite.Controllers
{
    public class DatabaseController : Controller
    {
        public List<PollController.Poll> GetPolls()
        {
            return new List<PollController.Poll>();
        }

        public PollController.Poll GetPoll(Guid id)
        {
            return new PollController.Poll();
        }

        public bool InsertNewPoll(PollController Poll)
        {
            return true;
        }
    }
}