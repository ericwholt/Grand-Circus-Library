using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grand_Circus_Library
{
    class BookListView : IView
    {
        public List<Book> BookList { get; set; }

        public BookListView(List<Book> BookList)
        {
            this.BookList = BookList;
        }

        //display a list of books
        public virtual void Display()
        {
            for (int i = 0; i < BookList.Count; i++)
            {
                Console.WriteLine($"{i+1} {BookList[i].Title}");
            }
        }
    }
}
