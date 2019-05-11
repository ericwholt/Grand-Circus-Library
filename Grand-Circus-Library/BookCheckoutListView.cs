﻿using System;
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
                if (BookList[i].Status == false)
                {
                //Eric please add Ternary for status

                Console.WriteLine($"{i + 1}. {BookList[i].Title} status {BookList[i].Status} due back on {BookList[i].DueDate.ToShortDateString()}");
                }
                else
                {
                  Console.WriteLine($"{i + 1}. {BookList[i].Title} status {BookList[i].Status}");
                }
            }
        }
    }
}
