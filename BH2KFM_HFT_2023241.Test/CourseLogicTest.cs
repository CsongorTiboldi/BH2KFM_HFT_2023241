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
    public class CoursetLogicTest
    {
        Mock<IRepository<Course>> mockCourseRepo;
        CourseLogic cl;

        [SetUp]
        public void Init()
        {
            var inputdata = new List<Course>()
            {
                new Course(){ CourseID = 1, StartTime = new DateTime(2023,9,4,8,0,0), EndTime = new DateTime(2023,9,4,9,30,0), CourseSubject = 1, Location = 1},
                new Course(){ CourseID = 2, StartTime = new DateTime(2023,9,4,9,0,0), EndTime = new DateTime(2023,9,4,10,0,0), CourseSubject = 2, Location = 1},
                new Course(){ CourseID = 3, StartTime = new DateTime(2023,9,5,8,0,0), EndTime = new DateTime(2023,9,5,10,0,0), CourseSubject = 1, Location = 1},
                new Course(){ CourseID = 4, StartTime = new DateTime(2023,9,6,10,0,0), EndTime = new DateTime(2023,9,6,11,0,0), CourseSubject = 1, Location = 1},
            };

            mockCourseRepo = new Mock<IRepository<Course>>();
            mockCourseRepo.Setup(m => m.ReadAll()).Returns(inputdata.AsQueryable());

            mockCourseRepo.Setup(m => m.Read(1)).Returns(inputdata[0]);
            mockCourseRepo.Setup(m => m.Read(2)).Returns(inputdata[1]);
            mockCourseRepo.Setup(m => m.Read(3)).Returns(inputdata[2]);
            mockCourseRepo.Setup(m => m.Read(4)).Returns(inputdata[3]);

            cl = new CourseLogic(mockCourseRepo.Object);
        }

        [Test]
        public void Create_Test()
        {
            //ARRANGE
            var sample = new Course()
            {
                CourseID = 5,
                StartTime = new DateTime(2023, 9, 7, 8, 0, 0),
                EndTime = new DateTime(2023, 9, 7, 9, 30, 0),
                CourseSubject = 1,
                Location = 1
            };

            //ACT
            cl.Create(sample);

            //ASSERT
            mockCourseRepo.Verify(m => m.Create(sample), Times.Once);
        }

        [Test]
        public void Create_ExceptionTest()
        {
            //ARRANGE
            var sample = new Course()
            {
                CourseID = 5,
                StartTime = new DateTime(2023, 9, 7, 0, 0, 0),
                EndTime = new DateTime(2023, 9, 4, 0, 0, 0),
                CourseSubject = 1,
                Location = 1
            };

            //ACT & ASSERT
            Assert.That(() => cl.Create(sample), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void CourseLengthMinutes_Test()
        {
            //ACT
            var result = cl.CourseLengthMinutes(1);

            //ASSERT
            Assert.That(result, Is.EqualTo(90));
        }

        [Test]
        public void AverageCourseLengthMinutes_Test()
        {
            //ACT
            var result = cl.AverageCourseLengthMinutes();

            //ASSERT
            Assert.That(result, Is.EqualTo(82.5));
        }

        [Test]
        public void MaxCourseLengthMinutes_Test()
        {
            //ACT
            var result = cl.MaxCourseLengthMinutes();

            //ASSERT
            Assert.That(result, Is.EqualTo(120));
        }

        [TestCase(1,2,true)]
        [TestCase(2,1,true)]
        [TestCase(3,4,false)]
        public void AreOverlapping_Test(int id1, int id2,bool expected)
        {
            //ACT
            var result = cl.AreOverlapping(id1,id2);

            //ASSERT
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void AnyOverlapping_Test()
        {
            //ACT
            var result = cl.AnyOverlapping();

            //ASSERT
            Assert.That(result, Is.EqualTo(true));
        }
    }
}
