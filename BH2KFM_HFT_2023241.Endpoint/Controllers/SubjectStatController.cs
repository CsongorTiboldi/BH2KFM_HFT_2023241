using BH2KFM_HFT_2023241.Logic;
using BH2KFM_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BH2KFM_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SubjectStatController : ControllerBase
    {
        ISubjectLogic logic;

        public SubjectStatController(ISubjectLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public double AverageCreditValue()
        {
            return this.logic.AverageCreditValue();
        }

        [HttpGet("{semester}")]
        public IEnumerable<Subject> SubjectsInSemester(int semester)
        {
            return this.logic.SubjectsInSemester(semester);
        }

        [HttpGet("{credit}")]
        public IEnumerable<Subject> SubjectsWithCreditValue(int credit)
        {
            return this.logic.SubjectsWithCreditValue(credit);
        }

        [HttpGet("{subject}")]
        public IEnumerable<Room> Rooms(int subject)
        {
            return this.logic.Rooms(subject);
        }

        [HttpGet]
        public int MostCreditSemester()
        {
            return this.logic.MostCreditSemester();
        }
    }
}
