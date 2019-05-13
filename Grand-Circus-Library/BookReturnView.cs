using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grand_Circus_Library
{
    class BookReturnView : IView
    {
        private List<Book> BookList {  get; set; }
        public BookReturnView(List<Book> BookList)
        {
            this.BookList = BookList;
        }
        //renumbering return list for customer
        public void Display()
        {
            int bookCheckoutCount = 0;

            Console.Clear();
            bool onShelf = false;

            foreach (Book book in BookList)
            {
                if (book.Status == false)
                {
                    bookCheckoutCount++;
                }
            }
            //displays books that are checked out
            Console.WriteLine("Books currently checked out");
            if (bookCheckoutCount > 0)
            {

                int count = 0;
                for (int i = 0; i < BookList.Count; i++)
                {
                    onShelf = BookList[i].Status;
                    if (!onShelf)
                    {
                        //split display for books with additional holds
                        count++;
                        if(BookList[i].HoldStatus == true)
                        {
                        Console.WriteLine($"{count} {BookList[i].Title} due by {BookList[i].DueDate.ToShortDateString()} and is On Hold.");
                        }
                        else
                        {
                        
                        Console.WriteLine($"{count} {BookList[i].Title} due by {BookList[i].DueDate.ToShortDateString()}");
                        }
                    }
                }
                Console.Write("Which book would you like to return: ");
            }
            else
            {
                Console.WriteLine("No books currently checked out.");
            }

        }
    }
}
