using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace CinemaConsoleApplication
{

    public class Account
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public int Age { get; set; }
        public int Level { get; set; }
        //public List<Review> UserReviewList { get; set; }
        public string AccountID;

        public Account()
        {
            FirstName = "";
            LastName = "";
            Gender = "";
            EMail = "";
            Password = "";
            Address = "";
            ZipCode = "";
            City = "";
            Age = 0;
            Level = 0;
            AccountID = "";
        }
        public string GetRoughInfo()
        {
            string lvlString = Level == 1 ? "User " : "Admin";
            return $"{lvlString} | {FirstName} {LastName}";
        }

        public void GetDetailedInfo()
        {
            Console.WriteLine($" 1> First name: {FirstName}");
            Console.WriteLine($" 2> Last name : {LastName}");
            Console.WriteLine($" 3> Gender    : {Gender}");
            Console.WriteLine($" 4> Mail      : {EMail}");
            Console.WriteLine($" 5> Password  : {Password}");
            Console.WriteLine($" 6> Address    : {Address}");
            Console.WriteLine($" 7> Zipcode   : {ZipCode}");
            Console.WriteLine($" 8> City      : {City}");
            Console.WriteLine($" 9> Age       : {Age}");
            Console.WriteLine($"10> Level     : {Level}");
        }

    }

    public class Manager
    {
        public static string jsonpath = Path.GetFullPath(@"accounts.json");

        public static void createAccount(string firstName, string lastName, string gender, string email, string password, string address, string zipCode, string city, int age, int level)
        {
            StreamWriter logStreamWriter;

            var accountsList = new List<Account>();
            string jsonData;

            if (!File.Exists(jsonpath))
            {
                logStreamWriter = new StreamWriter(jsonpath);
            }
            else
            {
                jsonData = File.ReadAllText(jsonpath);
                // De-serialize to object or create new list
                accountsList = JsonConvert.DeserializeObject<List<Account>>(jsonData)
                                      ?? new List<Account>();
            }

            accountsList.Add(new Account()
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                EMail = email,
                Password = password,
                Address = address,
                ZipCode = zipCode,
                City = city,
                Age = age,
                Level = level,
                AccountID = Booking.UniqueCode()
            }); ;

            jsonData = JsonConvert.SerializeObject(accountsList, Formatting.Indented);
            File.WriteAllText(jsonpath, jsonData);

        }

        public static Tuple<Account, bool> login(string email, string password)
        {

            var jsonData = File.ReadAllText(jsonpath);
            // De-serialize to object or create new list
            var accountsList = JsonConvert.DeserializeObject<List<Account>>(jsonData)
                                  ?? new List<Account>();

            bool valid = false;
            int index = 0;

            for (int x = 0; x < accountsList.Count; x++)
            {
                if (accountsList[x].EMail == email)
                {
                    if (accountsList[x].Password == password)
                    {
                        valid = true;
                        index = x;
                    }
                }
            }

            Account user = accountsList[index];
            return new Tuple<Account, bool>(user, valid);

        }

        public static Tuple<Account, bool> logout(Tuple<Account, bool> loggedInUser)
        {
            
            List<Account> accountsList = JsonStuff.JsonToAccountList();
            for (int i = 0; i < accountsList.Count; i++)
            {
                if (accountsList[i].Level == -1)
                {
                    Account guestAlreadyHere = accountsList[i];
                    return new Tuple<Account, bool>(guestAlreadyHere, false);
                }
            }
            Account guest = new Account();
            return new Tuple<Account, bool>(guest, false);
        }

        public static bool EditAccount()
        {
            List<Account> accountList = JsonStuff.JsonToAccountList();
            Console.WriteLine("\n#> Type  | Full Name\n--------------------");
            Console.WriteLine("0> Go back");
            for (int i = 1; i < accountList.Count; i++)
            {
                Console.WriteLine($"{i}> {accountList[i-1].GetRoughInfo()}");
            }

            Console.WriteLine("Type number of account you wish to edit.");
            string input = Console.ReadLine();
            int InputInt = int.Parse(input) -1;
            bool eval = true;
            while (eval)
            {
                for (int i = 1; i < accountList.Count; i++)
                {
                    if (input == "0")
                    {
                        return false;
                    }
                    if (input == $"{i}")
                    {
                        Console.WriteLine($"You picked:\n");
                        Console.WriteLine("0> Go back");
                        accountList[i - 1].GetDetailedInfo();
                        break;
                    } else
                    {
                        Console.WriteLine("Try again.");
                    }
                }
                Console.WriteLine("\nType number of variable you want to edit.");
                string varInput = Console.ReadLine();
                bool varEval = true;
                while (varEval)
                {                    
                    if (varInput == "0")
                    {
                        varEval = false;
                        eval = false;
                        break;
                    } else if(varInput == "1")
                    {
                        Console.WriteLine($"Current first name: {accountList[InputInt].FirstName}\nNew first name:");
                        accountList[InputInt].FirstName = Console.ReadLine();
                    }
                    else if (varInput == "2")
                    {
                        Console.WriteLine($"Current last name: {accountList[InputInt].LastName}\nNew last name:");
                        accountList[InputInt].LastName = Console.ReadLine();
                    }
                    else if (varInput == "3")
                    {
                        Console.WriteLine($"Current gender: {accountList[InputInt].Gender}\nNew gender:");
                        accountList[InputInt].Gender = Console.ReadLine();
                    }
                    else if (varInput == "4")
                    {
                        Console.WriteLine($"Current mail: {accountList[InputInt].EMail}\nNew mail:");
                        accountList[InputInt].EMail = Console.ReadLine();
                    }
                    else if (varInput == "5")
                    {
                        Console.WriteLine($"Current Password: {accountList[InputInt].Password}\nNew Password:");
                        accountList[InputInt].Password = Console.ReadLine();
                    }
                    else if (varInput == "6")
                    {
                        Console.WriteLine($"Current address: {accountList[InputInt].Address}\nNew address:");
                        accountList[InputInt].Address = Console.ReadLine();
                    }
                    else if (varInput == "7")
                    {
                        Console.WriteLine($"Current ZipCode: {accountList[InputInt].ZipCode}\nNew ZipCode:");
                        accountList[InputInt].ZipCode = Console.ReadLine();
                    }
                    else if (varInput == "8")
                    {
                        Console.WriteLine($"Current city: {accountList[InputInt].City}\nNew city:");
                        accountList[InputInt].City = Console.ReadLine();
                    }
                    else if (varInput == "9")
                    {
                        Console.WriteLine($"Current age: {accountList[InputInt].Age}\nNew value:");
                        accountList[InputInt].Age = int.Parse(Console.ReadLine());
                    }
                    else if (varInput == "10")
                    {
                        Console.WriteLine("Level 1 is a user. Level 2 is a user. Be careful.");
                        Console.WriteLine($"Current Level: {accountList[InputInt].Level}\nNew Level:");
                        accountList[InputInt].Level = int.Parse(Console.ReadLine());
                    } else
                    {
                        Console.WriteLine($"Try again.");
                    }
                    JsonStuff.AccountListToJson(accountList);
                    Console.WriteLine("Would you like to edit more values?\n0> No\n1> Yes");
                    int answer = int.Parse(Console.ReadLine());
                    if (answer != 0)
                    {
                        break;
                    } else
                    {
                        varEval = false;
                        break;
                    }
                }
            }
            return true;

        }

        public static bool RegisterAccount()
        {
            Console.WriteLine("Register");
            Console.WriteLine();
            Console.WriteLine("Enter first name : ");
            string regFirstName = Console.ReadLine();
            Screens.ClearCurrentConsoleLine(2);
            Console.WriteLine("Enter last name : ");
            string regLastName = Console.ReadLine();
            Screens.ClearCurrentConsoleLine(2);
            Console.WriteLine("Enter gender : ");
            string regGender = Console.ReadLine();
            Screens.ClearCurrentConsoleLine(2);
            Console.WriteLine("Enter age : ");
            int regAge = int.Parse(Console.ReadLine());
            Screens.ClearCurrentConsoleLine(2);
            Console.WriteLine("Enter e-mail : ");
            string regEMail = Console.ReadLine();
            Screens.ClearCurrentConsoleLine(2);
            Console.WriteLine("Enter Password : ");
            string regPass1 = Console.ReadLine();
            Screens.ClearCurrentConsoleLine(2);
            Console.WriteLine("Confirm Password : ");
            string regPass2 = Console.ReadLine();
            Screens.ClearCurrentConsoleLine(2);
            while (regPass1 != regPass2)
            {
                Console.WriteLine("Passwords do not match, please try again!");
                Console.WriteLine("Enter Password : ");
                regPass1 = Console.ReadLine();
                Screens.ClearCurrentConsoleLine(2);
                Console.WriteLine("Confirm Password : ");
                regPass2 = Console.ReadLine();
                Screens.ClearCurrentConsoleLine(2);
            }
            Console.WriteLine("Enter streetname + number : ");
            string regAddress = Console.ReadLine();
            Screens.ClearCurrentConsoleLine(2);
            Console.WriteLine("Enter zip code : ");
            string regZip = Console.ReadLine();
            Screens.ClearCurrentConsoleLine(2);
            Console.WriteLine("Enter city : ");
            string regCity = Console.ReadLine();
            Screens.ClearCurrentConsoleLine(2);
            Console.WriteLine("Enter user level : ");
            int regLevel = int.Parse(Console.ReadLine());
            Screens.ClearCurrentConsoleLine(2);

            Console.WriteLine("Filled in Information:");
            Console.WriteLine($"{regFirstName}\n{regLastName}\n{regGender}\n{regEMail}\n{regPass1}\n{regAddress}\n{regZip}\n{regCity}\n{regAge}\n{regLevel}\n");
            Console.WriteLine("Would you like to register this account?\n0> No\n1> Yes");
            int regChoice = int.Parse(Console.ReadLine());

            if (regChoice != 0)
            {
                Manager.createAccount(regFirstName, regLastName, regGender, regEMail, regPass1, regAddress, regZip, regCity, regAge, regLevel);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Screens.ClearCurrentConsoleLine(1);
                Console.WriteLine("\n" + "Registered successfully!");
                return true;
            } else
            {
                return true;
            }


        }

        public static bool RemoveAccount()
        {
            List<Account> accountList = JsonStuff.JsonToAccountList();
            Console.WriteLine("\n#> Type  | Full Name\n--------------------");
            Console.WriteLine("0> Go back");
            for (int i = 1; i < accountList.Count; i++)
            {
                Console.WriteLine($"{i}> {accountList[i].GetRoughInfo()}");
            }

            Console.WriteLine("Type number of account you wish to remove.");
            string input = Console.ReadLine();
            int InputInt = int.Parse(input);
            if (InputInt == 0)
            {
                return true;
            }
            for(int i = 0; i < accountList.Count; i++)
            {
                if (InputInt == (i+1))
                {
                    Console.WriteLine($"You are about to remove: {accountList[InputInt].GetRoughInfo()}\nAre you sure?\n0> No\n1> Yes");
                    int removeChoice = int.Parse(Console.ReadLine());
                    if (removeChoice != 0)
                    {
                        accountList.RemoveAt(InputInt);
                        JsonStuff.AccountListToJson(accountList);
                        Console.WriteLine("\nAccount is removed.");
                        Thread.Sleep(1500);
                        return true;
                    } else
                    {
                        return true;
                    }
                }
            }
            RemoveAccount();
            return true;
        }
    }
}
