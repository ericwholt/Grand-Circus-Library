using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grand_Circus_Library
{
    class BookView : IView
    {
        //Property to store book passed through the constructor
        public Book Book { get; set; }

        //Must have a book to display
        public BookView(Book Book)
        {
            this.Book = Book;//Put book into property
        }

        //Book information
        public void Display()
        {
            Console.WriteLine();
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Title: "+Book.Title);
            Console.WriteLine("Author: " + Book.Author);
            Console.WriteLine("Genre: " + Book.Genre);
            Console.WriteLine("Publisher: " + Book.Publisher);
            Console.WriteLine($"Status: {(Book.Status ? "On Shelf" : "Checked Out")}"); //Ternary to display On Shelf if true or Checked Out if false
            Console.WriteLine("Call Number: " + Book.DeweySystem);
            Console.WriteLine("Synopsis: " + Book.Synopsis);
            Console.WriteLine("------------------------------------");
        }
    }
}
