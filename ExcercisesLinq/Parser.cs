using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExcercisesLinq
{
    static public class Parser
    {
        static public List<Customer> CreateListOfCustomers(string dataFileWithCustomers)
        {
            // Read file to a list 

            List<string> peopleRows = ReadFileToList(dataFileWithCustomers);

            // Remove first line

            RemoveFirstLine(peopleRows);

            // Create list of customers

            var customerList = new List<Customer>();

            foreach (var row in peopleRows)
            {
                
                if (string.IsNullOrWhiteSpace(row))
                {
                    continue;
                }

                Customer customer = ParseCustomerFromRow(row);
                
                customerList.Add(customer);
            }

            return customerList;
        }

        static private Customer ParseCustomerFromRow(string row)
        {
            string[] rowArray = row.Split(',');

            if (rowArray.Length < 5)
                throw new Exception("rowArray.Length < 5");

            var customer = new Customer
            #region constructor
                (
                int.Parse(rowArray[0]),
                rowArray[1],
                rowArray[2],
                rowArray[3],
                rowArray.Length >= 5 ? (Gender)Enum.Parse(typeof(Gender), rowArray[4]) : Gender.NULL,
                int.Parse(rowArray[5]),
                rowArray.Length >= 7 ? (PlayTennis)Enum.Parse(typeof(PlayTennis), rowArray[6]) : PlayTennis.NULL,
                rowArray.Length >= 8 ? (LikeFruit)Enum.Parse(typeof(LikeFruit), rowArray[7]) : LikeFruit.NULL,
                rowArray.Length >= 8 ? rowArray[8] : string.Empty
                );

            {
                
            };
            #endregion

            return customer;
        }

        static public List<string> CreateListOfNames(string dataFileWithCustomers)
        {
            // Read file to a list 

            List<string> peopleRows = ReadFileToList(dataFileWithCustomers);

            // Remove first line

            RemoveFirstLine(peopleRows);

            // Create list of names

            return CreateListOfNamesFromRows(peopleRows);

        }

        static private List<string> CreateListOfNamesFromRows(List<string> peopleRows)
        {
            var nameList = new List<string>();

            foreach (var row in peopleRows)
            {
                // Expect this format: Id,First name,Last name,Email,Gender,Age
                if (string.IsNullOrWhiteSpace(row))
                {
                    continue;
                }
                string[] rowArray = row.Split(',');

                string fullName = rowArray[1] + " " + rowArray[2];
                nameList.Add(fullName);
            }

            return nameList;
        }

        static private void RemoveFirstLine(List<string> peopleRows)
        {
            peopleRows.RemoveAt(0);
        }

        static private List<string> ReadFileToList(string dataFileWithCustomers)
        {
            //return File.ReadAllLines($"Linq\\Data\\{dataFileWithCustomers}").ToList();
            return File.ReadAllLines($"{dataFileWithCustomers}").ToList();
        }
    }
}
