using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grand_Circus_Library
{
    class BookErrorView : IView
    {

    public string Message { get; set; }

    public BookErrorView(string Message)
    {
      this.Message = Message;
    }    

        //error handle and messaging for users
        public void Display()
        {
          Console.Clear();
          Console.WriteLine(Message);
        }
    }
}
