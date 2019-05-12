using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grand_Circus_Library
{
  class BookCheckoutListView : BookListView
  {
    public BookCheckoutListView(List<Book> BookList) : base(BookList)
    {

    }
    public override void Display()
    {
      Console.Clear();
      for (int i = 0; i < BookList.Count; i++)
      {
        try
        {
          if (BookList[i].Status == false)
          {
            if (BookList[i].HoldStatus == true)
            {
              Console.WriteLine($"{i + 1}. {BookList[i].Title} status {(BookList[i].Status ? "On Shelf" : "Checked Out")} and On Hold.");
            }
            else
            {
              Console.WriteLine($"{i + 1}. {BookList[i].Title} status {(BookList[i].Status ? "On Shelf" : "Checked Out")} due back on {BookList[i].DueDate.ToShortDateString()}");
            }
          }
          else
          {
            Console.WriteLine($"{i + 1}. {BookList[i].Title} status {(BookList[i].Status ? "On Shelf" : "Checked Out")}");
          }
        }

        catch (Exception e)
        {
          Console.WriteLine("Unable to display view");
        }
      }
    }
  }
}
