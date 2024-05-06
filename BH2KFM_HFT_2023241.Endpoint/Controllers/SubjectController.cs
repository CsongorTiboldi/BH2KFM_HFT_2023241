using BH2KFM_HFT_2023241.Endpoint.Services;
using BH2KFM_HFT_2023241.Logic;
using BH2KFM_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace BH2KFM_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        ISubjectLogic logic;
        IHubContext<SignalRHub> hub;

        public SubjectController(ISubjectLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("SubjectCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Subject value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("SubjectUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var value = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("SubjectDeleted", value);
        }
    }
}
