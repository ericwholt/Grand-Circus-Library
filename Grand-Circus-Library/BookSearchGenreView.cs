using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grand_Circus_Library
{
  class BookSearchGenreView : IView
  {
    public void Display()
    {
            Console.Clear();
      Console.Write("Genre of the book: ");
    }
  }
}
