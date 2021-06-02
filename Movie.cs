using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace CinemaConsoleApplication

{
    class Movie
    {
        public string MovieName { get; set; }
        public int Rating { get; set; }
        public string[] Cast { get; set; }
        public string Synopsys;
        public string Genre;
        public string MovieID;

        public List<Review> ReviewList { get; set; }

        public Movie(){}

        public DateTime MovieTime;

        public List<int[]> CinemaSeatsX;


        public Movie(string movieName, DateTime movieTime, int rating, string[] cast, string synopsys, string genre)
        {
            MovieName = movieName;
            MovieTime = movieTime;
            Rating = rating;
            Cast = cast;
            Synopsys = synopsys;
            Genre = genre;
            ReviewList = new List<Review>();
            for (int i = 0; i < 30; i++)
            {
                int[] SeatsX = new int[2] { i, 0 };
                CinemaSeatsX.Add(SeatsX);
            }
            MovieID = Booking.UniqueCode();
        }
        public string movieInfo()
        {
            return $"{MovieName} | {Genre} | {MovieTime} | {Rating}";
        }

        public bool removeSeatX(int SeatX)
        {
            if (CinemaSeatsX[SeatX][1] == 0)
            {
                CinemaSeatsX[SeatX][1] = 1;
                return true;
            } else
            {
                return false;
            }
        }
        public bool AddSeatX(int SeatX)
        {
            if (CinemaSeatsX[SeatX][1] == 1)
            {
                CinemaSeatsX[SeatX][1] = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}


