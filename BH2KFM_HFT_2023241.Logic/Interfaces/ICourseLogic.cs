using BH2KFM_HFT_2023241.Models;
using System.Collections.Generic;

namespace BH2KFM_HFT_2023241.Logic
{
    public interface ICourseLogic
    {
        void Create(Course item);
        void Delete(int id);
        Course Read(int id);
        IEnumerable<Course> ReadAll();
        void Update(Course item);
    }
}