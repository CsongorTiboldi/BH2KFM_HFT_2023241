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
    public class RoomController : ControllerBase
    {
        IRoomLogic logic;
        IHubContext<SignalRHub> hub;

        public RoomController(IRoomLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Room> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Room Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Room value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("RoomCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Room value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("RoomUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var value = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("RoomDeleted", value);
        }
    }
}
