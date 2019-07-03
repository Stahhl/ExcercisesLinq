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
            //Module_Search_Name(dataFiles[1]);
            Module_Search_Enum(dataFiles[1]);
        }
        static void Module_Search_Enum(string fileName)
        {
            List<Customer> customerList = Parser.CreateListOfCustomers(fileName);
            List<Customer> hits = new List<Customer>();

            string[] responseGenderOptions = new string[]
            {
                "Men",
                "Women",
                "Other",
                "Everybody"
            };
            string[] responsePlayTennisOptions = new string[]
            {
                "that never play tennis",
                "that tried tennis once",
                "that seldom play tennis",
                "that play tennis once a year",
                "that play tennis once a moth",
                "that play tennis once a week",
                "that play tennis every day",
                "that play tennis often",
                "that play tennis any amount of time"
            };
            string[] responseLikeFruitOptions = new string[]
            {
                "and like fruit",
                "but don't like fruit",
                "and have no opinion on fruit"
            };

            string responseGender = string.Empty;
            string responsePlayTennis = string.Empty;
            string responseLikeFruit = string.Empty;

            Gender gender = Gender.NULL;
            PlayTennis playTennis = PlayTennis.NULL;
            LikeFruit likeFruit = LikeFruit.NULL;

            Console.Write("Gender: (M)ale (F)emale (O)ther (I)gnore: ");
            switch (Console.ReadLine().ToUpper())
            {
                case "M":
                    gender = Gender.Male;
                    responseGender = responseGenderOptions[0];
                    break;
                case "F":
                    gender = Gender.Female;
                    responseGender = responseGenderOptions[1];
                    break;
                case "O":
                    gender = Gender.Other;
                    responseGender = responseGenderOptions[2];
                    break;
                default:
                    gender = Gender.NULL;
                    responseGender = responseGenderOptions[3];
                    break;
            }
            Console.Write("PlayTennis: (N)ever (O)nce (S)eldom (Y)early (M)onthly (W)eekly (D)aily (Of)ten (I)gnore: ");
            switch (Console.ReadLine().ToUpper())
            {
                case "N":
                    playTennis = PlayTennis.Never;
                    responsePlayTennis = responsePlayTennisOptions[0];
                    break;
                case "O":
                    playTennis = PlayTennis.Once;
                    responsePlayTennis = responsePlayTennisOptions[1];
                    break;
                case "S":
                    playTennis = PlayTennis.Seldom;
                    responsePlayTennis = responsePlayTennisOptions[2];
                    break;
                case "Y":
                    playTennis = PlayTennis.Yearly;
                    responsePlayTennis = responsePlayTennisOptions[3];
                    break;
                case "M":
                    playTennis = PlayTennis.Monthly;
                    responsePlayTennis = responsePlayTennisOptions[4];
                    break;
                case "W":
                    playTennis = PlayTennis.Weekly;
                    responsePlayTennis = responsePlayTennisOptions[5];
                    break;
                case "D":
                    playTennis = PlayTennis.Daily;
                    responsePlayTennis = responsePlayTennisOptions[6];
                    break;
                case "OF":
                    playTennis = PlayTennis.Often;
                    responsePlayTennis = responsePlayTennisOptions[7];
                    break;
                default:
                    playTennis = PlayTennis.NULL;
                    responsePlayTennis = responsePlayTennisOptions[8];
                    break;
            }
            Console.Write("LikeFruit: (T)rue (F)alse (I)gnore: ");
            switch (Console.ReadLine().ToUpper())
            {
                case "T":
                    likeFruit = LikeFruit.True;
                    responseLikeFruit = responseLikeFruitOptions[0];
                    break;
                case "F":
                    likeFruit = LikeFruit.False;
                    responseLikeFruit = responseLikeFruitOptions[1];
                    break;
                default:
                    likeFruit = LikeFruit.NULL;
                    responseLikeFruit = responseLikeFruitOptions[2];
                    break;
            }

            try
            {
                hits = (from c in customerList
                        where (c.Gender == gender || gender == Gender.NULL)
                        where (c.PlayTennis == playTennis || playTennis == PlayTennis.NULL)
                        where (c.LikeFruit == likeFruit || likeFruit == LikeFruit.NULL)
                        select c).ToList();
            }
            catch
            {
                throw new Exception("Error - Search Enum");
            }
            PrintMsg($"{responseGender} {responsePlayTennis} {responseLikeFruit}.", ConsoleColor.White);
            PrintMsg($"Number of hits: {hits.Count()}", ConsoleColor.White);
            foreach (var item in hits)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(item.FullName.PadRight(25) + "\t");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(item.Gender + "\t");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(item.PlayTennis + "\t");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(item.LikeFruit + "\t");
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Press RETUR to restart: ");
            Console.ReadLine();
            Main();
        }
        static void Module_Search_Text(string fileName)
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
                    customer.FullName.ToUpper().Contains(input) ||
                    customer.Email.ToUpper().Contains(input)
                    ).ToList();
            }
            catch
            {
                throw new Exception("Error - Search Text");
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

            bool[] boolArray = new bool[msg.Length];

            try
            {
                for (int i = 0; i < msg.Length - (input.Length - 1); i++)
                {
                    string sub = msg.Substring(i, input.Length);

                    if (sub.ToUpper() == input)
                    {
                        for (int x = i; x < (i + sub.Length); x++)
                        {
                            boolArray[x] = true;
                        }
                    }
                }
            }
            catch
            {
                throw new Exception("Error - HighlightLetters");
            }

            for (int i = 0; i < msg.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                if (boolArray[i] == true)
                    Console.ForegroundColor = ConsoleColor.Green;

                Console.Write(msg[i]);
                Console.ResetColor();
            }
            Console.Write("".PadRight(35 - msg.Length) + "\t");
        }
    }
}
