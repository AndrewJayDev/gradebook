using System;
using System.Collections.Generic;


namespace GradeBook
{

    class Program
    {
        static void Main(string[] args)
        {
            var book = new Book("Andrew's Grade Book");
            book.GradeAdded += OnGradeAdded;
            while (true)
            {
                System.Console.WriteLine("Please type grade and prese enter to continue (type Q to exit)");
                //stores entered value as a string                
                var input = Console.ReadLine();
                // adds grade into array until Q is entered which exits the program 
                if (input == "Q")
                {
                    break;
                }
                try                 //error handling if error will go to nearest catch statement 
                {
                    //converts the string into a double
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex) //nearest catch statement. Can be stacked
                {
                    System.Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
                /* finally //can be used to see if things are cleaned up correctly
                {
                    ...
                } */
            }
            book.GetStats();
            var stats = book.GetStats();
            Console.WriteLine(Book.CATEGORY);
            Console.WriteLine($"For the book named {book.Name}");
            Console.WriteLine($"the average grade is {stats.Low}");
            Console.WriteLine($"the highest grade is {stats.High}");
            Console.WriteLine($"the lowest grade is {stats.Average:N1}");
            Console.WriteLine($"The letter grade is {stats.Letter}");
        }
        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added");
        }
    }
}
