using BH2KFM_HFT_2023241.Models;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace BH2KFM_HFT_2023241.Logic
{
    public interface ICourseLogic
    {
        void Create(Course item);
        void Delete(int id);
        Course Read(int id);
        IEnumerable<Course> ReadAll();
        void Update(Course item);

        //non-CRUD methods:
        int CourseLengthMinutes(int courseId);
        double AverageCourseLengthMinutes();
        int MaxCourseLengthMinutes();
        bool AreOverlapping(int courseID_1, int courseID_2);
        bool AnyOverlapping();
    }
}