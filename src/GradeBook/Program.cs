using System;
using System.Collections.Generic;


namespace GradeBook
{

    class Program
    {
        static void Main(string[] args)
        {
            var book = new Book("Andrew's Grade Book");
            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.5);
            book.GetStats();
            var stats = book.GetStats();

            Console.WriteLine($"the average grade is {stats.Low}");
            Console.WriteLine($"the highest grade is {stats.High}");
            Console.WriteLine($"the lowest grade is {stats.Average:N1}");

        }
    }
}
