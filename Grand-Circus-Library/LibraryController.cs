using System;
using System.Collections.Generic;

namespace Grand_Circus_Library
{

    class LibraryController
    {
        public List<Book> LibraryDb { get; set; }
        public bool NoData { get; set; }
        public List<Book> CheckoutBookList { get; set; }

        public LibraryController()
        {
            BookLoadCSV BookDatabase = new BookLoadCSV();
            LibraryDb = new List<Book>(BookDatabase.GetBookList());

            if (LibraryDb.Count == 0)
            {
                NoData = true;
                BookErrorView bev = new BookErrorView("Julius Caesar burnt down the library of Alexandria and set civilization back by a few hundred years.");
                bev.Display();
            }
            
        }

        public void RunLibrary()
        {
            //Console.WriteLine($"LibraryDb Count: {LibraryDb.Count}");
            if (!NoData)
            {
                LibraryWelcome();
                LibraryMenu();
            }
            else
            {
                Console.ReadKey();
            }
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
                SearchBookTitle();
            }
            else if (userInput == 2)
            {
                SearchBookAuthor();
            }
            else if (userInput == 3)
            {
                SearchBookGenre();
            }
            else
            {
                LibraryMenu();
            }
        }

        public void SearchBookTitle()
        {
            BookSearchTitleView bstv = new BookSearchTitleView();
            bstv.Display();

            string userInput = Console.ReadLine().ToLower();

            for (int i = 0; i < LibraryDb.Count; i++)
            {
                if (LibraryDb[i].Title.ToLower().Contains(userInput))
                {
                    //Console.WriteLine($"{i}. {LibraryDb[i].Title} written by {LibraryDb[i].Author}");

                    BookView bv = new BookView(LibraryDb[i]);
                    bv.Display();
                }
            }
            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadLine();
            LibraryMenu();
        }

        public void SearchBookAuthor()
        {
            BookSearchAuthorView bsav = new BookSearchAuthorView();
            bsav.Display();

            string userInput = Console.ReadLine().ToLower();

            for (int i = 0; i < LibraryDb.Count; i++)
            {
                if (LibraryDb[i].Author.ToLower().Contains(userInput))
                {
                    //Console.WriteLine($"{i}. {LibraryDb[i].Title} written by {LibraryDb[i].Author}");

                    BookView bv = new BookView(LibraryDb[i]);
                    bv.Display();
        }
            }
            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadLine();
            LibraryMenu();
        }

        public void SearchBookGenre()
        {
            BookSearchGenreView bsgv = new BookSearchGenreView();
            bsgv.Display();

            string userInput = Console.ReadLine().ToLower();

            for (int i = 0; i < LibraryDb.Count; i++)
            {
                if (LibraryDb[i].Genre.ToLower().Contains(userInput))
                {
                    //Console.WriteLine($"{i}. {LibraryDb[i].Title} written by {LibraryDb[i].Author}");
                    BookView bv = new BookView(LibraryDb[i]);
                    bv.Display();
        }
            }
            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadLine();
            LibraryMenu();
        }

        public void CheckoutBook()
        {
            BookListView blv = new BookListView(LibraryDb);
            blv.Display();
            BookCheckoutView bcv = new BookCheckoutView();
            bcv.Display();

            int userInput = GetIntFromUser(1, 12);

            List<Book> CheckoutBookList = new List<Book>();

            CheckoutBookList.Add(LibraryDb[userInput-1]);
            //blv.BookList.RemoveAt(userInput);
            LibraryDb[userInput - 1].Status = false;
            LibraryDb[userInput - 1].DueDate = DateTime.Now.AddDays(14);

             blv.Display();

            Console.WriteLine("Do you want to check out another book? Y/N"); //need to add loop

            //leftover books to choose from to add to cart again
            
            

            Console.WriteLine("Books in your check out cart:");

            for (int i = 0; i < CheckoutBookList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {CheckoutBookList[i].Title} due on {CheckoutBookList[i].DueDate.ToShortDateString()}");
            }
            //BookSaveCSV bscsv = new BookSaveCSV(LibraryDb);
            //bscsv.SaveBookList();
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

