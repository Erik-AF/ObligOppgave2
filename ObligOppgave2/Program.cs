using ObligOppgave2;
using System.Numerics;

//Studenter for tjeneste
Student s = new Student();
Student student1 = new Student("Lisa", "lisa@email.no","passord123","LisaLisa", "lisa123");
Student student2 = new Student("Kåre Student", "kåre@uia.no","kåreerkul","kåremann", "kåre123");


s.Students.Add(student1);

s.Students.Add(student2);

//Ansatte for tjenesten
Employee e = new Employee();
Employee employee1 = new Employee("Chuck", "chuck@email.no","pasord2", "chucktoma", "chuck123", "Fag lærer", "HR");
Employee employee2 = new Employee("Karen", "kar@email.no", "pasord3", "kar", "kar123", "Bibliotek", "HR");

e.Employees.Add(employee1);
e.Employees.Add(employee2);

//Kurs for tjenesten
Course c = new Course();
Course tempCourse = new Course("IS-100", "Digitalisering", 10m, 100);
Course tempCourse1 = new Course("IS-110", "Programmering", 10m, 2);

c.Courses.Add(tempCourse1);
c.Courses.Add(tempCourse);
c.AddStudents(student1, tempCourse1);
s.AddCourses(tempCourse1 ,student1);

Grade testGrade = new Grade(student1, "A");
c.SetGrade(testGrade, tempCourse1);
Grade testGrade2 = new Grade(student1, "C");
c.SetGrade(testGrade2, tempCourse1);


//Bilbiotek for tjenesten
Book b = new Book();
Book tempBook = new Book("bok123", "Martins bok", "Emil", 2023, 10);
b.Books.Add(tempBook);
e.AddBooks(tempBook, employee1);
e.RemoveBooks(tempBook, employee1);
s.AddBooks(tempBook, student2);

Employee employee = new Employee();
Student student = new Student();

bool studentCheck = false;
bool exit = true;
int UserInfo = 0;
int UserRole = 0;

