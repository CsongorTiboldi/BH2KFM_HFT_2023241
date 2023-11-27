using BH2KFM_HFT_2023241.Models;
using System.Collections.Generic;

namespace BH2KFM_HFT_2023241.Logic
{
    public interface ISubjectLogic
    {
        void Create(Subject item);
        void Delete(int id);
        Subject Read(int id);
        IEnumerable<Subject> ReadAll();
        void Update(Subject item);

        //non-CRUD methods:
        double AverageCreditValue();
        IEnumerable<Subject> SubjectsInSemester(int semesterNumber);
        IEnumerable<Subject> SubjectsWithCreditValue(int creditValue);
        IEnumerable<Room> Rooms(int subjectId);
        int MostCreditSemester();
    }
}