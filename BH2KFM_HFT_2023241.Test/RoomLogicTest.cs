using BH2KFM_HFT_2023241.Logic;
using BH2KFM_HFT_2023241.Models;
using BH2KFM_HFT_2023241.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH2KFM_HFT_2023241.Test
{

    [TestFixture]
    public class RoomLogicTest
    {
        Mock<IRepository<Room>> mockRoomRepo;
        RoomLogic rl;

        [SetUp]
        public void Init()
        {
            var inputdata = new List<Room>()
            {
                new Room(){ DoorID = 1, Capacity = 10, HasProjector = true},
                new Room(){ DoorID = 2, Capacity = 20, HasProjector = true},
                new Room(){ DoorID = 3, Capacity = 50, HasProjector = false},
                new Room(){ DoorID = 4, Capacity = 50, HasProjector = true},
            };

            mockRoomRepo = new Mock<IRepository<Room>>();
            mockRoomRepo.Setup(m => m.ReadAll()).Returns(inputdata.AsQueryable());
            rl = new RoomLogic(mockRoomRepo.Object);
        }

        [Test]
        public void CreateTest()
        {
            var sample = new Room()
            {
                DoorID = 5,
                Capacity = 100,
                HasProjector = true 
            };

            rl.Create(sample);
            mockRoomRepo.Verify(m => m.Create(sample), Times.Once);
        }
    }
}
