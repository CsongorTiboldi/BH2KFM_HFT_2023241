using System;
using System.Linq;
using BH2KFM_HFT_2023241.Models;
using ConsoleTools;

namespace BH2KFM_HFT_2023241.Client
{
    internal class Program
    {
        static RestService rest;

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:60321/");

            //var subjectSubmenu = new ConsoleMenu(args, level: 1)
            //    .Add("List", () =>
            //    {
            //        subjectLogic.ReadAll().ToList().WriteToConsole();
            //        Console.ReadLine();
            //    })
            //    .Add("Exit", ConsoleMenu.Close);

            //var roomSubmenu = new ConsoleMenu(args, level: 1)
            //    .Add("List", () =>
            //    {
            //        roomLogic.ReadAll().ToList().WriteToConsole();
            //        Console.ReadLine();
            //    })
            //    .Add("Exit", ConsoleMenu.Close);

            //var courseSubmenu = new ConsoleMenu(args, level:1)
            //    .Add("List", () =>
            //    {
            //        courseLogic.ReadAll().ToList().WriteToConsole();
            //        Console.ReadLine();
            //    })
            //    .Add("Exit", ConsoleMenu.Close);

            //var mainMenu = new ConsoleMenu(args, level: 0)
            //    .Add("Subjects",() => subjectSubmenu.Show())
            //    .Add("Rooms",() => roomSubmenu.Show())
            //    .Add("Courses",() => courseSubmenu.Show())
            //    .Add("Exit", ConsoleMenu.Close);

            //mainMenu.Show();
        }
    }
}
