using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Xunit;

namespace ObligOppgave2
{
    public class TestKurs
    {
        [Fact]
        public void CheckIfCourseFull()
        {
            Course tempCourse1 = new Course("IS-110", "Programmering", 10m, 2);
            Student student2 = new Student("Kåre Student", "kåre@uia.no", "kåreerkul", "kåremann", "kåre123");

            tempCourse1.AddStudents(student2, tempCourse1);
            tempCourse1.Capacity--;
            Assert.Equal(1, tempCourse1.Capacity);

        }
        [Fact]

        public void CheckforMultiCourseOnStudent()
        {
            Course tempCourse1 = new Course("IS-110", "Programmering", 10m, 2);
            Student student2 = new Student("Kåre Student", "kåre@uia.no", "kåreerkul", "kåremann", "kåre123");
            tempCourse1.AddStudents(student2, tempCourse1);

            var exception = Assert.Throws<InvalidOperationException>(() => tempCourse1.AddStudents(student2, tempCourse1));

            Assert.Equal("Studenten er allerede meldt på kurset", exception.Message);

        }

        [Fact]
        public void CheckStudentCourse()
        {
            Course tempCourse1 = new Course("IS-110", "Programmering", 10m, 2);
            Student student2 = new Student("Kåre Student", "kåre@uia.no", "kåreerkul", "kåremann", "kåre123");
            Student student1 = new Student("Lisa", "lisa@email.no", "passord123", "LisaLisa", "lisa123");
            tempCourse1.AddStudents(student1 , tempCourse1);

            var exception = Assert.Throws<InvalidOperationException>(() => tempCourse1.RemoveStudents(student2, tempCourse1));

            Assert.Equal("Studenten er ikke påmeldt kurset", exception.Message);


        }
    }
}
