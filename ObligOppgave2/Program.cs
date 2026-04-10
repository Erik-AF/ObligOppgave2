using ObligOppgave2;
using System.Numerics;

//Studenter for tjeneste
Student s = new Student();
Student student1 = new Student("Lisa", "lisa@email.no","passord123","LisaLisa", "lisa123");
Student student2 = new Student("Kåre", "kåre@uia.no","kåreerkul","kåremann", "kåre123");

s.Students.Add(student1);
s.Students.Add(student2);

//Ansatte for tjenesten
Employee e = new Employee();
Employee employee1 = new Employee("Chuck", "chuck@email.no","pasord2", "chucktoma", "chuck123", "Leder", "HR");

e.Employees.Add(employee1);


//Kurs for tjenesten
Course c = new Course();
Course tempCourse = new Course("IS-100", "Digitalisering", 10m, 100);
Course tempCourse1 = new Course("IS-110", "Programmering", 10m, 2);

c.Courses.Add(tempCourse1);
c.Courses.Add(tempCourse);

c.AddStudents(student1, tempCourse1);
s.AddCourses(tempCourse1 ,student1);

//Bilbiotek for tjenesten
Book b = new Book();
Book tempBook = new Book("bok123", "Martins bok", "Emil", 2023, 10);
b.Books.Add(tempBook);

Employee employee = new Employee();
Student student = new Student();

bool studentCheck = false;
bool exit = true;


int UserInfo;
Console.WriteLine("[0] Eksisterende bruker");
Console.WriteLine("[1] Ny bruker");
UserInfo = int.Parse(Console.ReadLine());

//Eksisterende bruker
if (UserInfo == 0)
{
    Console.WriteLine("Velg rollen din:");
    Console.WriteLine("[0] Student");
    Console.WriteLine("[1] Ansatt");
    int role = int.Parse(Console.ReadLine());

    switch (role)
    {
        //Student Loggin
        case 0:

            break;

        //Ansatt loggin
        case 1:

            break;
    }


}

