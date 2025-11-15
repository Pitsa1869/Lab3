using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Course
    {
        public string CourseCode {  get; set; }

        public string Description { get; set; }

        public int Credits { get; set; }

        public Course(string courseCode, string description, int credits)
        {
            CourseCode = courseCode;
            Description = description;
            Credits = credits;
        }
        public Course()
        {
            CourseCode = string.Empty;
            Description = string.Empty;
            Credits = 0;
        }
        public void Print()
        {
            Console.WriteLine($"{this.CourseCode}\t {this.Description} \t {this.Credits}");
        }
    }
}
