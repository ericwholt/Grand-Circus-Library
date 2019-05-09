using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grand_Circus_Library
{
    class Program
    {
        static void Main(string[] args)
        {
            Book b = new Book();
            b.Title = "Clifford the Big Red Dog";
            b.Author = "Norman Bridwell";
            b.Genre = "Children";
            b.Publisher = "Scholastic";
            b.Status = true;
            b.Synopsis = "Clifford, the big red dog / story and pictures by Norman Bridwell. Emily Elizabeth describes the activities she enjoys with her very big, very red dog and how they take care of each other. Clifford is the biggest, reddest dog on Emily Elizabeth's street, and he makes a perfect watchdog!";
            
            BookView bv = new BookView(b);
            bv.Display();
            Console.WriteLine();
            Console.WriteLine("Book List:");
            List<Book> bookList = new List<Book>() { b };
            BookListView blv = new BookListView(bookList);
            blv.Display();
        }
    }
}
