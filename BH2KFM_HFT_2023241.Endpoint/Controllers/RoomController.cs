using BH2KFM_HFT_2023241.Logic;
using BH2KFM_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BH2KFM_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        IRoomLogic logic;

        public RoomController(IRoomLogic logic)
        {
            this.logic = logic;
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
        }

        [HttpPut]
        public void Update([FromBody] Room value)
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
