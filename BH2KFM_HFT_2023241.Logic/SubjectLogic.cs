using BH2KFM_HFT_2023241.Repository;
using BH2KFM_HFT_2023241.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BH2KFM_HFT_2023241.Logic
{
    public class SubjectLogic
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
            repository.Update(item);
        }
    }
}
