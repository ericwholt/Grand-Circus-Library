using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grand_Circus_Library
{
  class BookSearchTitleView : IView
  {
    public void Display()
    {
      Console.Write("Title of the book: ");
    }
  }
}
