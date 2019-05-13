using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grand_Circus_Library
{
    class BookReturnToMainMenuView : IView
    {
        //returns to main menu
        public void Display()
        {
            Console.WriteLine("Press any key to return to main menu.");
        }
    }
}
