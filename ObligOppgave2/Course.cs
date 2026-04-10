using System;
using System.Collections.Generic;
using System.Text;

using System;
using System.Collections.Generic;
using System.Text;

namespace ObligOppgave2;

public class Grade
{
    public Student Student { get; set; }
    public string StudentGrade { get; set; }

    public Grade() { }

    public Grade(Student student, string studentGrade)
    {
        Student = student;
        StudentGrade = studentGrade;
    }


}

public class Course
{
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal Points { get; set; }
    public int Capacity { get; set; }
    public Grade Grade { get; set; }
    public List<Course> Courses { get; set; } = new();
    public List<String> Participants { get; set; } = new();


    public Course()
    {

    }
    public Course(string code, string name, decimal points, int capacity)
    {
        Code = code;
        Name = name;
        Points = points;
        Capacity = capacity;

    }

    public void CreateCourse()
    {
        Console.WriteLine("Opprett et nytt kurs:");

        Console.WriteLine("\nHva er navnet til kurset: ");
        string name = Console.ReadLine();

        Console.WriteLine("\nHva er koden til kurset: ");
        string code = Console.ReadLine();

        Console.WriteLine("\nHvor mange poeng er kurset: ");
        decimal points = Decimal.Parse(Console.ReadLine());

        Console.WriteLine("\nHvor mange plasser er det i kurset: ");
        int capacity = int.Parse(Console.ReadLine());


        Course c = new Course(name, code, points, capacity);

        Courses.Add(c);
    }
    public void AddStudents(Student student, Course course)
    {
        course.Participants.Add(student.StudentID);
    }

    public void RemoveStudents(Student student, Course course)
    {
        course.Participants.Remove(student.StudentID);
    }

    public void ShowCourses()
    {
        foreach (Course cr in Courses)
        {
            string niceST = string.Join(", ", cr.Participants);
            Console.WriteLine($"Navn: {cr.Name} Code: {cr.Code} Studiepoeng: {cr.Points} Antall plasser: {cr.Capacity} Deltagere: {niceST} Students Karakterer: {cr.Grade.Student.Name} - {cr.Grade.StudentGrade}");

        }
    }
    public void SetGrade(Grade grade, Course course)
    {
        course.Grade = grade;
    }

}
