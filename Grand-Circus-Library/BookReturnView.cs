﻿using System;
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

        public void Display()
        {
            int bookCheckoutCount = 0;

            Console.Clear();

            foreach (Book book in BookList)
            {
                if (book.Status == false)
                {
                    bookCheckoutCount++;
                }
            }
            Console.WriteLine("Books currently checked out");
            if (bookCheckoutCount > 0)
            {


                for (int i = 0; i < BookList.Count; i++)
                {
                    if (!BookList[i].Status)
                    {
                        Console.WriteLine($"{i + 1} {BookList[i].Title} due by {BookList[i].DueDate.ToShortDateString()}");
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
