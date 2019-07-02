using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcercisesLinq
{
    class Program
    {
        static void Main()
        {
            string[] dataFiles = new string[] { "PersonShort.csv", "PersonExtra.csv" };

            Console.Clear();
            Console.ResetColor();

            //Module_1();
            //Module_2();
            //Module_3(file);
            //Module_4(file);
            Module_Search_Name(dataFiles[1]);
        }
        static void Module_Search_Name(string fileName)
        {
            List<Customer> customerList = Parser.CreateListOfCustomers(fileName);
            List<Customer> hits = new List<Customer>();
            Console.Write("Search customer by: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string input = Console.ReadLine().ToUpper();
            if (input == string.Empty)
                Main();

            Console.ResetColor();

            try
            {
                hits = customerList.Where(customer =>
                    customer.FirstName.ToUpper().Contains(input) ||
                    customer.LastName.ToUpper().Contains(input) ||
                    customer.FullName.ToUpper().Contains(input) ||
                    customer.Email.ToUpper().Contains(input)
                    ).ToList();
            }
            catch
            {
                Console.WriteLine("Error - Search");
            }

            PrintMsg($"Number of hits: {hits.Count()}", ConsoleColor.White);
            foreach (var item in hits)
            {
                HighlightLetters(item.FullName, input, ConsoleColor.Green);
                HighlightLetters(item.Email, input, ConsoleColor.Green);
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Press RETUR to restart: ");
            Console.ReadLine();
            Main();
        }
        static void Module_4(string fileName)
        {
            List<Customer> customerList = Parser.CreateListOfCustomers(fileName);
            var peopleThatLikeFruitAndPlayTennisDaily = (from c in customerList
                                                         where (c.PlayTennis == PlayTennis.Daily)
                                                         where (c.LikeFruit == LikeFruit.True)
                                                         select c).ToList();

            PrintMsg("Result: ", ConsoleColor.White);
            foreach (var item in peopleThatLikeFruitAndPlayTennisDaily)
            {
                PrintMsg($"{item.FirstName}     \t {item.LikeFruit} \t {item.PlayTennis}", ConsoleColor.DarkGray);
            }
            Console.ReadLine();
            Main();
        }
        static void Module_3(string fileName)
        {
            List<Customer> customerList = Parser.CreateListOfCustomers(fileName);
            var testList = customerList.Where(c => 
               c.Age > 75 && 
               c.Gender == Gender.Male &&
               c.FirstName[0] == 'A').ToList();

            PrintMsg("Result: ", ConsoleColor.White);
            foreach (var item in testList)
            {
                PrintMsg($"{item.FirstName}     \t {item.Age} \t {item.Gender}", ConsoleColor.DarkGray);
            }
            Console.ReadLine();
            Main();
        }
        static void Module_2(string fileName)
        {
            List<Customer> customerList = Parser.CreateListOfCustomers(fileName);
            var byAge = customerList.OrderBy(x => x.Age).ToList();
            var byFirstName = customerList.OrderBy(x => x.FirstName).ToList();
            var menOlderThan35 = (from c in customerList
                                 where (c.Age >= 35)
                                 where (c.Gender == Gender.Male)
                                 orderby(c.FirstName)
                                 select c).ToList();

            PrintMsg("Sorted list by age: ", ConsoleColor.White);
            foreach (var item in byAge)
            {
                PrintMsg($"{item.FirstName}     \t {item.Age} \t {item.Gender}", ConsoleColor.DarkGray);
            }
            PrintMsg("Sorted list by first name: ", ConsoleColor.White);
            foreach (var item in byFirstName)
            {
                PrintMsg($"{item.FirstName}     \t {item.Age} \t {item.Gender}", ConsoleColor.DarkGray);
            }
            PrintMsg("Men older than 35: ", ConsoleColor.White);
            foreach (var item in menOlderThan35)
            {
                PrintMsg($"{item.FirstName}     \t {item.Age} \t {item.Gender}", ConsoleColor.DarkGray);
            }
            PrintMsg("Manipulated: ", ConsoleColor.White);
            foreach (var item in menOlderThan35)
            {
                PrintMsg($"{item.FirstName.ToUpper()}     \t {item.Age + 1000} \t {item.Gender}", ConsoleColor.DarkGray);
            }
            Console.ReadLine();
            Main();
        }
        static void Module_1(string fileName)
        {
            List<string> nameList = Parser.CreateListOfNames(fileName);
            nameList.Sort();
            var startsWithJ = nameList.Where(name => Char.ToUpper(name[0]) == 'J').ToList();

            PrintMsg("Sorted List: ", ConsoleColor.White);
            foreach (string name in nameList)
            {
                PrintMsg(name, ConsoleColor.DarkGray);
            }
            PrintMsg("Starts with 'J': ", ConsoleColor.White);
            foreach (string name in startsWithJ)
            {
                PrintMsg(name, ConsoleColor.DarkGray);
            }
            PrintMsg("Starts with 'J' and uppercase: ", ConsoleColor.White);
            foreach (string name in startsWithJ)
            {
                PrintMsg(name.ToUpper(), ConsoleColor.DarkGray);
            }
            Console.ReadLine();
            Main();
        }
        static void PrintMsg(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
        static void HighlightLetters(string msg, string input, ConsoleColor color)
        {
            if (input.Length > msg.Length)
                return;

            List<KeyValuePair<string, bool>> kvpList = new List<KeyValuePair<string, bool>>();

            foreach (char c in msg)
            {
                kvpList.Add(new KeyValuePair<string, bool>(c.ToString(), false));
            }
            try
            {
                for (int i = 0; i < msg.Length - (input.Length - 1); i++)
                {
                    string sub = msg.Substring(i, input.Length);

                    if (sub.ToUpper() == input)
                    {
                        for (int x = i; x < (i + sub.Length); x++)
                        {
                            string key = kvpList[x].Key;

                            kvpList[x] = new KeyValuePair<string, bool>(key, true);
                        }
                    }
                }
            }
            catch
            {
                throw new Exception("Error - HighlightLetters");
            }

            for (int i = 0; i < kvpList.Count(); i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                if (kvpList[i].Value == true)
                    Console.ForegroundColor = ConsoleColor.Green;

                Console.Write(kvpList[i].Key);
                Console.ResetColor();
            }
            Console.Write("".PadRight(35 - msg.Length) + "\t");
        }
    }
}
