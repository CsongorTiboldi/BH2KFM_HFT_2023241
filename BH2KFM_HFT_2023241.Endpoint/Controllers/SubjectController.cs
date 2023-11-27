using BH2KFM_HFT_2023241.Logic;
using BH2KFM_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BH2KFM_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        ISubjectLogic logic;

        public SubjectController(ISubjectLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Subject> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Subject Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Subject value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Update([FromBody] Subject value)
        {
            this.logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
