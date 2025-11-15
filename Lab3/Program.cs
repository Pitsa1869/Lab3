using System.Net;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Lab3
{
    internal class Program
    {

        
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>();
            List<Course> courses = new List<Course>();
            
            SetUpData(courses, employees);


            int option = -1;
            string input;


            while (option != 9)
            {
                PrintMenu();

                Console.Write("Enter the option: ");
                input = Console.ReadLine() ?? string.Empty;

                while (!IsInt(input) || input == string.Empty)
                {
                    Console.WriteLine("Option must be numeric");
                    input = Console.ReadLine() ?? string.Empty;
                }
                option = int.Parse(input);
                switch (option)
                {
                    case 1:
                        {
                            DisplayEmployee(employees);
                            break;
                        }
                    case 2:
                        {
                            AddNewCourse(courses);
                            break;
                        }
                    case 3:
                        {
                            DisplayEmployeesWithNoRecentCourse(employees);
                            break;
                        }
                    case 4:
                        {
                            Dictionary<Course, List<Employee>> listOfEmployeesByCourse = new Dictionary<Course, List<Employee>>();

                            listOfEmployeesByCourse = ListEmployeesByCourse(employees, courses);

                            foreach (var KVP in listOfEmployeesByCourse)
                            {
                                Console.Write($"\n{KVP.Key.CourseCode} : ");
                                foreach (Employee employee in KVP.Value)
                                {

                                    if (employee != KVP.Value.Last())
                                        Console.Write($"{employee.FirstName} {employee.Surname}, ");
                                    else
                                        Console.Write($"{employee.FirstName} {employee.Surname} ");
                                }
                                
                            }
                            break;
                        }
                    case 5:
                        {
                            break;
                        }
                    case 6:
                        {
                            break;
                        }
                    case 7:
                        {
                            break;
                        }
                    case 8:
                        {
                            break;
                        }
                    case 9:
                        {
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("There is no such option.");
                            break;
                        }
                }
            }
        }



        

        
        private static void SetUpData(List<Course> courses, List<Employee> employees)

        {

            courses.Add(new Course { CourseCode = "C101", Description = "Project Management", Credits = 3 });

            courses.Add(new Course { CourseCode = "C102", Description = "Advanced C# Programming", Credits = 4 });

            courses.Add(new Course { CourseCode = "C103", Description = "Data Structures", Credits = 3 });

            courses.Add(new Course { CourseCode = "C104", Description = "Web Development", Credits = 4 });

            courses.Add(new Course { CourseCode = "C105", Description = "Database Design", Credits = 3 });

            courses.Add(new Course { CourseCode = "C106", Description = "Software Testing", Credits = 3 });


            // Create Employees and their Course Completions


            employees.Add(new Employee { EmployeeNum = 1001, FirstName = "Alice", Surname = "Johnson" });

            employees.Add(new Employee { EmployeeNum = 1002, FirstName = "Bob", Surname = "Smith" });

            employees.Add(new Employee { EmployeeNum = 1003, FirstName = "Carol", Surname = "White" });

            employees.Add(new Employee { EmployeeNum = 1004, FirstName = "David", Surname = "Green" });

            employees.Add(new Employee { EmployeeNum = 1005, FirstName = "Eve", Surname = "Black" });

            employees.Add(new Employee { EmployeeNum = 1006, FirstName = "Frank", Surname = "Brown" });

            employees.Add(new Employee { EmployeeNum = 1007, FirstName = "Grace", Surname = "Taylor" });

            employees.Add(new Employee { EmployeeNum = 1008, FirstName = "Henry", Surname = "Wilson" });

            employees.Add(new Employee { EmployeeNum = 1009, FirstName = "Irene", Surname = "Martinez" });

            employees.Add(new Employee { EmployeeNum = 1010, FirstName = "Jack", Surname = "Anderson" });



            // Assign courses to employees with completion dates

            employees[0].AddCourse(courses[0], new DateTime(2025, 1, 15));

            employees[0].AddCourse(courses[2], new DateTime(2025, 6, 10));


            employees[1].AddCourse(courses[1], new DateTime(2025, 5, 20));

            employees[1].AddCourse(courses[3], new DateTime(2025, 2, 5));

            employees[1].AddCourse(courses[5], new DateTime(2025, 7, 20));


            employees[2].AddCourse(courses[4], new DateTime(2025, 6, 18));

            employees[2].AddCourse(courses[2], new DateTime(2024, 11, 30));


            employees[3].AddCourse(courses[0], new DateTime(2024, 12, 15));

            employees[3].AddCourse(courses[5], new DateTime(2025, 5, 10));


            employees[4].AddCourse(courses[3], new DateTime(2025, 6, 12));


            employees[5].AddCourse(courses[4], new DateTime(2025, 3, 25));

            employees[5].AddCourse(courses[5], new DateTime(2025, 7, 5));


            employees[6].AddCourse(courses[0], new DateTime(2024, 11, 20));

            employees[6].AddCourse(courses[1], new DateTime(2025, 6, 25));


            employees[7].AddCourse(courses[2], new DateTime(2024, 12, 30));

            employees[7].AddCourse(courses[3], new DateTime(2025, 4, 5));


            employees[8].AddCourse(courses[4], new DateTime(2025, 5, 25));


            employees[9].AddCourse(courses[5], new DateTime(2025, 1, 18));

            employees[9].AddCourse(courses[1], new DateTime(2025, 7, 2));
        }

        public static void DisplayEmployee(List<Employee> employees)
        {
            Console.WriteLine("EmployeeNum\t FirstName \t Surname \t CompletedCourse \t");
            foreach (Employee employee in employees)
            {
                employee.Print();
            }
        }

        public static void AddNewCourse(List<Course> courses)
        {
            Course course = new Course();
            string code, description, input;
            int credits;
            Console.WriteLine("Enter Course Code: ");
            code = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter Course Description");
            description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter Credits: ");
            input = Console.ReadLine() ?? string.Empty;

            while (!IsInt(input) || input == string.Empty)
            {
                Console.WriteLine("Credits must be numeric. Try again");
                input = Console.ReadLine() ?? string.Empty;
            }

            credits = int.Parse(input);

            course.CourseCode = code;
            course.Description = description;
            course.Credits = credits;

            courses.Add(course);
        }

        public static void DisplayEmployeesWithNoRecentCourse(List<Employee> employees)
        {
            DateTime sixMonth = DateTime.Now.AddMonths(-6);

            Console.Write("List of employees who has not completed any course in past 6 months:\n");
            foreach (Employee employee in employees)
            {
                int coursecount = 0;
                foreach (EmployeeCourse course in employee.CompletedCourses)
                {
                    if (course.CompletionDate > sixMonth)
                    {
                        coursecount++;
                    }
                }

                if (coursecount == 0)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.Surname}");
                }
                
                
            }
        }

        public static Dictionary<Course, List<Employee>> ListEmployeesByCourse(List<Employee> employees, List<Course> courses)
        {
            Dictionary<Course, List<Employee>> dictionary = new Dictionary<Course, List<Employee>>();

            for (int i = 0; i < courses.Count; i++)
            {
                dictionary.Add(courses[i], new List<Employee>());
            }
            foreach (KeyValuePair<Course, List<Employee>> KVP in dictionary)
            {
                foreach (var employee in employees)
                {
                    foreach (var course in employee.CompletedCourses)
                    {
                        if(course.Course == KVP.Key)
                        {
                            KVP.Value.Add(employee);
                        }
                    }
                }
            }


            return dictionary;
        }


        public static bool IsInt(string input)
        {
            int output;
            if (int.TryParse(input, out output))
                return true;
            else return false;
        }
        public static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("------------------------------- Menu -------------------------------");
            Console.WriteLine("1. Display Employees Records and Courses");
            Console.WriteLine("2. Add a New Course to the System");
            Console.WriteLine("3. Display Employees with No Recent Course Completions");
            Console.WriteLine("4. List Employees by Course Completion");
            Console.WriteLine("5. Search Employees by Completed Courses");
            Console.WriteLine("6. Search Employees by Completed Courses");
            Console.WriteLine("7. Identify Employees Missing a Specific Cours");
            Console.WriteLine("8. Skill Breakdown Report");
            Console.WriteLine("9. Exit");
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine();
        }
    }
}