//Ny bruker
else if (UserInfo == 1)
{

    Console.WriteLine("\nOpprett ny bruker");
    Console.WriteLine("Velg rollen din:");
    Console.WriteLine("[0] Student");
    Console.WriteLine("[1] Ansatt");


    int UserRole = int.Parse(Console.ReadLine());

    switch (UserRole)
    {
        //Student
        case 0:
            student = s.CreateUser();
            s.AddStudentsInList(student);
            studentCheck = true;    

            break;

        //Ansatt
        case 1:
            employee = e.CreateUser();
            e.Employees.Add(employee);
            break;
    } 
}

 //Faglærer view
 if (employee.Posistion == "Fag lærer")
            {
                while (exit == true)
                {
                    try
                    {
                        Console.WriteLine("\n[0] Opprett kurs");
                        Console.WriteLine("[1] Søke på kurs");
                        Console.WriteLine("[2] Søk på bøker");
                        Console.WriteLine("[3] Lån bok");
                        Console.WriteLine("[4] Sett karakter");
                        Console.WriteLine("[5] Registrer pensum til kurs");
                        Console.WriteLine("[6] Avslutt");
                        int emp_choise = int.Parse(Console.ReadLine());
                        switch (emp_choise)
                        {
                            //Opprett kurs
                            case 0:
                                c.CreateCourse();
                                break;
                            //Søk etter kurs
                            case 1:
                                Console.WriteLine("Søk etter kurs på Kode og Navn");
                                string userSearch = Console.ReadLine();

                                var searchCourse = from search in c.Courses
                                                   where search.Code.Contains(userSearch) || search.Name.Contains(userSearch)
                                                   select search;

                                var searchList = searchCourse.ToList();

                                if (searchList.Count() > 0)
                                {
                                    foreach (Course sCourse in searchCourse)
                                    {
                                        Console.WriteLine($"Navn: {sCourse.Name} Kode: {sCourse.Code} Studiepoeng: {sCourse.Points} Antall plasser: {sCourse.Capacity}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Fant ingen kurs som stemmet med søket ditt: {userSearch}");
                                }
                                break;
                            //Søk etter bok
                            case 2:
                                Console.WriteLine("Søk etter bok på Tittel, Forfatter eller ID:");
                                string bookSearch = Console.ReadLine();

                                var searchBook = from search in b.Books
                                                 where search.Title.Contains(bookSearch) || search.Author.Contains(bookSearch) || search.Id.Contains(bookSearch)
                                                 select search;

                                var bookList = searchBook.ToList();

                                if (bookList.Count() > 0)
                                {
                                    foreach (Book sBook in searchBook)
                                    {
                                        Console.WriteLine($"Id: {sBook.Id} Tittel: {sBook.Title} Forfatter: {sBook.Author} Publiserings år: {sBook.Year} Antall kopier: {sBook.NumbCopies}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Fant ingen bok som stemmet med søket ditt: {bookSearch}");
                                }
                                break;
                            //lån bok
                            case 3:
                                break;
                            // Sett karakter
                            case 4:

                                foreach (Student students in s.Students)
                                {
                                    Console.WriteLine($"Navn: {students.Name} E-post: {students.Email} StudentID: {students.StudentID}");
                                }

                                Student studentQuerry = null;
                                bool addChecker = false;
                                while (addChecker == false)
                                {
                                    Console.WriteLine("\nSkriv navnet på studenten:");
                                    string addInput = Console.ReadLine();

                                    studentQuerry = (from students in s.Students
                                                     where students.Name == addInput
                                                     select students).FirstOrDefault();

                                    if (studentQuerry != null)
                                    {
                                        addChecker = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Fant ikke studenten {addInput}. Vennligst skriv navn på en student som er i listen");
                                    }
                                }
                                //Sjekke at studenten har kurs på brukeren sin

                                var activeCourse = from sts in c.Courses
                                                   where studentQuerry.Courses.Contains(sts.Code)
                                                   select sts;

                                var activeCourseList = activeCourse.ToList();

                                //Hvis studenten har emner på brukeren sin
                                if (activeCourseList.Count() > 0)
                                {

                                    //Viser Liste med kurs på studenen
                                    Console.WriteLine("\nListe med aktive kurs på studenten:");
                                    foreach (Course courses in activeCourse)
                                    {
                                        Console.WriteLine($"Navn: {courses.Name} Kode: {courses.Code} Studiepoeng: {courses.Points} Antall plasser: {courses.Capacity}");
                                    }

                                    //Søker etter kurs som bruker skriver inn å sjekker om det eksisterer
                                    Course courseQuerry = null;
                                    bool addChecker2 = false;
                                    while (addChecker2 == false)
                                    {
                                        Console.WriteLine("\nSkriv inn navnet eller koden på kurset:");
                                        string addInput1 = Console.ReadLine();

                                        courseQuerry = (from courses1 in activeCourse
                                                        where courses1.Name == addInput1 || courses1.Code == addInput1
                                                        select courses1).FirstOrDefault();

                                        if (courseQuerry != null)
                                        {
                                            Console.WriteLine("\nVelg karakter for studenten fra A-F");
                                            string userGrade = Console.ReadLine().ToUpper();

                                            Grade newGrade = new Grade(studentQuerry, userGrade);
                                            c.SetGrade(newGrade, courseQuerry);
                                            c.ShowCourses();
                                            addChecker2 = true;

                                        }
                                        else
                                        {
                                            Console.WriteLine($"Fant ikke kurset: {addInput1}. Vennligst skriv inn navn eller kode på en av kursene i listen");
                                        }

                                    }
                                }
                                //Hvis personen ikke har noen emner på brukeren sin
                                else
                                {
                                    Console.WriteLine("Personen har ingen aktive kurs på brukeren sin");
                                    break;
                                }

                                break;
                            //Registrer pensumbok
                            case 5:
                                break;
                            case 6:
                                exit = false;
                                break;

                        }
                    }
                    catch (System.FormatException)
                    {
                        Console.WriteLine("Du må skrive inn et tall");
                    }

                }
            }
//Bibliotek view
else if (employee.Posistion == "Biliotek")
{

}
//Student view
else if (UserInfo == 1 && studentCheck == true)
{

}

