using Jayride3TasksWebAPI.Data;
using Jayride3TasksWebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;

namespace Jayride3TasksWebAPI.Controllers
{
    [Route("/candidate")]
    [ApiController]
    public class Candidate : ControllerBase
    {

        [HttpGet]

        public CandidateModel getcandidate()
        {
            return Candidates.candidateLists.FirstOrDefault();
        }
    }
}
