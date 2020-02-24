
using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args); //first parameter is who is sending, second is arguments

    public class NamedObject //base class//classes usually go in different files 
    {
        public NamedObject(string name)
        {
            Name = name;
        }

        public string Name
        {
            get;
            set;
        }
    }

    public interface IBook //all interfaces begin with I
    {
        void AddGrade(double grade);
        Statistics GetStats();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }

    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public abstract Statistics GetStats(); //virtual says that a derived class might chose to override implementation details for method  

    }
    public class DiskBook : Book //need to specify name of base class to use.
    {                                   //Constructor. With Base(arg) pass along arguments 

        public DiskBook(string name) : base(name)
        {
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {

            using (var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
        }

        public override Statistics GetStats()
        {


            var result = new Statistics();
            using (var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();
                }
            }
            return result;

        }
    }

    public class InMemoryBook : Book //need to specify name of base class to use.
    {                                   //Constructor. With Base(arg) pass along arguments 
        public InMemoryBook(string name) : base(name)
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

        public override void AddGrade(double grade)
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
        public override event GradeAddedDelegate GradeAdded;
        public override Statistics GetStats()
        {
            var result = new Statistics();
            foreach (double grade in grades)
            {
                result.Add(grade);
            };
            return result;
        }
        public List<double> grades;

        public const string CATEGORY = "Science"; // readonly = can only be set in constructor or initialization. const = (PUT UPPERCASE)cannot be changes once initialized

    }
}