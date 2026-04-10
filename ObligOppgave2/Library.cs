using System;
using System.Collections.Generic;
using System.Text;

namespace ObligOppgave2;

public class Book
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public int NumbCopies { get; set; }
    public DateTime Loaned { get; set; }
    public DateTime Submitted { get; set; }
    public List<Book> Books { get; set; } = new();

    public Book() { }

    public Book(string id, string title, string author, int year, int numbCopies)
    {
        Id = id;
        Title = title;
        Author = author;
        Year = year;
        NumbCopies = numbCopies;
    }




    public void CreateMedium()
    {
        Console.WriteLine("Opprett en ny bok:");

        Console.WriteLine("\nHva er ID til boken: ");
        string id = Console.ReadLine();

        Console.WriteLine("\nHva er tittelen til boken: ");
        string title = Console.ReadLine();

        Console.WriteLine("\nHvem er forfatteren: ");
        string author = Console.ReadLine();

        Console.WriteLine("\nHvilket år ble boken publisert: ");
        int year = int.Parse(Console.ReadLine());

        Console.WriteLine("\nHvor mange eksemplarer er det: ");
        int numbCopies = int.Parse(Console.ReadLine());


        Book b = new Book(id, title, author, year, numbCopies);

        Books.Add(b);
    }


}