using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaConsoleApplication
{
    class Deal
    {
        public string Name;
        public int Discount;
        public string[] DealItems;
        public string Details;
        public int NumberOfSeats;
        public string DealType;
        public string DealID;

        public Deal(){}

        public Deal(string name, int discount, string[] dealItems, string details, int numberOfSeats, string dealType)
        {
            Name = name;
            Discount = discount;
            DealItems = dealItems;
            Details = details;
            NumberOfSeats = numberOfSeats;
            DealType = dealType;
            DealID = Booking.UniqueCode();
        }
    }

    //class MovieDeal : Deal
    //{
    //    public Movie[] MovieDealList;
    //    public MovieDeal(string name, int discount, Movie[] movieDealList) : base(name, discount)
    //    {
    //        MovieDealList = movieDealList;
    //    }
    //}

    //class MealDeal : Deal
    //{
    //    public MealDeal(string name) : base(name)
    //    {

    //    }
    //}

    //class ComboDeal : Deal
    //{
    //    public ComboDeal(string name) : base(name)
    //    {

    //    }
    //}
}