bool loggin = false;
while (loggin == false)
{

    Console.WriteLine("\n[0] Eksisterende bruker");
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
                Console.WriteLine("\nSkriv inn bruker navn:");
                string studentUName = Console.ReadLine();

                Console.WriteLine("\nSkriv inn passord:");
                string studentPassword = Console.ReadLine();

                student = s.CheckLoggin(studentUName, studentPassword);

                if (student.StudentID != null)
                {
                    loggin = true;
                    studentCheck = true;
                    Console.WriteLine($"Du er nå logget inn som {student.Name}");

                }
                else
                {
                    Console.WriteLine("Fant ingen bruker med den kobinasjonen av bruker navn og passord");

                }
                break;

            //Ansatt loggin
            case 1:
                Console.WriteLine("\nSkriv inn bruker navn:");
                string employeeUName = Console.ReadLine();

                Console.WriteLine("\nSkriv inn passord:");
                string employeePassword = Console.ReadLine();

                employee = e.CheckLoggin(employeeUName, employeePassword);

                if (employee.EmployeeID != null)
                {
                    loggin = true;
                    Console.WriteLine($"Du er nå logget inn som {employee.Name}");

                }
                else
                {
                    Console.WriteLine("Fant ingen bruker med den kobinasjonen av bruker navn og passord");

                }
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


        UserRole = int.Parse(Console.ReadLine());

        switch (UserRole)
        {
            //Student
            case 0:
                student = s.CreateUser();
                s.AddStudentsInList(student);
                studentCheck = true;
                loggin = true;
                break;

            //Ansatt
            case 1:
                employee = e.CreateUser();
                e.Employees.Add(employee);
                loggin = true;
                break;
        }
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
            Console.WriteLine("[4] Lever bok");
            Console.WriteLine("[5] Sett karakter");
            Console.WriteLine("[6] Registrer pensum til kurs");
            Console.WriteLine("[7] Avslutt");
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
                    Console.WriteLine("\nListe med bøker i bilblioteket:");
                    foreach (Book books in b.Books)
                    {
                        Console.WriteLine($"Id: {books.Id} Tittel: {books.Title} Forfatter: {books.Author} Publiserings år: {books.Year} Antall kopier: {books.NumbCopies}");
                    }

                    //Søker etter bøker som bruker skriver inn å sjekker om det eksisterer
                    Book bookQuerry = null;
                    bool addChecker3 = false;
                    while (addChecker3 == false)
                    {
                        Console.WriteLine("\nSkriv inn tittelen, forfatteren eller ID på boken:");
                        string addInput1 = Console.ReadLine();

                        bookQuerry = (from books1 in b.Books
                                      where books1.Title == addInput1 || books1.Author == addInput1 || books1.Id == addInput1
                                      select books1).FirstOrDefault();

                        if (bookQuerry != null)
                        {
                            if (bookQuerry.NumbCopies > 0)
                            {
                                bookQuerry.NumbCopies--;
                                addChecker3 = true;
                                e.AddBooks(bookQuerry, employee);
                            }
                            else
                            {
                                Console.WriteLine("Det er ikke flere av denne boken igjen. Vennligst velg en annen.");
                                Console.WriteLine("Eller skriv STOPP for å gå tilbake til menyen");
                            }

                        }
                        else if (addInput1 == "STOPP")
                        {
                            addChecker3 = true;
                        }
                        else
                        {
                            Console.WriteLine($"Fant ikke boken: {addInput1}. Vennligst Skriv inn tittelen, forfatteren eller ID på en av bøkene i listen");
                            Console.WriteLine("Eller skriv STOPP for å gå tilbake til menyen");
                        }

                    }
                    break;
                //Lever bok
                case 4:
                    //Sjekke at den Ansatte har noen bøker på kontoen sin
                    var activeLoans = from sts in b.Books
                                      where employee.Loans.Count > 0
                                      select sts;


                    var activeLoanList = activeLoans.ToList();

                    //Hvis den ansatte har emner på brukeren sin
                    if (activeLoanList.Count() > 0)
                    {
                        Console.WriteLine($"\nListe med bøker på {employee.Name}:");
                        foreach (Book books in activeLoans)
                        {
                            Console.WriteLine($"Id: {books.Id} Tittel: {books.Title} Forfatter: {books.Author} Publiserings år: {books.Year} Antall kopier: {books.NumbCopies}");
                        }

                        //Søker etter bøker som bruker skriver inn å sjekker om det eksisterer
                        Book bookQuerry1 = null;
                        bool addChecker6 = false;
                        while (addChecker6 == false)
                        {
                            Console.WriteLine("\nSkriv inn tittelen, forfatteren eller ID på boken:");
                            string addInput1 = Console.ReadLine();

                            bookQuerry = (from books1 in activeLoans
                                          where books1.Title == addInput1 || books1.Author == addInput1 || books1.Id == addInput1
                                          select books1).FirstOrDefault();

                            if (bookQuerry != null)
                            {

                                bookQuerry.NumbCopies++;
                                addChecker6 = true;
                                e.RemoveBooks(bookQuerry, employee);


                            }
                            else if (addInput1 == "STOPP")
                            {
                                addChecker3 = true;
                            }
                            else
                            {
                                Console.WriteLine($"Fant ikke boken: {addInput1}. Vennligst Skriv inn tittelen, forfatteren eller ID på en av bøkene i listen");
                                Console.WriteLine("Eller skriv STOPP for å gå tilbake til menyen");
                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine("Denne personen har ingen bøker koblet til seg");
                    }
                    break;
                // Sett karakter
                case 5:

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
                                // Kan brukes for å vise kurs info c.ShowCourses();
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
                case 6:
                    Console.WriteLine("\nListe med aktive Kurs:");
                    foreach (Course courses in c.Courses)
                    {
                        Console.WriteLine($"Navn: {courses.Name} Kode: {courses.Code} Studiepoeng: {courses.Points} Antall plasser: {courses.Capacity}");
                    }

                    //Søker etter kurs som bruker skriver inn å sjekker om det eksisterer
                    Course courseQuerry1 = null;
                    bool addChecker1 = false;
                    while (addChecker1 == false)
                    {
                        Console.WriteLine("\nSkriv inn navnet eller koden på kurset:");
                        string addInput1 = Console.ReadLine();

                        courseQuerry1 = (from courses1 in c.Courses
                                         where courses1.Name == addInput1 || courses1.Code == addInput1
                                         select courses1).FirstOrDefault();

                        if (courseQuerry1 != null)
                        {
                            addChecker1 = true;
                        }
                        else if (addInput1 == "STOPP")
                        {
                            addChecker1 = true;
                        }
                        else
                        {
                            Console.WriteLine($"Fant ikke kurset: {addInput1}. Vennligst skriv inn navn eller kode på en av kursene i listen");
                            Console.WriteLine("Eller skriv STOPP for å gå tilbake til menyen");
                        }

                    }


                    Console.WriteLine("\nListe med bøker i bilblioteket:");
                    foreach (Book books in b.Books)
                    {
                        Console.WriteLine($"Id: {books.Id} Tittel: {books.Title} Forfatter: {books.Author} Publiserings år: {books.Year} Antall kopier: {books.NumbCopies}");
                    }

                    //Søker etter bøker som bruker skriver inn å sjekker om det eksisterer
                    Book MediumQuerry = null;
                    bool addChecker4 = false;
                    while (addChecker4 == false)
                    {
                        Console.WriteLine("\nSkriv inn tittelen, forfatteren eller ID på boken:");
                        string addInput1 = Console.ReadLine();

                        MediumQuerry = (from books1 in b.Books
                                        where books1.Title == addInput1 || books1.Author == addInput1 || books1.Id == addInput1
                                        select books1).FirstOrDefault();

                        if (MediumQuerry != null)
                        {
                            if (MediumQuerry.NumbCopies > 0)
                            {
                                addChecker4 = true;
                                c.AddBook(MediumQuerry, courseQuerry1);
                            }
                            else
                            {
                                Console.WriteLine("Det er ikke flere av denne boken igjen. Vennligst velg en annen.");
                                Console.WriteLine("Eller skriv STOPP for å gå tilbake til menyen");
                            }

                        }
                        else if (addInput1 == "STOPP")
                        {
                            addChecker4 = true;
                        }
                        else
                        {
                            Console.WriteLine($"Fant ikke boken: {addInput1}. Vennligst Skriv inn tittelen, forfatteren eller ID på en av bøkene i listen");
                            Console.WriteLine("Eller skriv STOPP for å gå tilbake til menyen");
                        }

                    }
                    break;

                case 7:
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
else if (employee.Posistion == "Bibliotek")
{
    while (exit == true)
    {
        try
        {
            Console.WriteLine("\n[0] Registrer bok");
            Console.WriteLine("[1] Se aktive lån");
            Console.WriteLine("[2] Se låne historik");
            Console.WriteLine("[3] Avslutt");
            int emp_choise = int.Parse(Console.ReadLine());

            switch (emp_choise)
            {
                //Registrere bok
                case 0:
                    b.CreateMedium();
                    break;

                //Se aktive lån
                case 1:
                    Console.WriteLine("Liste med aktive lån:");
                    foreach (Employee emp in e.Employees)
                    {
                        //Sjekke at den Ansatte har noen bøker på kontoen sin
                        var activeLoans = from sts in b.Books
                                          where emp.Loans.Count > 0
                                          select sts;


                        var activeLoanList = activeLoans.ToList();

                        //Hvis den ansatte har bøker på brukeren sin
                        if (activeLoanList.Count() > 0)
                        {
                            Console.WriteLine($"\nListe med bøker på {emp.Name}:");
                            foreach (Book books in activeLoans)
                            {
                                Console.WriteLine($"Id: {books.Id} Tittel: {books.Title} Forfatter: {books.Author} Publiserings år: {books.Year} Antall kopier: {books.NumbCopies}");
                            }
                        }
                    }
                    foreach (Student std in s.Students)
                    {
                        //Sjekke at den Ansatte har noen bøker på kontoen sin
                        var activeLoans = from sts in b.Books
                                          where std.Loans.Count > 0
                                          select sts;


                        var activeLoanList = activeLoans.ToList();

                        //Hvis den ansatte har bøker på brukeren sin
                        if (activeLoanList.Count() > 0)
                        {
                            Console.WriteLine($"\nListe med bøker på {std.Name}:");
                            foreach (Book books in activeLoans)
                            {
                                Console.WriteLine($"Id: {books.Id} Tittel: {books.Title} Forfatter: {books.Author} Publiserings år: {books.Year} Antall kopier: {books.NumbCopies}");
                            }
                        }
                    }

                    break;

                //Se på låne historikk
                case 2:
                    Console.WriteLine("Låne historik for ansatte:");
                    foreach (Employee emp in e.Employees)
                    {
                        var activeLoans = from sts in b.Books
                                          where emp.LoanHistory.Count > 0
                                          select sts;


                        var activeLoanList = activeLoans.ToList();

                        if (activeLoanList.Count() > 0)
                        {
                            e.ShowLoanHistoryEmp(emp);
                        }

                    }
                    Console.WriteLine("\nLåne historik for studenter:");
                    foreach (Student std in s.Students)
                    {
                        //Sjekke at den Ansatte har noen bøker på kontoen sin
                        var activeLoans = from sts in b.Books
                                          where std.LoanHistory.Count > 0
                                          select sts;
                        var activeLoanList = activeLoans.ToList();


                        if (activeLoanList.Count() > 0)
                        {
                            s.ShowLoanHistorySt(std);
                        }

                    }

                    break;
                case 3:
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
//Student view
else if (UserInfo == 0 && studentCheck == true || UserInfo == 1 && UserRole == 0 && studentCheck == true)
{
    while (exit == true)
    {
        try
        {
            Console.WriteLine("\n[0] Melde på kurs");
            Console.WriteLine("[1] Melde av kurs");
            Console.WriteLine("[2] Se karakterer");
            Console.WriteLine("[3] Søk på bøker");
            Console.WriteLine("[4] Lån bøker");
            Console.WriteLine("[5] Se på kurs du er meldt på.");
            Console.WriteLine("[6] Avslutt");
            int emp_choise = int.Parse(Console.ReadLine());

            switch (emp_choise)
            {
                //Melde student opp til kurs
                case 0:
                    Console.WriteLine("\nListe med aktive Kurs:");
                    foreach (Course courses in c.Courses)
                    {
                        Console.WriteLine($"Navn: {courses.Name} Kode: {courses.Code} Studiepoeng: {courses.Points} Antall plasser: {courses.Capacity}");
                    }

                    //Søker etter kurs som bruker skriver inn å sjekker om det eksisterer
                    Course courseQuerry = null;
                    bool addChecker1 = false;
                    while (addChecker1 == false)
                    {
                        Console.WriteLine("\nSkriv inn navnet eller koden på kurset:");
                        string addInput1 = Console.ReadLine();

                        courseQuerry = (from courses1 in c.Courses
                                        where courses1.Name == addInput1 || courses1.Code == addInput1
                                        select courses1).FirstOrDefault();

                        if (courseQuerry != null)
                        {
                            if (courseQuerry.Capacity > 0)
                            {
                                courseQuerry.Capacity--;
                                addChecker1 = true;
                                c.AddStudents(student, courseQuerry);
                                s.AddCourses(courseQuerry, student);
                            }
                            else
                            {
                                Console.WriteLine("Det er ikke flere ledige plasser på dette kurset. Vennligst velg et annet.");
                            }

                        }
                        else if (addInput1 == "STOPP")
                        {
                            addChecker1 = true;
                        }
                        else
                        {
                            Console.WriteLine($"Fant ikke kurset: {addInput1}. Vennligst skriv inn navn eller kode på en av kursene i listen");
                            Console.WriteLine("Eller skriv STOPP for å gå tilbake til menyen");
                        }

                    }
                    break;
                //Melde av kurs
                case 1:
                    //Sjekke at studenten har kurs på brukeren sin

                    var activeCourse = from sts in c.Courses
                                       where student.Courses.Contains(sts.Code)
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
                        Course courseQuerry9 = null;
                        bool addChecker2 = false;
                        while (addChecker2 == false)
                        {
                            Console.WriteLine("\nSkriv inn navnet eller koden på kurset:");
                            string addInput1 = Console.ReadLine();

                            courseQuerry9 = (from courses1 in activeCourse
                                            where courses1.Name == addInput1 || courses1.Code == addInput1
                                            select courses1).FirstOrDefault();

                            if (courseQuerry9 != null)
                            {
                                courseQuerry9.Capacity++;
                                addChecker2 = true;

                            }
                            else
                            {
                                Console.WriteLine($"Fant ikke kurset: {addInput1}. Vennligst skriv inn navn eller kode på en av kursene i listen");
                            }

                        }
                        c.RemoveStudents(student, courseQuerry9);
                        s.RemoveCorses(courseQuerry9, student);
                    }
                    //Hvis personen ikke har noen emner på brukeren sin
                    else
                    {
                        Console.WriteLine("Personen har ingen aktive kurs på brukeren sin");
                        break;
                    }


            break;
                //Se karatker
                case 2:
                    c.ShowGrade(student);
                    break;
                //Søk på bøker
                case 3:
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
                //Lån bøker
                case 4:
                    Console.WriteLine("\nListe med bøker i bilblioteket:");
                    foreach (Book books in b.Books)
                    {
                        Console.WriteLine($"Id: {books.Id} Tittel: {books.Title} Forfatter: {books.Author} Publiserings år: {books.Year} Antall kopier: {books.NumbCopies}");
                    }

                    //Søker etter bøker som bruker skriver inn å sjekker om det eksisterer
                    Book bookQuerry = null;
                    bool addChecker3 = false;
                    while (addChecker3 == false)
                    {
                        Console.WriteLine("\nSkriv inn tittelen, forfatteren eller ID på boken:");
                        string addInput1 = Console.ReadLine();

                        bookQuerry = (from books1 in b.Books
                                      where books1.Title == addInput1 || books1.Author == addInput1 || books1.Id == addInput1
                                      select books1).FirstOrDefault();

                        if (bookQuerry != null)
                        {
                            if (bookQuerry.NumbCopies > 0)
                            {
                                bookQuerry.NumbCopies--;
                                addChecker3 = true;
                                s.AddBooks(bookQuerry, student);
                            }
                            else
                            {
                                Console.WriteLine("Det er ikke flere av denne boken igjen. Vennligst velg en annen.");
                                Console.WriteLine("Eller skriv STOPP for å gå tilbake til menyen");
                            }

                        }
                        else if (addInput1 == "STOPP")
                        {
                            addChecker3 = true;
                        }
                        else
                        {
                            Console.WriteLine($"Fant ikke boken: {addInput1}. Vennligst Skriv inn tittelen, forfatteren eller ID på en av bøkene i listen");
                            Console.WriteLine("Eller skriv STOPP for å gå tilbake til menyen");
                        }

                    }
                    break;
                // Se på kurs du er meldt på
                case 5:
                    var activeCourse6 = from sts in c.Courses
                                       where student.Courses.Contains(sts.Code)
                                       select sts;

                    var activeCourseList1 = activeCourse6.ToList();

                    //Hvis studenten har emner på brukeren sin
                    if (activeCourseList1.Count() > 0)
                    {

                        //Viser Liste med kurs på studenen
                        Console.WriteLine($"\nListe med aktive kurs på :{student.Name}");
                        foreach (Course courses in activeCourse6)
                        {
                            Console.WriteLine($"Navn: {courses.Name} Kode: {courses.Code} Studiepoeng: {courses.Points} Antall plasser: {courses.Capacity}");
                        }

                    }
                        break;
                //Avslutt
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
