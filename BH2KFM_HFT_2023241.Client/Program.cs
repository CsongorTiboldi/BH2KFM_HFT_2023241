using System;
using System.Linq;
using BH2KFM_HFT_2023241.Logic;
using BH2KFM_HFT_2023241.Repository;
using BH2KFM_HFT_2023241.Models;
using ConsoleTools;

namespace BH2KFM_HFT_2023241.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {

            LectureDbContext ctx = new LectureDbContext();
            IRepository<Subject> subjectRepo= new SubjectRepository(ctx);
            IRepository<Room> roomRepo = new RoomRepository(ctx);
            IRepository<Course> courseRepo = new CourseRepository(ctx);

            ISubjectLogic subjectLogic = new SubjectLogic(subjectRepo);
            IRoomLogic roomLogic = new RoomLogic(roomRepo);
            ICourseLogic courseLogic = new CourseLogic(courseRepo);


            var subjectSubmenu = new ConsoleMenu(args, level: 1)
                .Add("List", () =>
                {
                    subjectLogic.ReadAll().ToList().ForEach(t => Console.WriteLine(t));
                    Console.ReadLine();
                })
                .Add("Exit", ConsoleMenu.Close);

            var roomSubmenu = new ConsoleMenu(args, level: 1)
                .Add("List", () =>
                {
                    roomLogic.ReadAll().ToList().ForEach(t => Console.WriteLine(t));
                    Console.ReadLine();
                })
                .Add("Exit", ConsoleMenu.Close);

            var courseSubmenu = new ConsoleMenu(args, level:1)
                .Add("List", () =>
                {
                    courseLogic.ReadAll().ToList().ForEach(t => Console.WriteLine(t));
                    Console.ReadLine();
                })
                .Add("Exit", ConsoleMenu.Close);

            var mainMenu = new ConsoleMenu(args, level: 0)
                .Add("Subjects",() => subjectSubmenu.Show())
                .Add("Rooms",() => roomSubmenu.Show())
                .Add("Courses",() => courseSubmenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            mainMenu.Show();
        }
    }
}
