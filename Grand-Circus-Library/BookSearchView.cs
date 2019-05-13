using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grand_Circus_Library
{
    class BookSearchView : IView
    {
        //menu for different ways to search for a book
        public void Display()
        {
            Console.Clear();
            Console.WriteLine("1. Search All Books");
            Console.WriteLine("2. Search by Title");
            Console.WriteLine("3. Search by Author");
            Console.WriteLine("4. Search by Genre");
            Console.WriteLine("5. Search by Dewey Decimal System");
            Console.Write("How can we help you? Select 1-5 or (C)ancel: ");
        }
    }
}
