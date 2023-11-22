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
                new Subject(){SubjectID = 4, Name = "Embeded systems", Credits = 6, Semester = 3 },
            };

            mockSubjectRepo = new Mock<IRepository<Subject>>();
            mockSubjectRepo.Setup(m => m.ReadAll()).Returns(inputdata.AsQueryable());
            sl = new SubjectLogic(mockSubjectRepo.Object);
        }

        [Test]
        public void CreateTest()
        {
            var sample = new Subject()
            {
                SubjectID = 5,
                Name = "Thermodynamics",
                Credits = 6,
                Semester = 3
            };

            sl.Create(sample);
            mockSubjectRepo.Verify(m => m.Create(sample), Times.Once);
        }
    }
}
