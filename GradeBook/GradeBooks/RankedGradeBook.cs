using GradeBook.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.GradeBooks 
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isweighted) : base(name, isweighted )
        {
            Type = GradeBookType.Ranked;
            IsWeighted = isweighted;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("You must have at least 5 students to do ranked grading.");
            }

            int threshold = (int)Math.Ceiling(Students.Count * 0.2);

            List<Student> sortedStudents = Students.OrderByDescending(s => s.AverageGrade).ToList();

            for (int i = 0; i < sortedStudents.Count; i += threshold)
            {
                if (i == 0 || sortedStudents[i - 1].AverageGrade > averageGrade)
                {
                    if (i + threshold > sortedStudents.Count || sortedStudents[i + threshold - 1].AverageGrade < averageGrade)
                    {
                        return 'A';
                    }
                    else
                    {
                        return 'B';
                    }
                }
                else if (i + threshold > sortedStudents.Count || sortedStudents[i + threshold - 1].AverageGrade < averageGrade)
                {
                    if (i + threshold * 2 > sortedStudents.Count || sortedStudents[i + threshold * 2 - 1].AverageGrade < averageGrade)
                    {
                        return 'C';
                    }
                    else
                    {
                        return 'D';
                    }
                }
            }

            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students.");
                return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count() < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students.");
                return;
            }

            base.CalculateStudentStatistics(name);
        }
    }
}
