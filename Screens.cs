using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;

namespace CinemaConsoleApplication
{
    public class Screens
    {


        public static Tuple<Account, bool> LoggedIn;

        public static void ClearCurrentConsoleLine(int howMany)
        {
            for (int i = 0; i < howMany; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor);
            }
        }
        public static bool Movies()
        {
            List<Movie> movieList = JsonStuff.JsonToMovieList();
            Booking Ticket;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("*****Movies page and order tickets*****");
                Console.WriteLine();
                Console.WriteLine("___________________________________");
                Console.WriteLine();
                Console.WriteLine("**_Movies & Showtimes_**");
                Console.WriteLine();
                for(int i = 1; i <= movieList.Count; i++)
                {
                    Console.WriteLine($"{i}> {movieList[i-1].movieInfo()}");
                }
                Console.WriteLine("___________________________________");
                Console.WriteLine();
                Console.WriteLine("To reserve a ticket, enter the movie number ...\n(0) To cancel your reservation ...\n(-1) Back to main menu...\n(-2) Filter by Genre\n(-3) Sort by rating Descending");
                int movienumber = int.Parse(Console.ReadLine());
                while (true)
                {
                    if (movienumber == -1)
                    {
                        return true;
                    }
                    for(int i = 1; i < movieList.Count; i++)
                    {
                        if (movienumber == i)
                        {
                            Console.Clear();
                            Console.WriteLine($"\nMovie | {movieList[i].MovieName}");
                            Console.WriteLine($"Rating| {movieList[i].Rating}/10");
                            Console.WriteLine($"Genre | {movieList[i].Genre}");
                            Console.WriteLine($"Time  | {movieList[i].MovieTime}");
                            foreach(string actor in movieList[i].Cast)
                            {
                                Console.WriteLine($"Cast  | {actor}");
                            }
                            Console.WriteLine($"Plot  |\n{movieList[i].Synopsys}\n");
                            void AvailableSeats()
                            {
                                
                                Console.WriteLine($"\nAvailable seats for {movieList[i-1].MovieName} {movieList[i - 1].MovieTime}\n\n     --------   SCREEN ON THIS SIDE   --------\n");
                                string s = "[";
                                foreach (int[] num in movieList[i - 1].CinemaSeatsX)
                                {
                                    if (num[1] == 0)
                                    {
                                        if (num[0] < 10)
                                        {
                                            s += $" {num[0]}";
                                        }
                                        else
                                        {
                                            s += num[0];
                                        }
                                    }
                                    else
                                    {
                                        s += "RR";
                                    }
                                    if (num[0] == 9 || num[0] == 19)
                                    {
                                        s += " ][$15 per seat]\n[";
                                    } else if(num[0]== 19)
                                    {
                                        s += " ][$12 per seat]\n[";
                                    }
                                    else if (num[0] == 29)
                                    {
                                        s += " ][$9 per seat]\n";
                                    }
                                    else
                                    {
                                        s += " - ";
                                    }
                                }
                                Console.WriteLine(s);
                            }

                            List<int> seatArray = new List<int>();
                            string seatString = "";
                            int currentPrice = 0;
                            while (true)
                            {
                                AvailableSeats();
                                Console.WriteLine($"Picked seats so far: {seatString}");
                                Console.WriteLine($"Total price so far: ${currentPrice}");
                                Console.WriteLine("Choose a(nother) seat number : (or type -1 to order, or type -2 to stop ordering)");
                                int choice = int.Parse(Console.ReadLine());
                                if (choice == -1)
                                {
                                    break;
                                } else if(choice == -2)
                                {
                                    return true;
                                }else if (choice >= 0 && choice < 30)
                                {
                                    if (movieList[i - 1].CinemaSeatsX[choice][1] == 0)
                                    {
                                        movieList[i - 1].CinemaSeatsX[choice][1] = 1;
                                        seatArray.Add(choice);
                                        seatString += $"{choice} ";
                                        if(choice < 10)
                                        {
                                            currentPrice += 15;
                                        }else if(choice > 30)
                                        {
                                            currentPrice += 12;
                                        } else
                                        {
                                            currentPrice += 9;
                                        }
                                    } else
                                    {
                                        Console.WriteLine("\nThis seat is reserved.\n");
                                    }
                                }
                                ClearCurrentConsoleLine(13);
                            }

                            //while (true)
                            //{
                            //    Console.WriteLine("Do you have a discount code?\n1> Yes\n2> No");
                            //    int discountQuestion = int.Parse(Console.ReadLine());
                            //    if (discountQuestion == 1)
                            //    {
                            //        Console.WriteLine("Please enter the code.");
                            //        string discountCode = Console.ReadLine();
                            //        break;
                            //        // LS: discount koppelen

                            //    }
                            //    else
                            //    {
                            //        break;
                            //    }
                            //}

                            Ticket = new Booking(movieList[i - 1], seatArray);
                            if (Ticket.BookTicket())
                            {
                                JsonStuff.MovieListToJson(movieList);
                                return true;
                            } else
                            {
                                Console.WriteLine("Something went very wrong! Please try again later.");
                                Thread.Sleep(1500);
                                return true;
                            }
                        }
                    }

                    if (movienumber == 0)
                    {
                        List<int> seatArray = new List<int>();

                        Console.WriteLine("Enter your unique ticket code:\n>> \n");
                        string UniqueCodeInput = Console.ReadLine();

                        List<Booking> bookingList = JsonStuff.JsonToBookingList();

                        for(int i = 0; i < bookingList.Count; i++)
                        {
                            if (bookingList[i].uniqueCode == UniqueCodeInput)
                            {

                                foreach(Movie movie in movieList)
                                {
                                    if(movie.MovieName == bookingList[i].MovieInfo.MovieName){
                                        foreach(int seat in bookingList[i].SeatList)
                                        {
                                            movie.CinemaSeatsX[seat][1] = 0;
                                        }
                                    }
                                }

                                bookingList[i].ReturnTicket(bookingList[i].SeatList);
                                JsonStuff.MovieListToJson(movieList);
                                bookingList.RemoveAt(i);
                                JsonStuff.BookingListToJson(bookingList);
                                break;
                            }
                        }
                    }

                    break;
                }
                break;
            }
            return false;
        }

        public static void ContactPage()
        {
            static void GenericInfo(string currentSelection)
            {
                Console.Clear();
                Console.WriteLine("*****Contact Page*****");
                Console.WriteLine();
                Console.WriteLine("___________________________________");
                Console.WriteLine();
                Console.WriteLine(currentSelection);
                Console.WriteLine();
                Console.WriteLine("___________________________________");
                Console.WriteLine();
            }
            while (true)
            {

                GenericInfo("\n> Cinema name (1)\n> Email (2)\n> Phone number (3)\n> Opening hours (4)\n> Location (5)\n> About us (6)\n> Back to main menu (7)");

                Cinema CP = JsonStuff.JsonToContactPage();

                //JsonStuff.ContactPageToJson(CP);

                int x = int.Parse(Console.ReadLine());
                while (true)
                {
                    if (x == 1)
                    {
                        GenericInfo("\n>> Cinema name (1)\n> Email (2)\n> Phone number (3)\n> Opening hours (4)\n> Location (5)\n> About us (6)\n> Back to main menu (7)");
                        Console.WriteLine(CP.CinemaName + "\n");
                    }
                    else if (x == 2)
                    {
                        GenericInfo("\n> Cinema name (1)\n>> Email (2)\n> Phone number (3)\n> Opening hours (4)\n> Location (5)\n> About us (6)\n> Back to main menu (7)");
                        Console.WriteLine(CP.Email + "\n");
                    }
                    else if (x == 3)
                    {

                        GenericInfo("\n> Cinema name (1)\n>> Email (2)\n>> Phone number (3)\n> Opening hours (4)\n> Location (5)\n> About us (6)\n> Back to main menu (7)");
                        Console.WriteLine(CP.PhoneNumber + "\n");
                    }
                    else if (x == 4)
                    {

                        GenericInfo("\n> Cinema name (1)\n> Email (2)\n> Phone number (3)\n>> Opening hours (4)\n> Location (5)\n> About us (6)\n> Back to main menu (7)");
                        Console.WriteLine(CP.Openinghours + "\n");
                    }
                    else if (x == 5)
                    {

                        GenericInfo("\n> Cinema name (1)\n> Email (2)\n> Phone number (3)\n> Opening hours (4)\n>> Location (5)\n> About us (6)\n> Back to main menu (7)");
                        Console.WriteLine(CP.Location + "\n");
                    }
                    else if (x == 6)
                    {

                        GenericInfo("\n> Cinema name (1)\n> Email (2)\n> Phone number (3)\n> Opening hours (4)\n> Location (5)\n>> About us (6)\n> Back to main menu (7)");
                        Console.WriteLine(CP.AboutUs + "\n");
                    }
                    else if (x == 7)
                    {
                        break;
                    }
                    else
                    {
                        GenericInfo("\n> Cinema name (1)\n> Email (2)\n> Phone number (3)\n> Opening hours (4)\n> Location (5)\n> About us (6)\n> Back to main menu (7)");
                        Console.WriteLine("Try again!!\n");
                    }
                    x = int.Parse(Console.ReadLine());
                }
                break;
            }
        }

        public static void UserMenu()
        {

            void ViewUserBookings()
            {
                Console.WriteLine("Hier is nog niks");
            }

            void RequestRefund()
            {
                Console.WriteLine("Hier is nog niks");
            }

            void ReviewMovie()
            {
                List<Movie> movieList = JsonStuff.JsonToMovieList();
                Console.WriteLine("0> Go back to user menu.");
                for (int i = 1; i < movieList.Count; i++)
                {
                    Console.WriteLine($"{i}> {movieList[i].MovieName}");
                }

                int x = int.Parse(Console.ReadLine());

                if (x == 0)
                {
                    return;
                }

                Review UserReview = new Review(0, "", LoggedIn.Item1);
                UserReview.SetRating();
                UserReview.SetText();

                while (true)
                {
                    Console.WriteLine("\nAre you happy with this?\n");
                    Console.WriteLine($"Your rating: {UserReview.Rating}/10\nYour review: {UserReview.Text}\n");
                    Console.WriteLine("0> Stop reviewing this movie.\n1> Yes\n2> Redo rating\n3> Redo review\n4> Redo both");

                    int r = int.Parse(Console.ReadLine());
                    if (r == 0)
                    {
                        break;
                    } else if(r == 1)
                    {
                        //movieList[x - 1].ReviewList.Add(UserReview);

                        List<Review> reviewList = JsonStuff.JsonToReviewList();
                        reviewList.Add(UserReview);

                        //List<Review> newReviewList = new List<Review> {UserReview, UserReview};
                        JsonStuff.ReviewListToJson(reviewList);
                        movieList[x - 1].ReviewList.Add(UserReview);
                        JsonStuff.MovieListToJson(movieList);

                        break;
                    } else if (r == 2)
                    {
                        UserReview.SetRating();
                    } else if (r == 3)
                    {
                        UserReview.SetText();
                    } else if (r == 4)
                    {
                        UserReview.SetRating();
                        UserReview.SetText();
                    } else
                    {
                        Console.WriteLine("That wasn't an option!");
                    }
                }

                //while (true)
                //{
                //    Console.WriteLine("Give this movie a rating from 1 to 10:");
                //    int userR = int.Parse(Console.ReadLine());
                //    Console.WriteLine("Give this movie a text review (Max: 300 character");
                //    string userT = Console.ReadLine();
                //    if (userT.Length > 300)
                //    {
                //        Console.WriteLine("")
                //    }
                //}
                //Console.WriteLine("Give this movie a rating from 1 to 10:");
                //int userR = int.Parse(Console.ReadLine());
                //Console.WriteLine("Give this movie a text review (Max: 300 character");
                //string userT = Console.ReadLine();

                

                //Review NewReview = new Review(userR, userT);
                

            }

            while (true)
            {
                if (LoggedIn.Item1.Level == 1)
                {
                    Console.Clear();
                    Console.WriteLine("*****User menu*****");
                    Console.WriteLine();
                    Console.WriteLine($"Welcome, {LoggedIn.Item1.FirstName}!");
                    Console.WriteLine();
                    Console.WriteLine("___________________________________");
                    Console.WriteLine();
                    Console.WriteLine("0> Log out");
                    Console.WriteLine("1> See the playing movies and order tickets");
                    Console.WriteLine("2> See our snack and food deals");
                    Console.WriteLine("3> Our contact page");
                    Console.WriteLine("4> Review a movie");
                    Console.WriteLine("5> Request a refund");
                    Console.WriteLine("6> View past reservations");
                    Console.WriteLine();
                    Console.WriteLine("___________________________________");
                    Console.WriteLine();

                    int x = int.Parse(Console.ReadLine());

                    while (true)
                    {
                        if (x == 0)
                        {
                            LoggedIn = Manager.logout(LoggedIn);
                        } else if (x == 1)
                        {
                            Movies();
                        } else if (x == 2)
                        {
                            DealsPage();
                        } else if (x == 3)
                        {
                            ContactPage();
                        } else if (x == 4)
                        {
                            ReviewMovie();
                        } else if (x == 5)
                        {
                            RequestRefund();
                        } else if (x == 6)
                        {
                            ViewUserBookings();
                        } else
                        {
                            Console.WriteLine("Try again!!\n");
                        }
                        break;
                    }


                }
                else if (LoggedIn.Item1.Level == 2)
                {
                    Console.Clear();
                    Console.WriteLine("*****Admin menu*****");
                    Console.WriteLine();
                    Console.WriteLine($"Welcome, {LoggedIn.Item1.FirstName}!");
                    Console.WriteLine();
                    Console.WriteLine("___________________________________");
                    Console.WriteLine();
                    Console.WriteLine("\n0> Log out");
                    Console.WriteLine("1> Add/Remove/Edit account");
                    Console.WriteLine("2> Add/Remove/Edit movie");
                    Console.WriteLine("3> Add/Remove/Edit deal");
                    Console.WriteLine("4> Edit Contact Page");
                    Console.WriteLine("5> See the playing movies and order tickets");
                    Console.WriteLine("6> See our snack and food deals");
                    Console.WriteLine("7> Our contact page");
                    Console.WriteLine("8> Review a movie");
                    Console.WriteLine("9> Grant/Request a refund");
                    Console.WriteLine("10> View past reservations");
                    Console.WriteLine();
                    Console.WriteLine("___________________________________");
                    Console.WriteLine();

                    int x = int.Parse(Console.ReadLine());

                    while (true)
                    {
                        if (x == 0)
                        {
                            LoggedIn = Manager.logout(LoggedIn);
                        } else if (x == 1)
                        {
                            while (true)
                            {
                                Console.WriteLine("0> Go back");
                                Console.WriteLine("1> Add account");
                                Console.WriteLine("2> Remove account");
                                Console.WriteLine("3> Edit account");

                                int p = int.Parse(Console.ReadLine());
                                if (p == 0)
                                {
                                    break;
                                } else if (p == 1)
                                {
                                    Manager.RegisterAccount();
                                }
                                else if (p == 2)
                                {
                                    Manager.RemoveAccount();
                                }
                                else if (p == 3)
                                {
                                    Manager.EditAccount();
                                }
                                else
                                {
                                    Console.WriteLine("Try again.");
                                }
                                break;
                            }
                        } else if (x == 2)
                        {
                            Console.WriteLine($"Add/Remove/Edit movie");
                        } else if (x == 3)
                        {
                            Console.WriteLine($"Add/Remove/Edit deal");
                        } else if (x == 4)
                        {
                            Cinema CP = JsonStuff.JsonToContactPage();
                            CP.EditCinema();
                            JsonStuff.ContactPageToJson(CP);
                        } else if (x == 5)
                        {
                            Movies();
                        } else if (x == 6)
                        {
                            DealsPage();
                        }
                        else if (x == 7)
                        {
                            ContactPage();
                        } else if (x == 8)
                        {
                            ReviewMovie();
                        } else if (x == 9)
                        {
                            Console.WriteLine($"Request a refund");
                        } else if (x == 10)
                        {
                            Console.WriteLine($"View my past reservations");
                        } else
                        {
                            Console.WriteLine("Try again!!\n");
                        }
                        break;
                    }
                } 
                else if (LoggedIn.Item1.Level == -1){
                    Console.WriteLine("Sending you back to the main menu.");
                    Thread.Sleep(2000);
                    Program.Main();
                }
            }
        }

        public static void LoginPage()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("*****Login/Register*****");
                Console.WriteLine();
                Console.WriteLine("___________________________________");
                Console.WriteLine();
                Console.WriteLine("\n" + "> Log in (1)" + "\n" + "> Register (2)" + "\n" + "> Back to main menu (3)");
                Console.WriteLine();
                Console.WriteLine("___________________________________");
                Console.WriteLine();

                int x = int.Parse(Console.ReadLine());
                while (true)
                {
                    if (x == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("*****Log in*****");
                        Console.WriteLine();
                        Console.WriteLine("___________________________________");
                        Console.WriteLine();
                        Console.WriteLine("\n" + ">> Log in (1)" + "\n" + "> Register (2)" + "\n" + "> Back to main menu (3)");
                        Console.WriteLine();
                        Console.WriteLine("___________________________________");
                        Console.WriteLine();
                        Console.WriteLine("Log in");
                        Console.WriteLine();
                        Console.WriteLine("Enter E-mail : ");
                        string logUser = Console.ReadLine();
                        Console.WriteLine("Enter Password : ");
                        string logPass = Console.ReadLine();
                        LoggedIn = Manager.login(logUser, logPass);
                        if (LoggedIn.Item2 == true)
                        {
                            Console.WriteLine("Login successfull");
                            Thread.Sleep(2000);
                            UserMenu();
                        }
                        else
                        {
                            Console.WriteLine("E-mail and/or password incorrect.");
                            Thread.Sleep(2000);
                            break;
                        }


                    }
                    else if (x == 2)
                    {
                        Console.Clear();
                        Console.WriteLine("*****Register*****");
                        Console.WriteLine();
                        Console.WriteLine("___________________________________");
                        Console.WriteLine();
                        Console.WriteLine("\n" + "> Log in (1)" + "\n" + ">> Register (2)" + "\n" + "> Back to main menu (3)");
                        Console.WriteLine();
                        Console.WriteLine("___________________________________");
                        Console.WriteLine();
                        Console.WriteLine("Register");
                        Console.WriteLine();
                        Console.WriteLine("Enter first name : ");
                        string regFirstName = Console.ReadLine();
                        ClearCurrentConsoleLine(2);
                        Console.WriteLine("Enter last name : ");
                        string regLastName = Console.ReadLine();
                        ClearCurrentConsoleLine(2);
                        Console.WriteLine("Enter gender : ");
                        string regGender = Console.ReadLine();
                        ClearCurrentConsoleLine(2);
                        Console.WriteLine("Enter age : ");
                        int regAge = int.Parse(Console.ReadLine());
                        ClearCurrentConsoleLine(2);
                        Console.WriteLine("Enter e-mail : ");
                        string regEMail = Console.ReadLine();
                        ClearCurrentConsoleLine(2);
                        Console.WriteLine("Enter Password : ");
                        string regPass1 = Console.ReadLine();
                        ClearCurrentConsoleLine(2);
                        Console.WriteLine("Confirm Password : ");
                        string regPass2 = Console.ReadLine();
                        ClearCurrentConsoleLine(2);
                        while (regPass1 != regPass2)
                        {
                            Console.WriteLine("Passwords do not match, please try again!");
                            Console.WriteLine("Enter Password : ");
                            regPass1 = Console.ReadLine();
                            ClearCurrentConsoleLine(2);
                            Console.WriteLine("Confirm Password : ");
                            regPass2 = Console.ReadLine();
                            ClearCurrentConsoleLine(3);
                        }
                        Console.WriteLine("Enter streetname + number : ");
                        string regAddress = Console.ReadLine();
                        ClearCurrentConsoleLine(2);
                        Console.WriteLine("Enter zip code : ");
                        string regZip = Console.ReadLine();
                        ClearCurrentConsoleLine(2);
                        Console.WriteLine("Enter city : ");
                        string regCity = Console.ReadLine();
                        ClearCurrentConsoleLine(2);

                        Manager.createAccount(regFirstName, regLastName, regGender, regEMail, regPass1, regAddress, regZip, regCity, regAge, 1);
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        ClearCurrentConsoleLine(1);
                        Console.WriteLine("\n" + "Registered successfully!");
                        Thread.Sleep(3000);
                    }
                    else if (x == 3)
                    {
                        break;
                    }
                    break;
                }
                break;
            }
        }

        public static void DealsPage()
        {
            List<Deal> dealList = JsonStuff.JsonToDealList();

            void GenericDealsInfo(){

                Console.Clear();
                Console.WriteLine("\n*****Deals Page*****");
                Console.WriteLine("___________________________________\n");
                Console.WriteLine("To pick a deal, pick the corresponding number.\n(-1) Back to main menu...");
                Console.WriteLine("___________________________________\n");
                for (int i = 0; i < dealList.Count; i++)
                {
                    Console.WriteLine($"{i+1}> {dealList[i].Name} | Discount: {dealList[i].Discount}% | Details: {dealList[i].Details}\n");
                }
                Console.WriteLine("___________________________________\n");
            }
            while (true)
            {
                GenericDealsInfo();

                int dealChoice = int.Parse(Console.ReadLine());
                if (dealChoice == -1)
                {
                    break;
                }
                for(int i = 1; i <= dealList.Count; i++)
                {
                    if (dealChoice == i)
                    {
                        Console.WriteLine("You have picked this deal:");
                        Console.WriteLine($"{i + 1}> {dealList[i].Name} | Discount: {dealList[i].Discount}% | Details: {dealList[i].Details}\n");
                        Console.WriteLine("Do you accept?\n1> Yes\n2> No");
                        int dealAccept = int.Parse(Console.ReadLine());
                        if (dealAccept == 1)
                        {
                            string code = Booking.UniqueCode();
                            Console.WriteLine($"This is your unique code: {code}\nEnter this code when ordering tickets to get your discount!\nIf there is any snack/drink discount, show this unique code to the catering service to receive your discount\n");
                            
                            Console.WriteLine($"Press any key to go back to the deals page.");
                            Console.ReadLine();

                            // LS: Voeg toe aan class valid deals & serialize naar json

                            break;
                        } else if (dealAccept == 2) {
                            break;
                        }
                    }
                }
            }
        }
    }
}
