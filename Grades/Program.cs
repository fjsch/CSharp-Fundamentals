using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Grades
{
    class Program
    {
        static void Main(string[] args)
        {
            IGradeTracker book = CreateGradeBook();
            AddGrades(book);
            WriteGradeData(book);
            ShowGradeData(book);
        }

        private static IGradeTracker CreateGradeBook()
        {
            return new ThrowAwayGradeBook();
        }

        private static void AddGrades(IGradeTracker book)
        {
            book.AddGrade(8);
            book.AddGrade(9.1f);
            book.AddGrade(75);
        }

        private static void WriteGradeData(IGradeTracker book)
        {
            using (StreamWriter outputFile = File.CreateText("grades.txt"))
            {
                book.WriteGrades(outputFile);
                outputFile.Close();
            }
        }

        private static void ShowGradeData(IGradeTracker book)
        {
            GradeStatistics stats = book.ComputeStatistics();
            Console.WriteLine(book.Name);
            WriteResult("Average", stats.AverageGrade);
            WriteResult("Highest", stats.HighestGrade);
            WriteResult("Lowest", stats.LowestGrade);
        }

        static void OnNameChanged(object sender, NameChangedEventArgs args)
        {
            Console.WriteLine($"Grade book changing name from {args.ExistingName} to {args.NewName}");
        }

        static void WriteResult(string description, int result)
        {
            Console.WriteLine(description + ": " + result);
        }

        static void WriteResult(string description, float result)
        {
            Console.WriteLine(description + ": " + result);
        }
    }
}
