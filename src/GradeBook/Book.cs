
using System;
using System.Collections.Generic;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args); //first parameter is who is sending, second is arguments
    public class Book
    {
        public Book(string name) //Constructor
        {
            grades = new List<double>();
            Name = name;
        }

        public void AddLetterGrade(char letter) //can be overloaded (have the same name as another method) since it has a different signature
        {
            switch (letter)
            {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                default:
                    AddGrade(0);
                    break;

            }
        }

        public void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0) //only adds grade if between 0 and 100
            {
                grades.Add(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs()); //sends an event from this to the Eventsubscriber
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }
        public event GradeAddedDelegate GradeAdded;
        public Statistics GetStats()
        {
            var result = new Statistics();
            result.Average = 0.0;
            result.High = double.MinValue;
            result.Low = double.MaxValue;
            foreach (double grade in grades)
            {
                result.High = Math.Max(grade, result.High);
                result.Low = Math.Min(grade, result.Low);
                result.Average += grade;
            };

            result.Average /= grades.Count;
            switch (result.Average)
            {
                case var d when d >= 90.0:
                    result.Letter = 'A';
                    break;
                case var d when d >= 80.0:
                    result.Letter = 'B';
                    break;
                case var d when d >= 70.0:
                    result.Letter = 'C';
                    break;
                case var d when d >= 60.0:
                    result.Letter = 'D';
                    break;
                default:
                    result.Letter = 'F';
                    break;
            }

            return result;
        }
        public List<double> grades;
        public string Name //READ ONLY if setter is listes as private. 
        {
            get;
            set; //making it private encapsulates the state
        }

        public const string CATEGORY = "Science"; // readonly = can only be set in constructor or initialization. const = (PUT UPPERCASE)cannot be changes once initialized

    }
}