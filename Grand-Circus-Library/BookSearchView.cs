﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grand_Circus_Library
{
    class BookSearchView : IView
    {
        public void Display()
        {
            Console.WriteLine("1. Search by Title");
            Console.WriteLine("2. Search by Author");
            Console.WriteLine("3. Search by Genre");
        }
    }
}
