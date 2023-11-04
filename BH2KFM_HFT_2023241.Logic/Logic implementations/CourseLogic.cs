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
    }
}
