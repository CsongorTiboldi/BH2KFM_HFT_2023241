using BH2KFM_HFT_2023241.Repository;
using BH2KFM_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH2KFM_HFT_2023241.Logic
{
    public class CourseLogic : ICourseLogic
    {
        IRepository<Course> repository;

        public CourseLogic(IRepository<Course> repository)
        {
            this.repository = repository;
        }

        public void Create(Course item)
        {
            if (item.EndTime < item.StartTime)
            {
                throw new ArgumentException("Course cannot end before it even started");
            }

            repository.Create(item);
        }

        public void Delete(int id)
        {
            Read(id);

            repository.Delete(id);
        }

        public Course Read(int id)
        {
            var item = repository.Read(id);
            if (item is null)
            {
                throw new ArgumentException("Course does not exist");
            }

            return item;
        }

        public IEnumerable<Course> ReadAll()
        {
            return repository.ReadAll();
        }

        public void Update(Course item)
        {
            repository.Update(item);
        }

        //non-CRUD methods:

        public int CourseLengthMinutes(int courseId)
        {
            var course = this.Read(courseId);
            return (int) (course.EndTime - course.StartTime).TotalMinutes;
        }

        public double AverageCourseLengthMinutes()
        {
            return this.ReadAll().Average(t => (t.EndTime - t.StartTime).TotalMinutes);
        }

        public int MaxCourseLengthMinutes()
        {
            return (int) this.ReadAll().Max(t => (t.EndTime - t.StartTime).TotalMinutes);
        }

        public bool AreOverlapping(int courseID_1, int courseID_2)
        {
            var course1 = this.Read(courseID_1);
            var course2 = this.Read(courseID_2);

            return
                course1.Location == course2.Location &&
                course1.StartTime < course2.EndTime &&
                course2.StartTime < course1.EndTime;
        }

        public bool AnyOverlapping()
        {
            var courses = this.ReadAll();
            
            foreach (var i in courses)
            {
                foreach (var j in courses)
                {
                    if (!i.Equals(j) && i.Location == j.Location && i.StartTime < j.EndTime && j.StartTime < i.EndTime)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
