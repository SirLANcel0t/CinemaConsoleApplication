using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaConsoleApplication
{
    public class Review
    {
        public int Rating;
        public string Text;
        public string MadeByAccountID;
        public string MovieID;

        public Review(){}

        public Review(int rating, string text, Account account)
        {
            Rating = rating;
            Text = text;
            MadeByAccountID = account.AccountID;
        }

        public void SetRating()
        {
            Console.WriteLine("Give this movie a rating from 1 to 10:");
            int input = int.Parse(Console.ReadLine());
            if (input >= 0 && input <= 10)
            {
                Rating = input;
            } else
            {
                Console.WriteLine("Keep it within 1 to 10 please.");
                SetText();
            }
        }

        public void SetText()
        {
            Console.WriteLine("Give this movie a text review (Max: 300 character");
            string input = Console.ReadLine();
            if(input.Length > 300)
            {
                Console.WriteLine("You went over 300 characters. Try again.");
                SetText();
            } else
            {
                Text = input;
            }
        }

        public void CheckInfo()
        {
            Console.WriteLine($"Rating: {Rating}/10");
            Console.WriteLine($"Review: {Text}");
        }
    }
}
