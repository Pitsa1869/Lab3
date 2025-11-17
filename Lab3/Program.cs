using Microsoft.CSharp.RuntimeBinder;
using System.Net;
using System.Net.Http.Headers;
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
                Console.WriteLine();
                option = int.Parse(input);
                switch (option)
                {
                    case 1:
                        {
                            DisplayEmployeesAndCourses(employees, courses);
                            break;
                        }
                    case 2:
                        {
                            AddNewCourse(courses);
                            break;
                        }
                    case 3:
                        {
                            PrintDashes();
                            DisplayEmployeesWithNoRecentCourse(employees);
                            PrintDashes();
                            
                            break;
                        }
                    case 4:
                        {
                            Dictionary<Course, List<Employee>> listOfEmployeesByCourse = new Dictionary<Course, List<Employee>>();

                            listOfEmployeesByCourse = ListOfEmployeesByCourse(employees, courses);

                            PrintDashes();
                            Console.WriteLine($"{"List of employees by course",65}");

                            PrintDashes();
                            Console.WriteLine($"{"Course Code",-15}{"Course Description",-50}{"Completed By",-30}");
                            PrintDashes();
                            foreach (KeyValuePair<Course, List<Employee>> KVP in listOfEmployeesByCourse)

                            {
                                foreach (Employee employee in KVP.Value)
                                {

                                    if (employee == KVP.Value.First())
                                        Console.WriteLine($"{KVP.Key.CourseCode,-15}{KVP.Key.Description,-50}{employee.FirstName+" "+employee.Surname,-30}");
                                    else
                                        Console.WriteLine($"{"",-15}{"",-50}{employee.FirstName + " " + employee.Surname,-30}");
                                }
                                PrintDashes();
                            }

                            PrintDashes();

                            break;
                        }

                    case 5:
                        {

                            List<string> codes = new List<string>();

                            Dictionary<Course, List<Employee>> listOfEmployeesByEnteredCourses = new Dictionary<Course, List<Employee>>();
                            Console.WriteLine("Enter Course Codes (Press enter to stop):");
                            IEnumerable<Employee> intersect = new List<Employee>();
                            input = Console.ReadLine() ?? string.Empty;
                            codes.Add(input);
                            while (input != string.Empty)
                            {
                                input = Console.ReadLine() ?? string.Empty;
                                codes.Add(input);
                            }

                            listOfEmployeesByEnteredCourses = ListOfEmployeesByEnteredCourses(employees, courses, codes);

                            intersect = listOfEmployeesByEnteredCourses.First().Value;
                            foreach (KeyValuePair<Course,List<Employee>> KVP in listOfEmployeesByEnteredCourses)
                            {
                                intersect = intersect.Intersect(KVP.Value);
                            }

                           
                            Console.WriteLine($"{"Employees who complted all of the entered courses",75}");
                            PrintDashes();

                            Console.WriteLine($"{"EmployeeNum",-15}{"FirstName",-20}{"Surname",-20}");
                            foreach (var employee in intersect)
                            {
                                PrintDashes();
                                Console.WriteLine($"{employee.EmployeeNum,-15}{employee.FirstName,-20}{employee.Surname,-20}");
                                PrintDashes();
                            }

                            Console.WriteLine();
                      
                            break;
                        }

                    case 6:
                        {
                            List<string> codes = new List<string>();

                            Dictionary<Course, List<Employee>> listOfEmployeesByEnteredCourses = new Dictionary<Course, List<Employee>>();
                            Console.WriteLine("Enter Course Codes (Press enter to stop):");
                            IEnumerable<Employee> set = new List<Employee>();
                            input = Console.ReadLine() ?? string.Empty;
                            codes.Add(input);
                            while (input != string.Empty)
                            {
                                input = Console.ReadLine() ?? string.Empty;
                                codes.Add(input);
                            }
                            
                            listOfEmployeesByEnteredCourses = ListOfEmployeesByEnteredCourses(employees, courses, codes);
                            set = listOfEmployeesByEnteredCourses.First().Value;
                            
                            foreach (KeyValuePair<Course, List<Employee>> KVP in listOfEmployeesByEnteredCourses)
                            {
                                set = set.Union(KVP.Value);
                            }
                            Console.WriteLine($"{"Employees who complted any of the entered courses",75}");
                            PrintDashes();

                            Console.WriteLine($"{"EmployeeNum",-15}{"FirstName",-20}{"Surname",-20}");
                            foreach (Employee employee in set)
                            {
                                PrintDashes();
                                Console.WriteLine($"{employee.EmployeeNum,-15}{employee.FirstName,-20}{employee.Surname,-20}");
                                PrintDashes();
                            }

                            Console.WriteLine();
                            break;
                        }
                    case 7:
                        {
                            string code;

                            Console.Write("Enter course code:");

                            code = Console.ReadLine() ?? string.Empty;

                            DisplayEmployeesWhoDidntCompleteCourse(employees, courses, code);
                            break;
                        }
                    case 8:
                        {
                            Dictionary<Course, List<Employee>> listOfEmployeesByCourse = new Dictionary<Course, List<Employee>>();
                            int completedBy, totalEmployees = employees.Count();
                            double completionPercentage;
                            listOfEmployeesByCourse = ListOfEmployeesByCourse(employees, courses);

                            Console.WriteLine($"{"Skill Breakdown Report",60}");
                            PrintDashes();
                            Console.WriteLine($"{"Course Code", -13}{"Course Description", -27}{"Completed By",-15}" +
                                $"{"Total Employees",-19}{"Completion Percentage", -15}");
                            PrintDashes();
                            foreach (KeyValuePair<Course,List<Employee>> KVP in listOfEmployeesByCourse)
                            {
                                completedBy = KVP.Value.Count();
                                completionPercentage = (double)completedBy / totalEmployees * 100;
                                Console.WriteLine($"{KVP.Key.CourseCode,-13}{KVP.Key.Description,-27}{completedBy,-15}" +
                                    $"{totalEmployees,-19}{completionPercentage + "%",-15}");
                                PrintDashes();
                            }
                            break;
                        }
                    case 9:
                        {
                            Console.WriteLine("Bye!");
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

        public static void DisplayEmployeesAndCourses(List<Employee> employees,List<Course> courses)
        {
            PrintDashes();
            Console.WriteLine($"{"Employees", 52}");
            Console.WriteLine();
            Console.WriteLine($"{"EmployeeNum",-15}{"FirstName",-20}{"Surname",-20}{"CompletedCourse",-40}");
            foreach (Employee employee in employees)
            {
                PrintDashes();
                employee.Print();
                PrintDashes();
            }

            Console.WriteLine("\n\n");

            PrintDashes();
            Console.WriteLine($"{"Courses",51}");

            Console.WriteLine($"{"Course Code",-20}{"Description",-50}{"Credits",-10}");
            foreach (Course course in courses)
            {
                PrintDashes();
                course.Print();
                PrintDashes();
            }
            Console.WriteLine();
            PrintDashes();
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
            DateTime now = DateTime.Now;
            DateTime sixMonth = now.AddMonths(-6);

            Console.WriteLine($"{ "List of employees who has not completed any course in past 6 months",80}\n");
            PrintDashes();
            Console.WriteLine($"{"EmployeeNum",-15}{"FirstName",-20}{"Surname",-20}");

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
                    PrintDashes();
                    Console.WriteLine($"{employee.EmployeeNum,-15}{employee.FirstName,-20}{employee.Surname,-20}");
                    PrintDashes();
                }


            }
            Console.WriteLine();
        }

        public static Dictionary<Course, List<Employee>> ListOfEmployeesByCourse(List<Employee> employees, List<Course> courses)
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
                        if (course.Course == KVP.Key)
                        {
                            KVP.Value.Add(employee);
                        }
                    }
                }
            }


            return dictionary;
        }


        public static Dictionary<Course, List<Employee>> ListOfEmployeesByEnteredCourses(List<Employee> employees, List<Course> courses, List<string> codes)
        {
            Dictionary<Course, List<Employee>> dictionary = new Dictionary<Course, List<Employee>>();


            foreach (Course course in courses)
            {
                foreach (string code in codes)
                {
                    if (course.CourseCode == code)
                    {
                        dictionary.Add(course, new List<Employee>());
                    }
                }
            }

            foreach (KeyValuePair<Course, List<Employee>> KVP in dictionary)
            {
                foreach (Employee employee in employees)
                {
                    foreach (EmployeeCourse course in employee.CompletedCourses)
                    {
                        if (course.Course == KVP.Key)
                        {
                            KVP.Value.Add(employee);
                        }
                    }
                }
            }


            return dictionary;
        }

        public static void DisplayEmployeesWhoDidntCompleteCourse(List<Employee> employees, List<Course> courses, string code)
        {
            Course EnteredCourse = null;
            List<Employee> employesWhoCompletedCourse = new List<Employee>();
            List<Employee> employesWhoDidntCompleteCourse = new List<Employee>();
            foreach (Course course in courses)
            {
                if (course.CourseCode == code)
                {
                    EnteredCourse = course;
                    break;
                }
            }
            if (EnteredCourse == null) {Console.WriteLine("Course not found"); return; }

            foreach (Employee employee in employees)
            {
                foreach (EmployeeCourse course in employee.CompletedCourses)
                {
                    if (course.Course == EnteredCourse)
                    {
                        employesWhoCompletedCourse.Add(employee);
                    }
                }
            }


            employesWhoDidntCompleteCourse = employees.Except(employesWhoCompletedCourse).ToList();

            
            Console.WriteLine($"{"Employees who didn't complete entered course", 70}");
            PrintDashes();
            Console.WriteLine($"{"EmployeeNum",-15}{"FirstName",-20}{"Surname",-20}");
            foreach (Employee employee in employesWhoDidntCompleteCourse)
            {
                PrintDashes();
                Console.WriteLine($"{employee.EmployeeNum,-15}{employee.FirstName,-20}{employee.Surname,-20}");
                PrintDashes();
            }
            Console.WriteLine();
            
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
            Console.WriteLine("5. Search Employees by Completed Courses (AND Condition)");
            Console.WriteLine("6. Search Employees by Completed Courses (OR Condition) ");
            Console.WriteLine("7. Identify Employees Missing a Specific Cours");
            Console.WriteLine("8. Skill Breakdown Report");
            Console.WriteLine("9. Exit");
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine();
        }


        public static void PrintDashes()
        {
            //Console.WriteLine($"{string.Concat(Enumerable.Repeat("-", 15))}|{string.Concat(Enumerable.Repeat("-", 20))}|" +
            //   $"{string.Concat(Enumerable.Repeat("-", 20))}|{string.Concat(Enumerable.Repeat("-", 40))}");

            string dashes = new string('-', 95);
            Console.WriteLine(dashes);


        }

    }
}
