using System;
using System.Collections.Generic;

namespace Grand_Circus_Library
{

  class LibraryController
  {
    private List<Book> _LibraryDb = new List<Book>();
    public List<Book> LibraryDb
    {
      get
      {
        return _LibraryDb;
      }
    }

    public LibraryController()
    {
      BookLoadCSV BookDatabase = new BookLoadCSV();
      List<Book> _LibraryDb = BookDatabase.GetBookList();
    }

    public void RunLibrary()
    {
      LibraryWelcome();
      LibraryMenu();
    }

    public void LibraryWelcome()
    {
      BookWelcomeView wv = new BookWelcomeView();
      wv.Display();
    }

    public void LibraryMenu()
    {
      BookMenuView bmv = new BookMenuView();
      bmv.Display();
      int userInput = GetIntFromUser(1, 3);

      if (userInput == 1)
      {
        SearchMenu();
      }
      else if (userInput == 2)
      {
        CheckoutBook();
      }
      else if (userInput == 3)
      {
        ReturnBook();
      }
      else
      {
        LibraryMenu();
      }
    }

      public void SearchMenu()
    {

      BookSearchView smv = new BookSearchView();
      smv.Display();
      int userInput = GetIntFromUser(1, 3);

      if (userInput == 1)
      {
        Console.WriteLine("Call Search by Title");
      }
      else if (userInput == 2)
      {
        Console.WriteLine("Call Search by Author");
      }
      else if (userInput == 3)
      {
        Console.WriteLine("Call Search by Genre");
      }
      else
      {
        LibraryMenu();
      }
    }

    public void CheckoutBook()
    {
      Console.WriteLine("Display checkout menu in method");
    }
    public void ReturnBook()
    {
      Console.WriteLine("Display return menu in method");
    }

    /// <summary>
    /// gets valid response and returns value(int)
    /// </summary>
    /// <returns>int</returns>
    public static int GetIntFromUser(int min, int max)
    {
      try
      {
        int userInput = int.Parse(Console.ReadLine());
        if (userInput >= min && userInput <= max)
        {
          return userInput;
        }
        else
        {
          throw new Exception();
        }

      }
      catch (Exception e)
      {
        Console.WriteLine($"Please select options {min} - {max}");
        return GetIntFromUser(min, max);
      }
    }

  }
}
