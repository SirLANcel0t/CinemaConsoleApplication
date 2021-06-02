using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace CinemaConsoleApplication
{
    class JsonStuff
    {
        public static string moviePath = Path.GetFullPath(@"movies.json");
        public static string bookingPath = Path.GetFullPath(@"bookings.json");
        public static string dealPath = Path.GetFullPath(@"deals.json");
        public static string accountPath = Path.GetFullPath(@"accounts.json");
        public static string contactPagePath = Path.GetFullPath(@"contactPage.json");
        public static string reviewPath = Path.GetFullPath(@"reviews.json");

        // Movies
        public static List<Movie> JsonToMovieList()
        {
            string moviesText = File.ReadAllText(moviePath);
            List<Movie> movieList = JsonConvert.DeserializeObject<List<Movie>>(moviesText);
            return movieList;
        }
        public static void MovieListToJson(List<Movie> movieList)
        {
            string moviesData = JsonConvert.SerializeObject(movieList, Formatting.Indented);
            File.WriteAllText(moviePath, moviesData);
        }

        // Bookings
        public static List<Booking> JsonToBookingList()
        {
            string bookingText = File.ReadAllText(bookingPath);
            List<Booking> bookingList = JsonConvert.DeserializeObject<List<Booking>>(bookingText);
            return bookingList;
        }
        public static void BookingListToJson(List<Booking> bookingList)
        {
            string bookingData = JsonConvert.SerializeObject(bookingList, Formatting.Indented);
            File.WriteAllText(bookingPath, bookingData);
        }

        // Deals
        public static List<Deal> JsonToDealList()
        {
            string dealText = File.ReadAllText(dealPath);
            List<Deal> dealList = JsonConvert.DeserializeObject<List<Deal>>(dealText);
            return dealList;
        }
        public static void BookingListToJson(List<Deal> dealList)
        {
            string dealData = JsonConvert.SerializeObject(dealList, Formatting.Indented);
            File.WriteAllText(dealPath, dealData);
        }

        // Contact Page
        public static void ContactPageToJson(Cinema cinema)
        {
            string contactData = JsonConvert.SerializeObject(cinema, Formatting.Indented);
            File.WriteAllText(contactPagePath, contactData);
        }
        public static Cinema JsonToContactPage()
        {
            string contactPageText = File.ReadAllText(contactPagePath);
            Cinema contactPage = JsonConvert.DeserializeObject<Cinema>(contactPageText);
            return contactPage;
        }

        // Acounts
        public static void AccountListToJson(List<Account> accountList)
        {
            string accountData = JsonConvert.SerializeObject(accountList, Formatting.Indented);
            File.WriteAllText(accountPath, accountData);
        }
        public static List<Account> JsonToAccountList()
        {
            string accountText = File.ReadAllText(accountPath);
            List<Account> accountList = JsonConvert.DeserializeObject<List<Account>>(accountText);
            return accountList;
        }

        // Reviews
        public static void ReviewListToJson(List<Review> reviewList)
        {
            string reviewData = JsonConvert.SerializeObject(reviewList, Formatting.Indented);
            File.WriteAllText(reviewPath, reviewData);
        }
        public static List<Review> JsonToReviewList()
        {
            string accountText = File.ReadAllText(reviewPath);
            List<Review> reviewList = JsonConvert.DeserializeObject<List<Review>>(accountText);
            return reviewList;
        }
    }

}