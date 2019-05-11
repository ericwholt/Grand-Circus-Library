using System;

namespace Grand_Circus_Library
{
    class BookSearchAuthorView : IView
    {
        public void Display()
        {
            Console.Clear();
            Console.Write("Author of the book: ");
        }
    }
}
