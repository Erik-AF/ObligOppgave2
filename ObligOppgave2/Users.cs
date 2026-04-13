using ObligOppgave2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace ObligOppgave2;

public class Users
{
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Book> Loans { get; set; } = new();
    public List<String> LoanHistory { get; set; } = new();

    public Users()
    {

    }

    protected Users(string name, string email, string password, string username)
    {
        Name = name;
        Email = email;
        UserName = username;
        Password = password;
    }
    public virtual void ShowStudents()
    {
        //Skal vise Alle studentene
    }

    public virtual Users CreateUser()
    {
        return new Users();
    }

    public virtual Users CheckLoggin(string uname, string password)
    {

        return new Users();
    }

}

public class Student : Users
    {
        public string StudentID { get; }
        public List<Student> Students { get; set; } = new List<Student>();

        public List<Grade> Grades { get; set; } = new();
        public List<String> Courses { get; set; } = new();



        public Student()
        {

        }

        public Student(string name, string email, string password, string username, string studentID) : base(name, email, password, username)
        {
            StudentID = studentID;
        }


        public override Student CreateUser()
        {
            Console.WriteLine("Skriv inn ditt fulle navn:");
            string fullName = Console.ReadLine();

            Console.WriteLine("Skriv inn epost:");
            string email = Console.ReadLine();

            Console.WriteLine("Skriv inn bruker navn:");
            string userName = Console.ReadLine();

            Console.WriteLine("Skriv inn ditt passord:");
            string password = Console.ReadLine();

            string id = userName + Random.Shared.Next(100, 1000).ToString();

            Student s = new Student(fullName, email, password, userName, id);

            return s;

        }
        public override Student CheckLoggin(string uname, string password)
        {
            var studentQuerry = (from students in Students
                                 where students.Password == password && students.UserName == uname
                                 select students).FirstOrDefault();

        if (studentQuerry != null)
        {
            return studentQuerry;
        }
        else
        {
            Student wrongS = new Student();
            return wrongS;
        }


        }

        public void AddStudentsInList(Student student)
        {
            Students.Add(student);
        }

        public override void ShowStudents()
        {
            foreach (Student student in Students)
            {
                string niceCr = string.Join(", ", student.Courses);
                Console.WriteLine($"Navn: {student.Name} Epost: {student.Email} Kurs: {niceCr}");

            }
        }

        public void AddCourses(Course course, Student student)
        {
            student.Courses.Add(course.Code);
        }

        public void RemoveCorses(Course course, Student student)
        {
            student.Courses.Remove(course.Code);
        }

        public void AddBooks(Book book, Student student)
        {
            book.Loaned = DateTime.Now;
            string histroy = $"{book.Id}, {book.Title} {book.Author} {book.Year} Lånt: {book.Loaned}";


            student.Loans.Add(book);
            student.LoanHistory.Add(histroy);
        }
        public void RemoveBooks(Book book, Student student)
        {
            book.Submitted = DateTime.Now;
            string histroy = ($"{book.Id}, {book.Title} {book.Author} {book.Year} Lånt: {book.Loaned} Levert: {book.Submitted}");

            student.Loans.Remove(book);
            student.LoanHistory.Add(histroy);
        }

        public void ShowLoanHistorySt(Student student)
        {
            Console.WriteLine($"Låne historiken til {student.Name}:");
            foreach (var book in student.LoanHistory)
            {
                Console.WriteLine(book);
            }


        }
    }
    class InterStudent : Users
    {
        string UniversityOrigin { get; set; }
        string Country { get; set; }
        DateOnly StartDate { get; set; }
        DateOnly EndDate { get; set; }
        public List<InterStudent> InterStudents { get; set; } = new List<InterStudent>();

        public InterStudent(string name, string email, string password, string username, string uniOrigin, string country, DateOnly start, DateOnly end) : base(name, email, password, username)
        {
            UniversityOrigin = uniOrigin;
            Country = country;
            StartDate = start;
            EndDate = end;

        }
        public void AddInterStudents(InterStudent student)
        {
            InterStudents.Add(student);
        }

        public override void ShowStudents()
        {
            foreach (InterStudent student in InterStudents)
            {

                Console.WriteLine($"Navn: {student.Name} Epost: {student.Email}");
            }
        }
    }
    class Employee : Users
    {
        public string EmployeeID { get; set; }
        public string Posistion { get; set; }
        public string Department { get; set; }
        public List<Employee> Employees { get; set; } = new();

        public Employee() { }
        public Employee(string name, string email, string password, string username, string employeeID, string position, string department) : base(name, email, password, username)
        {
            EmployeeID = employeeID;
            Posistion = position;
            Department = department;
        }

        public override Employee CheckLoggin(string uname, string password)
        {
            var empQuerry = (from employee in Employees
                                 where employee.Password == password && employee.UserName == uname
                                 select employee).FirstOrDefault();

            if (empQuerry != null)
            {
                return empQuerry;
            }
            else
            {
                Employee wrongS = new Employee();
                return wrongS;
            }


        }
    public override Employee CreateUser()
        {
            Console.WriteLine("Skriv inn ditt fulle navn:");
            string fullName = Console.ReadLine();

            Console.WriteLine("Skriv inn epost:");
            string email = Console.ReadLine();

            Console.WriteLine("Skriv inn avdelingen:");
            string department = Console.ReadLine();

            string role = "";
            bool addChecker = false;
            int roleNum;

            while (addChecker == false)
            {
                Console.WriteLine("Velg Rolle:");
                Console.WriteLine("[0] Fag lærer");
                Console.WriteLine("[1] Biliotek");
                try
                {
                    roleNum = int.Parse(Console.ReadLine());
                    if (roleNum == 0)
                    {
                        role = "Fag lærer";
                        addChecker = true;
                    }
                    else if (roleNum == 1)
                    {
                        role = "Bibliotek";
                        addChecker = true;
                    }

                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Du må skrive inn et tall");
                }


            }

            Console.WriteLine("Skriv inn bruker navn:");
            string userName = Console.ReadLine();

            Console.WriteLine("Skriv inn ditt passord:");
            string password = Console.ReadLine();

            string id = userName + Random.Shared.Next(100, 1000).ToString();

            Employee e = new Employee(fullName, email, password, userName, id, role, department);

            return e;

        }

        public void AddBooks(Book book, Employee employee)
        {
            book.Loaned = DateTime.Now;
            string histroy = $"{book.Id}, {book.Title} {book.Author} {book.Year} Lånt: {book.Loaned}";

            employee.Loans.Add(book);
            employee.LoanHistory.Add(histroy);
        }
        public void RemoveBooks(Book book, Employee employee)
        {
            book.Submitted = DateTime.Now;
            string histroy = ($"{book.Id}, {book.Title} {book.Author} {book.Year} Lånt: {book.Loaned} Levert: {book.Submitted}");

            employee.Loans.Remove(book);
            employee.LoanHistory.Add(histroy);
        }

        public void ShowLoanHistoryEmp(Employee employee)
        {
            Console.WriteLine($"Låne historiken til {employee.Name}:");
            foreach (var book in employee.LoanHistory)
            {
                Console.WriteLine(book);
            }


        }
    }
