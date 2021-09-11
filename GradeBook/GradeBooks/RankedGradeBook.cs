using System;
using System.Collections.Generic;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
        {
            Type = GradeBookType.Ranked;
        }
        public override void CalculateStatistics()
        {
            var totalStudents = Students.Count;

            if (totalStudents < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            var totalStudents = Students.Count;

            if (totalStudents < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStudentStatistics(name);
        }

        public override char GetLetterGrade(double averageGrade)
        {
            var totalStudents = Students.Count;
            
            if (totalStudents < 5)
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work.");

            var sortedStudents = getSortedStudents();
            var gradeIndex = findGradeIndex(averageGrade, sortedStudents);

            switch ((double)(gradeIndex + 1) / (totalStudents + 1))
            {
                case var position when position > 0.8:
                    return 'A';
                case var position when position > 0.6:
                    return 'B';
                case var position when position > 0.4:
                    return 'C';
                case var position when position > 0.2:
                    return 'D';
                default:
                    return 'F';
            }
        }

        private List<Student> getSortedStudents()
        {
            List<Student> studentsToSort = new List<Student>(Students);
            
            studentsToSort.Sort(delegate (Student studentA, Student studentB)
            {
                return studentA.AverageGrade.CompareTo(studentB.AverageGrade);
            });

            return studentsToSort;
        }

        private int findGradeIndex(double averageGrade, List<Student> sortedStudents)
        {
            var gradeIndex = 0;

            while (gradeIndex < Students.Count && averageGrade > sortedStudents[gradeIndex].AverageGrade)
            {
                gradeIndex++;
            }

            return gradeIndex;
        }
        
    }
}
