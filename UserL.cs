//using System;
//using System.Collections.Generic;
//using System.Text;
//using Newtonsoft.Json;

//namespace CinemaConsoleApplication
//{

//    class User
//    {
//        public string FirstName;
//        public string LastName;
//        public string FullName;
//        public string Mail;

//        public Reservation UserReservation;
//        public Ticket[] UserTickets;
//        public Review[] UserReviews;
//        public Ticket[] UserRefundRequests;
//        public Ticket[] UserRefundAccepted;

//        public User(string firstName, string lastName, string mail)
//        {
//            this.FirstName = firstName;
//            this.LastName = lastName;
//            this.FullName = firstName + lastName;
//            this.Mail = mail;
//        }

//    }

//    class Admin : User
//    {
//        public Admin(string firstName, string lastName, string mail) : base(firstName, lastName, mail)
//        {
            
//        }
//    }
//}
