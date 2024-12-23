using PRN212_Q11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


internal class Program
{
    static void Main(string[] args)
    {
        // Create authors
        Author author1 = new Author(1, "Jane Austen");
        Author author2 = new Author(2, "Mark Twain");

        // Create books
        Book book1 = new Book(1, "Pride and Prejudice", author1);
        Book book2 = new Book(2, "Emma", author1);
        Book book3 = new Book(3, "The Adventures of Tom Sawyer", author2);

        // Create library
        Library<Book> bookLibrary = new Library<Book>();

        // Add books to library
        bookLibrary.Add(book1);
        bookLibrary.Add(book2);
        bookLibrary.Add(book3);

        // Display books
        Console.WriteLine("Books in the library:");
        foreach (var book in bookLibrary.GetAll())
        {
            Console.WriteLine($"{book.Title} by {book.Author.Name}");
        }
    }
}