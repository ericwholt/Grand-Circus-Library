using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grand_Circus_Library
{
    class BookYesNoPromptView : IView
    {
        public string Prompt { get; set; }
        public BookYesNoPromptView(string Prompt)
        {
            this.Prompt = Prompt;
        }
        //used for asking library patron yes or no
        public void Display()
        {
            Console.WriteLine(Prompt.Trim() + " (Yes or No)");
        }
    }
}
