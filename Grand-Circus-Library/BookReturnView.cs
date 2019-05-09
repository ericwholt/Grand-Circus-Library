using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grand_Circus_Library
{
    class BookReturnView : IView
    {
        public List<Book> BookList { get; set; }
        public BookReturnView(List<Book> BookList)
        {

        }

        public void Display()
        {
            Console.WriteLine("Books currently checked out");
            for (int i = 0; i < BookList.Count; i++)
            {
                if (!BookList[i].Status)
                {
                    Console.WriteLine($"{i} {BookList[i].Title} {BookList[i].DueDate}");
                }
            }
            Console.Write("Which book would you like to return: ");

        }
    }
}
