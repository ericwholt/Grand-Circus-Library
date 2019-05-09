using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grand_Circus_Library
{
    interface IView
    {
        //Choose interface so that we enforce views to have a display method. 
        //Currently not need for constructors or props so interface should be fine.
        void Display();
    }
}
