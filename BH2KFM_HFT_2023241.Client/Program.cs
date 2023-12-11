using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BH2KFM_HFT_2023241.Models;
using ConsoleTools;

namespace BH2KFM_HFT_2023241.Client
{
    internal class Program
    {
        public static RestService rest;

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:60321/");


            //Console submenu for subjects:
            #region subjectSubmenu
            var subjectSubmenu = new ConsoleMenu(args, level: 1)
                .Add("Create", action: GenericCRUD.Create<Subject>)
                .Add("Read", action: GenericCRUD.Read<Subject>)
                .Add("ReadAll", action: GenericCRUD.ReadAll<Subject>)
                .Add("Update", action: GenericCRUD.Update<Subject>)
                .Add("Delete", action: GenericCRUD.Delete<Subject>)
                .Add("AverageCreditValue", action: () =>
                {
                    try
                    {
                        Console.WriteLine("Average credit value of currently stored subjects: " + rest.GetSingle<double>("SubjectStat/AverageCreditValue"));
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("SubjectsInSemester", action: () =>
                {
                    Console.Write("Which semeseter: ");
                    try
                    {
                        int semester = int.Parse(Console.ReadLine());
                        Console.WriteLine("Subjects in this semester:");
                        rest.Get<Subject>($"SubjectStat/SubjectsInSemester/{semester}").WriteToConsole();
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("IsSubjectInAnyProjectorRoom", action: () =>
                {
                    Console.Write("Which subject (ID): ");
                    try
                    {
                        int id = int.Parse(Console.ReadLine());
                        if (rest.GetSingle<bool>($"SubjectStat/IsSubjectInAnyProjectorRoom/{id}"))
                        {
                            Console.WriteLine("This subject has at least one course associated with it that is taking place in a room with a projector.");
                        }
                        else
                        {
                            Console.WriteLine("This subject is not associated with any course taking place in a room with a projector.");
                        }
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("Rooms", action: () =>
                {
                    Console.Write("Which subject (ID): ");
                    try
                    {
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("This subject is taught in the following rooms:");
                        rest.Get<Room>($"SubjectStat/Rooms/{id}").WriteToConsole();
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("MostCreditSemester", action: () =>
                {
                    try
                    {
                        Console.WriteLine("Number of the semester with the largest amount of credits: " + rest.GetSingle<int>("SubjectStat/MostCreditSemester"));
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("< Back", action: ConsoleMenu.Close);
            #endregion subjectSubmenu


            //Console submenu for rooms:
            #region roomSubmenu
            var roomSubmenu = new ConsoleMenu(args, level: 1)
                .Add("Create", action: GenericCRUD.Create<Room>)
                .Add("Read", action: GenericCRUD.Read<Room>)
                .Add("ReadAll", action: GenericCRUD.ReadAll<Room>)
                .Add("Update", action: GenericCRUD.Update<Room>)
                .Add("Delete", action: GenericCRUD.Delete<Room>)
                .Add("ProjectorRooms", action: () => 
                {
                    Console.WriteLine("Rooms with projectors:");
                    try
                    {
                        rest.Get<Room>("RoomStat/ProjectorRooms").WriteToConsole();
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("MaxCapacity", action: () => 
                {
                    try
                    {
                        Console.WriteLine("The capacity of the largest room is " + rest.GetSingle<int>("RoomStat/MaxCapacity"));
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("AverageSubjectCreditInRoom", action: () =>
                {
                    Console.Write("Which room (ID): ");
                    try
                    {
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Average credit value of subjects taking place in this room: " + rest.GetSingle<double>($"RoomStat/AverageSubjectCreditInRoom/{id}"));
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("MaxSubjectSemesterInRoom", action: () =>
                {
                    Console.Write("Which room (ID): ");
                    try
                    {
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Maximum semester number among subjects taking place in this room: " + rest.GetSingle<int>($"RoomStat/MaxSubjectSemesterInRoom/{id}"));
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("Subjects", action: () => 
                {
                    Console.Write("Which room (ID): ");
                    try
                    {
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Subjects taught in this room:");
                        rest.Get<Subject>($"RoomStat/Subjects/{id}").WriteToConsole();
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("< Back", action: ConsoleMenu.Close);
            #endregion roomSubmenu


            //Console submenu for courses:
            #region courseSubmenu
            var courseSubmenu = new ConsoleMenu(args, level: 1)
                .Add("Create", action: GenericCRUD.Create<Course>)
                .Add("Read", action: GenericCRUD.Read<Course>)
                .Add("ReadAll", action: GenericCRUD.ReadAll<Course>)
                .Add("Update", action: GenericCRUD.Update<Course>)
                .Add("Delete", action: GenericCRUD.Delete<Course>)
                .Add("CourseLengthMinutes", action: () => 
                {
                    Console.Write("Which course (ID): ");
                    try
                    {
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("This course is " + rest.GetSingle<int>($"CourseStat/CourseLengthMinutes/{id}" + " minutes long."));
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("AverageCourseLengthMinutes", action: () => 
                {
                    try
                    {
                        Console.WriteLine("A course is " + rest.GetSingle<double>("CourseStat/AverageCourseLengthMinutes") + " minutes long on average.");
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("MaxCourseLengthMinutes", action: () => 
                {
                    try
                    {
                        Console.WriteLine("The longest course is " + rest.GetSingle<int>("CourseStat/MaxCourseLengthMinutes") + " minutes long.");
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("AreOverlapping", action: () => 
                {
                    try
                    {
                        Console.Write("First course (ID): ");
                        int id1 = int.Parse(Console.ReadLine());
                        Console.Write("Second course (ID): ");
                        int id2 = int.Parse(Console.ReadLine());

                        if (rest.GetSingle<bool>($"CourseStat/AreOverlapping?course1={id1}&course2={id2}"))
                        {
                            Console.WriteLine("These two courses are overlapping!");
                        }
                        else
                        {
                            Console.WriteLine("These two courses aren't overlapping!");
                        }
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("AnyOverlapping", action: () => 
                {
                    try
                    {
                        if (rest.GetSingle<bool>($"CourseStat/AnyOverlapping"))
                        {
                            Console.WriteLine("There are overlapping courses!");
                        }
                        else
                        {
                            Console.WriteLine("There aren't any overlapping courses!");
                        }
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("< Back", action: ConsoleMenu.Close);
            #endregion courseSubmenu


            //Main console menu:
            #region mainMenu
            var mainMenu = new ConsoleMenu(args, level: 0)
                .Add("Subjects", action: () => subjectSubmenu.Show())
                .Add("Rooms", action: () => roomSubmenu.Show())
                .Add("Courses", action: () => courseSubmenu.Show())
                .Add("Exit", action: ConsoleMenu.Close);

            mainMenu.Show();
            #endregion mainMenu
        }

        public static void ConsoleContinuation()
        {
            WriteWithColor("\r\nPress enter to return to menu...", ConsoleColor.Blue);
            Console.ReadLine();
        }

        public static void WriteWithColor(string line, ConsoleColor color)
        {
            ConsoleColor col = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(line);
            Console.ForegroundColor = col;
        }
    }
}
