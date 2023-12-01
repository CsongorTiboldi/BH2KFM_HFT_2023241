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


            var subjectSubmenu = new ConsoleMenu(args, level: 1)
                .Add("Create", action: GenericCRUD.Create<Subject>)
                .Add("Read", action: GenericCRUD.Read<Subject>)
                .Add("ReadAll", action: GenericCRUD.ReadAll<Subject>)
                .Add("Update", action: GenericCRUD.Update<Subject>)
                .Add("Delete", action: GenericCRUD.Delete<Subject>)
                .Add("AverageCreditValue", () =>
                {
                    try
                    {
                        Console.WriteLine("Average credit value of a subject: " + rest.GetSingle<double>("SubjectStat/AverageCreditValue"));
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("SubjectsInSemester", () =>
                {
                    Console.Write("Which semeseter: ");
                    int semester = int.Parse(Console.ReadLine());
                    try
                    {
                        rest.Get<Subject>($"SubjectStat/SubjectsInSemester/{semester}").WriteToConsole();
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("SubjectsWithCreditValue", () =>
                {
                    Console.Write("Credits: ");
                    int credits = int.Parse(Console.ReadLine());
                    try
                    {
                        rest.Get<Subject>($"SubjectStat/SubjectsWithCreditValue/{credits}").WriteToConsole();
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("Rooms", () =>
                {
                    Console.Write("Which subject: ");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        rest.Get<Room>($"SubjectStat/Rooms/{id}").WriteToConsole();
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("MostCreditSemester", () =>
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
                .Add("< Back", ConsoleMenu.Close);


            var roomSubmenu = new ConsoleMenu(args, level: 1)
                .Add("Create", action: GenericCRUD.Create<Room>)
                .Add("Read", action: GenericCRUD.Read<Room>)
                .Add("ReadAll", action: GenericCRUD.ReadAll<Room>)
                .Add("Update", action: GenericCRUD.Update<Room>)
                .Add("Delete", action: GenericCRUD.Delete<Room>)
                .Add("ProjectorRooms", () => 
                {
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
                .Add("MaxCapacity", () => 
                {
                    try
                    {
                        Console.WriteLine("The maximum room capacity: " + rest.GetSingle<int>("RoomStat/MaxCapacity"));
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("LargestCapacityRooms", () => 
                {
                    Console.WriteLine("Rooms with largest capacity:");
                    try
                    {
                        rest.Get<Room>("RoomStat/LargestCapacityRooms").WriteToConsole();
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("AverageCapacity", () => 
                {
                    try
                    {
                        Console.WriteLine("Average room capacity: " + rest.GetSingle<double>("RoomStat/AverageCapacity"));
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("Subjects", () => 
                {
                    Console.Write("Which room: ");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        rest.Get<Subject>($"RoomStat/Subjects/{id}").WriteToConsole();
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("< Back", ConsoleMenu.Close);


            var courseSubmenu = new ConsoleMenu(args, level: 1)
                .Add("Create", action: GenericCRUD.Create<Course>)
                .Add("Read", action: GenericCRUD.Read<Course>)
                .Add("ReadAll", action: GenericCRUD.ReadAll<Course>)
                .Add("Update", action: GenericCRUD.Update<Course>)
                .Add("Delete", action: GenericCRUD.Delete<Course>)
                .Add("CourseLengthMinutes", () => 
                {
                    Console.Write("Which course: ");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine("Length of this course: " + rest.GetSingle<int>($"CourseStat/CourseLengthMinutes/{id}"));
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("AverageCourseLengthMinutes", () => 
                {
                    try
                    {
                        Console.WriteLine("Average length of a course in minutes: " + rest.GetSingle<double>("CourseStat/AverageCourseLengthMinutes"));
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("MaxCourseLengthMinutes", () => 
                {
                    try
                    {
                        Console.WriteLine("Lenght of the longest course in minutes: " + rest.GetSingle<double>("CourseStat/MaxCourseLengthMinutes"));
                    }
                    catch (Exception e)
                    {
                        WriteWithColor(e.Message, ConsoleColor.Red);
                    }
                    ConsoleContinuation();
                })
                .Add("AreOverlapping", () => 
                {
                    Console.Write("First course: ");
                    int id1 = int.Parse(Console.ReadLine());
                    Console.Write("Second course: ");
                    int id2 = int.Parse(Console.ReadLine());

                    try
                    {
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
                .Add("AnyOverlapping", () => 
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
                .Add("< Back", ConsoleMenu.Close);


            var mainMenu = new ConsoleMenu(args, level: 0)
                .Add("Subjects", () => subjectSubmenu.Show())
                .Add("Rooms", () => roomSubmenu.Show())
                .Add("Courses", () => courseSubmenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            mainMenu.Show();
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
