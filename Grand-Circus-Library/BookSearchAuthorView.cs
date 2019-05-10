using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grand_Circus_Library
{
  class BookSearchAuthorView : IView
  {
    public void Display()
    {
      Console.Write("Author of the book: ");
    }
  }
}
