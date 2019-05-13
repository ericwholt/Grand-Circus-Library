using System;
using System.Collections.Generic;

namespace Grand_Circus_Library
{
    class BookCheckoutListView : BookListView
    {
        //list of books in checkout
        public BookCheckoutListView(List<Book> BookList) : base(BookList)
        {

        }
        public override void Display()
        {
            Console.Clear();
            bool listCreated = false;
            for (int i = 0; i < BookList.Count; i++)
            {
                try
                {
                    if (BookList[i].Status == false)
                    {
                        listCreated = true;
                        //displayed updated status of books
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
                        listCreated = true;
                        Console.WriteLine($"{i + 1}. {BookList[i].Title} status {(BookList[i].Status ? "On Shelf" : "Checked Out")}");
                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine("Unable to display view");
                }
            }
            if (listCreated)
            {
                Console.WriteLine();
                Console.Write("Which book would you like to check out or (C)ancel: ");
            }
        }
    }
}
