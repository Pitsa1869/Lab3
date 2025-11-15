using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class EmployeeCourse
    {
        public Course Course {  get; set; }
        public DateTime CompletionDate { get; set; }

        public EmployeeCourse(Course course, DateTime completionDate)
        {
            Course = course;
            CompletionDate = completionDate;
        }
        public EmployeeCourse()
        {
            Course = new Course();
            CompletionDate = DateTime.Now;
        }
    }
}
