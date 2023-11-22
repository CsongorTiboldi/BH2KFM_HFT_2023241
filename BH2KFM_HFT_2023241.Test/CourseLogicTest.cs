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
        Mock<IRepository<Course>> mockCoursetRepo;
        CourseLogic cl;

        [SetUp]
        public void Init()
        {
            var inputdata = new List<Course>()
            {
                new Course(){ CourseID = 1, StartTime = new DateTime(2023,9,4,8,30,0), EndTime = new DateTime(2023,9,4,10,0,0), CourseSubject = 1, Location = 1},
                new Course(){ CourseID = 2, StartTime = new DateTime(2023,9,4,8,30,0), EndTime = new DateTime(2023,9,4,10,0,0), CourseSubject = 2, Location = 1},
                new Course(){ CourseID = 3, StartTime = new DateTime(2023,9,5,8,30,0), EndTime = new DateTime(2023,9,4,10,0,0), CourseSubject = 1, Location = 1},
                new Course(){ CourseID = 4, StartTime = new DateTime(2023,9,6,8,30,0), EndTime = new DateTime(2023,9,4,11,0,0), CourseSubject = 1, Location = 1},
            };

            mockCoursetRepo = new Mock<IRepository<Course>>();
            mockCoursetRepo.Setup(m => m.ReadAll()).Returns(inputdata.AsQueryable());
            cl = new CourseLogic(mockCoursetRepo.Object);
        }

        [Test]
        public void CreateTest()
        {
            var sample = new Course()
            {
                CourseID = 5,
                StartTime = new DateTime(2023, 9, 4, 8, 0, 0),
                EndTime = new DateTime(2023, 9, 4, 9, 30, 0),
                CourseSubject = 1,
                Location = 1
            };

            cl.Create(sample);
            mockCoursetRepo.Verify(m => m.Create(sample), Times.Once);
        }
    }
}
