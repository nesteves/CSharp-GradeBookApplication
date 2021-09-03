using System;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            var totalStudents = Students.Count;
            
            if (totalStudents < 5)
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work.");

            sortStudents();
            var gradeIndex = findGradeIndex(averageGrade);

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

        private void sortStudents()
        {
            Students.Sort(delegate (Student studentA, Student studentB)
            {
                return studentA.AverageGrade.CompareTo(studentB.AverageGrade);
            });
        }

        private int findGradeIndex(double averageGrade)
        {
            var gradeIndex = 0;

            while (gradeIndex < Students.Count && averageGrade > Students[gradeIndex].AverageGrade)
            {
                gradeIndex++;
            }

            return gradeIndex;
        }
        
    }
}
