using BH2KFM_HFT_2023241.Repository;
using BH2KFM_HFT_2023241.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BH2KFM_HFT_2023241.Logic
{
    public class SubjectLogic : ISubjectLogic
    {
        IRepository<Subject> repository;

        public SubjectLogic(IRepository<Subject> repository)
        {
            this.repository = repository;
        }

        public void Create(Subject item)
        {
            if (item.Credits < 0 || item.Credits > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(item), "Credit value must not fall outside the range between 0 and 10");
            }

            repository.Create(item);
        }

        public void Delete(int id)
        {
            Read(id);

            repository.Delete(id);
        }

        public Subject Read(int id)
        {
            var item = repository.Read(id);
            if (item is null)
            {
                throw new ArgumentException("Subject does not exist");
            }

            return item;
        }

        public IEnumerable<Subject> ReadAll()
        {
            return repository.ReadAll();
        }

        public void Update(Subject item)
        {
            this.Read(item.SubjectID);

            if (item.Credits < 0 || item.Credits > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(item), "Credit value must not fall outside the range between 0 and 10");
            }

            repository.Update(item);
        }

        //non-CRUD methods:

        public double AverageCreditValue()
        {
            return this.ReadAll().Average(t => t.Credits);
        }

        public IEnumerable<Subject> SubjectsInSemester(int semesterNumber)
        {
            return this.ReadAll().Where(t => t.Semester == semesterNumber);
        }

        public IEnumerable<Subject> SubjectsWithCreditValue(int creditValue)
        {
            return this.ReadAll().Where(t => t.Credits == creditValue);
        }

        public IEnumerable<Room> Rooms(int subjectId)
        {
            return this.Read(subjectId).Courses.Select(t => t.Room).Distinct();
        }

        public int MostCreditSemester()
        {
            var res =
            (
                from item in this.ReadAll()
                group item by item.Semester into grp
                orderby grp.Sum(t => t.Credits) descending
                select grp.Key
            ).Take(1);

            return res.ElementAt(0);
        }
    }
}
