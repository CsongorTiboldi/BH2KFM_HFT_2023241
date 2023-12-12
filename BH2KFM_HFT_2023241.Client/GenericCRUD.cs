using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BH2KFM_HFT_2023241.Client
{
    internal class GenericCRUD
    {
        public static void Create<T>() where T : class
        {
            string model = typeof(T).Name;
            try
            {
                T item = ModelFromConsole<T>();
                Program.rest.Post<T>(item, model);
                Console.WriteLine($"New {model} created successfully!");
            }
            catch (Exception e)
            {
                Program.WriteWithColor(e.Message, ConsoleColor.Red);
            }
            Program.ConsoleContinuation();
        }

        public static void Read<T>() where T : class
        {
            string model = typeof(T).Name;
            Console.Write($"ID of {model} to read: ");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine(Program.rest.GetSingle<T>($"{model}/{id}"));
            }
            catch (Exception e)
            {
                Program.WriteWithColor(e.Message, ConsoleColor.Red);
            }
            Program.ConsoleContinuation();
        }

        public static void ReadAll<T>() where T : class
        {
            string model = typeof(T).Name;
            Console.WriteLine($"List of every {model}:");
            try
            {
                Program.rest.Get<T>(model).WriteToConsole();
            }
            catch (Exception e)
            {
                Program.WriteWithColor(e.Message, ConsoleColor.Red);
            }
            Program.ConsoleContinuation();
        }

        public static void Update<T>() where T : class
        {
            string model = typeof(T).Name;
            try
            {
                T item = ModelFromConsole<T>();
                Program.rest.Put<T>(item, model); ;
                Console.WriteLine($"{model} updated successfully!");
            }
            catch (Exception e)
            {
                Program.WriteWithColor(e.Message, ConsoleColor.Red);
            }
            Program.ConsoleContinuation();
        }

        public static void Delete<T>() where T : class
        {
            string model = typeof(T).Name;
            Console.Write($"ID of {model} to delete: ");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Program.rest.Delete(id, model);
                Console.WriteLine($"{model} deleted successfully!");
            }
            catch (Exception e)
            {
                Program.WriteWithColor(e.Message, ConsoleColor.Red);
            }
            Program.ConsoleContinuation();
        }

        static T ModelFromConsole<T>() where T : class
        {
            Console.WriteLine($"Reading {typeof(T).Name} from console...\r\nFill out the required fields with appropriate data!\r\nUse ddd HH:mm format for time fields (i.e. fri 14:30 -> Friday, 2:30 PM)\r\n");

            T item = (T)Activator.CreateInstance(typeof(T), null);

            foreach (var prop in item.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    Console.Write(prop.Name + ": ");
                    string line = Console.ReadLine();

                    if (line.Equals(""))
                    {
                        throw new ArgumentNullException($"The value of {prop.Name} cannot be null");
                    }

                    if (prop.PropertyType == typeof(string))
                    {
                        prop.SetValue(item, line);
                    }
                    else if (prop.PropertyType == typeof(DateTime))
                    {
                        string[] dayLine = line.Split(' ');

                        string day = dayLine[0] switch
                        {
                            "mon" => "2023/09/04",
                            "tue" => "2023/09/05",
                            "wed" => "2023/09/06",
                            "thu" => "2023/09/07",
                            "fri" => "2023/09/08",
                            _ => ""
                        };

                        string time = dayLine[1];
                        //int hour = int.Parse(dayLine[1].Split(':')[0]);

                        //int minute = int.Parse(dayLine[1].Split(':')[1]);

                        DateTime date = DateTime.ParseExact($"{day} {time}", "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);

                        prop.SetValue(item, date);
                    }
                    else
                    {
                        var parseMethod = prop.PropertyType.GetMethods().First(t => t.Name.Contains("Parse"));
                        var converted = parseMethod.Invoke(null, new object[] { line });
                        prop.SetValue(item, converted);
                    }
                }
            }

            return item;
        }
    }
}
