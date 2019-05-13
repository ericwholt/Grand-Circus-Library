using System;
using System.Collections.Generic;

namespace Grand_Circus_Library
{
  class Program
  {
    static void Main(string[] args)
    {
      //using the mvc method - controller runs the entire program
      LibraryController LibraryApp = new LibraryController();
      LibraryApp.RunLibrary();
    }
  }
}
