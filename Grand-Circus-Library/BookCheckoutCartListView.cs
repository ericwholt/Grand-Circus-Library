using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grand_Circus_Library
{
    class BookCheckoutCartListView : BookListView
    {
        public BookCheckoutCartListView(List<Book> BookList) : base(BookList)
        {

        }
        public override void Display()
        {
            Console.WriteLine();
            Console.WriteLine("Books in your check out cart:");
            for (int i = 0; i < BookList.Count; i++)
            {
                if (BookList[i].Status == false && BookList[i].HoldStatus == true)
                {
                    Console.WriteLine($"{i + 1}. {BookList[i].Title} is on hold and due on {BookList[i].DueDate.ToShortDateString()} ");
                }
                else if (BookList[i].Status == false)
                {
                    Console.WriteLine($"{i + 1}. {BookList[i].Title} and due on {BookList[i].DueDate.ToShortDateString()} ");
                }
            }
        }
    }
}
