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
                new Room()
                {
                    DoorID = 1,
                    Capacity = 10,
                    HasProjector = true
                },
                new Room()
                {
                    DoorID = 2,
                    Capacity = 50,
                    HasProjector = true
                },
                new Room()
                {
                    DoorID = 3,
                    Capacity = 20,
                    HasProjector = false,
                    Courses = new List<Course>()
                    {
                        new Course(){CourseID = 1, Subject = new Subject(){ SubjectID = 1, Name = "Literature", Credits = 4}},
                        new Course(){CourseID = 2, Subject = new Subject(){ SubjectID = 1, Name = "Geography", Credits = 2}},
                        new Course(){CourseID = 3, Subject = new Subject(){ SubjectID = 2, Name = "Philosophy", Credits = 6}},
                    }
                },
                new Room()
                { DoorID = 4,
                    Capacity = 50,
                    HasProjector = true
                },
            };

            mockRoomRepo = new Mock<IRepository<Room>>();
            mockRoomRepo.Setup(m => m.ReadAll()).Returns(inputdata.AsQueryable());

            mockRoomRepo.Setup(m => m.Read(1)).Returns(inputdata[0]);
            mockRoomRepo.Setup(m => m.Read(2)).Returns(inputdata[1]);
            mockRoomRepo.Setup(m => m.Read(3)).Returns(inputdata[2]);
            mockRoomRepo.Setup(m => m.Read(4)).Returns(inputdata[3]);

            rl = new RoomLogic(mockRoomRepo.Object);
        }

        [Test]
        public void Create_Test()
        {
            //ARRANGE
            var sample = new Room()
            {
                DoorID = 5,
                Capacity = 100,
                HasProjector = true 
            };

            //ACT
            rl.Create(sample);

            //ASSERT
            mockRoomRepo.Verify(m => m.Create(sample), Times.Once);
        }

        [Test]
        public void Create_ExceptionTest()
        {
            //ARRANGE
            var sample = new Room()
            {
                DoorID = 5,
                Capacity = 5,
                HasProjector = true
            };

            //ACT & ASSERT
            Assert.That(() => rl.Create(sample), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void ProjectorRooms_Test()
        {
            //ARRANGE
            var expected = new List<Room>()
            {
                new Room(){ DoorID = 1, Capacity = 10, HasProjector = true},
                new Room(){ DoorID = 2, Capacity = 50, HasProjector = true},
                new Room(){ DoorID = 4, Capacity = 50, HasProjector = true},
            }.AsEnumerable();

            //ACT
            var result = rl.ProjectorRooms();

            //ASSERT
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void MaxCapacity_Test()
        {
            //ACT
            var result = rl.MaxCapacity();

            //ASSERT
            Assert.That(result, Is.EqualTo(50));
        }

        [Test]
        public void LargestCapacityRooms_Test()
        {
            //ARRANGE
            var expected = new List<Room>()
            {
                new Room(){ DoorID = 2, Capacity = 50, HasProjector = true},
                new Room(){ DoorID = 4, Capacity = 50, HasProjector = true},
            }.AsEnumerable();

            //ACT
            var result = rl.LargestCapacityRooms();

            //ASSERT
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void AverageCapacity_Test()
        {
            //ACT
            var result = rl.AverageCapacity();

            //ASSERT
            Assert.That(result, Is.EqualTo(32.5));
        }

        [Test]
        public void Subjects_Test()
        {
            //ARRANGE
            var expected = new List<Subject>()
            {
                new Subject() { SubjectID = 1, Name = "Literature", Credits = 4},
                new Subject() { SubjectID = 1, Name = "Geography", Credits = 2 },
                new Subject() { SubjectID = 2, Name = "Philosophy", Credits = 6 }
            }.AsEnumerable();

            //ACT
            var result = rl.Subjects(3);

            //ASSERT
            Assert.AreEqual(result, expected);
        }
    }
}
