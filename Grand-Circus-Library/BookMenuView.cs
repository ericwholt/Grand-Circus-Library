using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grand_Circus_Library
{
    class BookMenuView : IView
    {
        //main menu of library
        public void Display()
        {
            Console.WriteLine("1. Search for a book");
            Console.WriteLine("2. Check out a book");
            Console.WriteLine("3. Return a book");
            Console.Write("How can we help you? Select 1-3: ");
        }
    }
}
