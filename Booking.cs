using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CinemaConsoleApplication
{
    class Booking
    {
        public Movie MovieInfo;
        public List<int> SeatList;
        bool buyingTicket;
        bool returnTicket;
        int price;
        public string uniqueCode;

        public Booking() { }
        public Booking(Movie _movieInfo, List<int> seatList)
        {
            MovieInfo = _movieInfo;
            SeatList = seatList;
        }
        public Booking(Movie _movieInfo, List<int> seatList, List<int> discountList)
        {
            MovieInfo = _movieInfo;
            SeatList = seatList;
        }

        public bool BookTicket()
        {
            uniqueCode = UniqueCode();
            buyingTicket = true;
            if (BookingInfo())
            {
                return true;
            } else
            {
                return false;
            }
        }
        public void ReturnTicket(List<int> ReservedSeatList)
        {
            ReturnInfo();
        }

        public bool BookingInfo()
        {
            if (buyingTicket == true)
            {
                Console.Clear();
                Console.WriteLine("--------------------------------------\n");
                Console.WriteLine(">> your ticket details: \n");
                string StringSeatList = "|";
                foreach(int seat in SeatList)
                {
                    if (seat >= 0 && seat <= 9)
                    {
                        price += 15;
                    }
                    else if (seat > 9 && seat <= 29)
                    {
                        price += 12;
                    }
                    else
                    {
                        price += 9;
                    }
                    StringSeatList += $" {seat} |";
                }
                Console.WriteLine(MovieInfo.movieInfo() + "\nTicket Code: " + uniqueCode + "\nSeat number: " + StringSeatList + "\nTicket price: " + price + "$");

                Console.WriteLine("--------------------------------------\n");
                Console.WriteLine("Are you happy with this reservation?\n0> No\n1> Yes");
                int reserveChoice = int.Parse(Console.ReadLine());
                if(reserveChoice != 0)
                {
                    List<Booking> bookingList = JsonStuff.JsonToBookingList();
                    bookingList.Add(this);
                    JsonStuff.BookingListToJson(bookingList);
                    Console.WriteLine("\nOrdering was a success. Don't forget the unique code, and enjoy the movie!\nPress any key to continue");
                    Console.ReadLine();
                    return true;
                }
            }
            else
            {
                Console.WriteLine("This seat is reserved, choose another seat.");
            }
            return false;



        }
        public void ReturnInfo()
        {
            if (returnTicket == true)
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("\n\n");
                Console.WriteLine(">>Your reservation has been cancelled successfully....\n");
            }
            else
            {
                Console.WriteLine("The reservation yor are canceling does not exist.");
            }
        }
        public static string UniqueCode()
        {
            int length = 6;
            string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random randomCode = new Random();
            return new string(Enumerable.Repeat(Chars, length).Select(x => x[randomCode.Next(x.Length)]).ToArray());
        }
    }
}
