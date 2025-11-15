using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Employee
    {
        public int EmployeeNum { get; set; }
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public List<EmployeeCourse> CompletedCourses { get; set; }

        public Employee(int employeeNum, string firstname, string surname, List<EmployeeCourse> completedCourses)
        {
            EmployeeNum = employeeNum;
            FirstName = firstname;
            Surname = surname;
            CompletedCourses = completedCourses;
        }

        public Employee()
        {
            EmployeeNum = 0;
            FirstName = string.Empty;
            Surname = string.Empty;
            CompletedCourses = new List<EmployeeCourse>();
        }

        public void AddCourse(Course course, DateTime completionDate)
        {
            this.CompletedCourses.Add(new EmployeeCourse(course,completionDate) );
        }
        
        public List<EmployeeCourse> GetCourses()
        {
            return this.CompletedCourses;
        }

        public void Print()
        {
            
            Console.Write($"{this.EmployeeNum} \t\t {this.FirstName}  \t {this.Surname} \t");
            foreach (var course in this.CompletedCourses)
            {
                Console.Write($"{course.Course.CourseCode} - {course.CompletionDate}\n\t\t\t\t\t");
            }
            Console.WriteLine();
        }
    }
}
