using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
    }
}