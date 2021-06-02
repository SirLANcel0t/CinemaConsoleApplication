using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaConsoleApplication
{
    public class Cinema
    {
        public string CinemaName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Openinghours { get; set; }
        public string Location { get; set; }
        public string AboutUs { get; set; }


        public Cinema(string cinemaName, string email, string phoneNumber, string openingHours, string location, string aboutUs)
        {
            CinemaName = cinemaName;
            Email = email;
            PhoneNumber = phoneNumber;
            Openinghours = openingHours;
            Location = location;
            AboutUs = aboutUs;

        }

        public bool EditCinema()
        {
            Console.Clear();
            Console.WriteLine($"\nPick a number to edit the value\n\n0> Go back\n1> Cinema Name: {CinemaName}\n2> Email: {Email}\n3> Phone number: {PhoneNumber}\n4> Opening hours: {Openinghours}\n5> Location: {Location}\n6> About us: {AboutUs}\n");
            int editChoice = int.Parse(Console.ReadLine());
            if (editChoice == 0)
            {
                return true;
            } else if(editChoice == 1)
            {
                Console.WriteLine("Enter text to overwrite Cinema Name:");
                CinemaName = Console.ReadLine();
            } else if(editChoice == 2)
            {
                Console.WriteLine("Enter text to overwrite Email:");
                Email = Console.ReadLine();
            } else if(editChoice == 3)
            {
                Console.WriteLine("Enter text to overwrite Phone number:");
                PhoneNumber = Console.ReadLine();
            } else if(editChoice == 4)
            {
                Console.WriteLine("Enter text to overwrite Opening hours:");
                Openinghours = Console.ReadLine();
            } else if(editChoice == 5)
            {
                Console.WriteLine("Enter text to overwrite Location:");
                Location = Console.ReadLine();

            } else if(editChoice == 6)
            {
                Console.WriteLine("Enter text to overwrite About us:");
                AboutUs = Console.ReadLine();
            }
            JsonStuff.ContactPageToJson(this);
            EditCinema();
            return true;
        }
    }
}