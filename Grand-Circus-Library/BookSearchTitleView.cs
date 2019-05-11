using System;

namespace Grand_Circus_Library
{
    class BookSearchTitleView : IView
    {
        public void Display()
        {
            Console.Clear();
            Console.Write("Search by title of the book: ");
        }
    }
}
