using BH2KFM_HFT_2023241.Logic;
using BH2KFM_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BH2KFM_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RoomStatController : ControllerBase
    {
        IRoomLogic logic;

        public RoomStatController(IRoomLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Room> ProjectorRooms()
        {
            return this.logic.ProjectorRooms();
        }

        [HttpGet]
        public int MaxCapacity()
        {
            return this.logic.MaxCapacity();
        }

        [HttpGet("{roomId}")]
        public double AverageSubjectCreditInRoom(int roomId)
        {
            return this.logic.AverageSubjectCreditInRoom(roomId);
        }

        [HttpGet("{roomId}")]
        public int MaxSubjectSemesterInRoom(int roomId)
        {
            return this.logic.MaxSubjectSemesterInRoom(roomId);
        }

        [HttpGet("{roomId}")]
        public IEnumerable<Subject> Subjects(int roomId)
        {
            return this.logic.Subjects(roomId);
        }
    }
}
