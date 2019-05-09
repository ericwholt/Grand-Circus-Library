using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grand_Circus_Library
{
    class BookCheckoutView : IView
    {
        public void Display()
        {
            Console.WriteLine("Which book would you like to check out: ");
        }
    }
}
