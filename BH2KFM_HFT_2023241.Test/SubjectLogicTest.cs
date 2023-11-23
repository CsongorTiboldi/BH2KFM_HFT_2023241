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
    public class SubjectLogicTest
    {
        Mock<IRepository<Subject>> mockSubjectRepo;
        SubjectLogic sl;

        [SetUp]
        public void Init()
        {
            var inputdata = new List<Subject>()
            {
                new Subject(){SubjectID = 1, Name = "Organic chemistry", Credits = 5, Semester = 1 },
                new Subject(){SubjectID = 2, Name = "Algebra", Credits = 5, Semester = 2 },
                new Subject(){SubjectID = 3, Name = "Architecture", Credits = 4, Semester = 2 },
                new Subject()
                {
                    SubjectID = 4,
                    Name = "Embeded systems",
                    Credits = 6,
                    Semester = 3,
                    Courses = new List<Course>()
                    {
                        new Course(){ CourseID = 1, Room = new Room() { DoorID = 1, Capacity = 100, HasProjector = true}},
                        new Course(){ CourseID = 2, Room = new Room() { DoorID = 2, Capacity = 200, HasProjector = false}},
                        new Course(){ CourseID = 3, Room = new Room() { DoorID = 3, Capacity = 300, HasProjector = true}}
                    }
                },
            };

            mockSubjectRepo = new Mock<IRepository<Subject>>();
            mockSubjectRepo.Setup(m => m.ReadAll()).Returns(inputdata.AsQueryable());

            mockSubjectRepo.Setup(m => m.Read(1)).Returns(inputdata[0]);
            mockSubjectRepo.Setup(m => m.Read(2)).Returns(inputdata[1]);
            mockSubjectRepo.Setup(m => m.Read(3)).Returns(inputdata[2]);
            mockSubjectRepo.Setup(m => m.Read(4)).Returns(inputdata[3]);

            sl = new SubjectLogic(mockSubjectRepo.Object);
        }

        [Test]
        public void Create_Test()
        {
            //ARRANGE
            var sample = new Subject()
            {
                SubjectID = 5,
                Name = "Thermodynamics",
                Credits = 6,
                Semester = 3
            };

            //ACT
            sl.Create(sample);

            //ASSERT
            mockSubjectRepo.Verify(m => m.Create(sample), Times.Once);
        }

        [TestCase(-1)]
        [TestCase(11)]
        public void Create_ExceptionTest(int credits)
        {
            //ARRANGE
            var sample = new Subject()
            {
                SubjectID = 5,
                Name = "Thermodynamics",
                Credits = credits,
                Semester = 3
            };

            //ACT & ASSERT
            Assert.That(() => sl.Create(sample), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void AverageCreditValue_Test()
        {
            //ACT
            var result = sl.AverageCreditValue();

            //ASSERT
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void SubjectsInSemester_Test()
        {
            //ARRANGE
            var expected = new List<Subject>()
            {
                new Subject(){SubjectID = 2, Name = "Algebra", Credits = 5, Semester = 2 },
                new Subject(){SubjectID = 3, Name = "Architecture", Credits = 4, Semester = 2 }
            }.AsEnumerable();

            //ACT
            var result = sl.SubjectsInSemester(2);

            //ASSERT
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void SubjectsWithCreditValue_Test()
        {
            //ARRANGE
            var expected = new List<Subject>()
            {
                new Subject(){SubjectID = 1, Name = "Organic chemistry", Credits = 5, Semester = 1 },
                new Subject(){SubjectID = 2, Name = "Algebra", Credits = 5, Semester = 2 }
            }.AsEnumerable();

            //ACT
            var result = sl.SubjectsWithCreditValue(5);

            //ASSERT
            Assert.AreEqual(result, expected);
        }


        [Test]
        public void Rooms_Test()
        {
            //ARRANGE
            var expected = new List<Room>()
            {
                new Room() { DoorID = 1, Capacity = 100, HasProjector = true},
                new Room() { DoorID = 2, Capacity = 200, HasProjector = false },
                new Room() { DoorID = 3, Capacity = 300, HasProjector = true }
            }.AsEnumerable();

            //ACT
            var result = sl.Rooms(4);

            //ASSERT
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void MostCreditSemester_Test()
        {
            //ACT
            var result = sl.MostCreditSemester();

            //ASSERT
            Assert.That(result, Is.EqualTo(2));
        }
    }
}
