using System;
using System.Collections.Generic;
using System.Threading;

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
            //this library is created from a csv file
            LibraryDb = new List<Book>(BookDatabase.GetBookList());

            //error handling if no csv data is loaded
            if (LibraryDb.Count == 0)
            {
                NoData = true;
                BookErrorView bev = new BookErrorView("Julius Caesar burnt down the library of Alexandria and set civilization back by a few hundred years.");
                bev.Display();
            }
            else if (LibraryDb.Count == 20)
            {
                NoData = true;
                BookErrorView bev = new BookErrorView("Quintus Varo has your legions.");
                bev.Display();
            }
            //creates a checkout book list from csv file
            CheckoutBookList = new List<Book>();
            foreach (Book book in LibraryDb)
            {
                if (book.Status == false)
                {
                    CheckoutBookList.Add(book);
                }
            }
        }

        public void RunLibrary()
        {

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
        //method displays library logo
        public void LibraryWelcome()
        {
            BookWelcomeView wv = new BookWelcomeView();
            wv.Display();
            Thread.Sleep(5000);
        }
        //method displays library menu
        public void LibraryMenu()
        {
            Console.Clear();
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
        //method displays menu within search
        public void SearchMenu()
        {
            BookSearchView smv = new BookSearchView();
            smv.Display();
            int userInput = GetIntFromUser(1, 5);

            if (userInput == 1)
            {
                SearchBook("All");
            }
            else if (userInput == 2)
            {
                SearchBook("Title");
            }
            else if (userInput == 3)
            {
                SearchBook("Author");
            }
            else if (userInput == 4)
            {
                SearchBook("Genre");
            }
            else if (userInput == 5)
            {
                SearchBook("Dewey");
            }
            else
            {
                Console.WriteLine("No book found based on your search parameters.");
                LibraryMenu();
            }
        }
        //method for searching a book and error for search parameters
        public void SearchBook(string searchType)
        {
            //searchView changes depending on what is being searched. 
            //generic Iview is used to store different views depending on search type.
            IView searchView = new SearchView(searchType);

            if (searchType == "Dewey")
            {
                searchView = new BookSearchDeweyView();
            }
            searchView.Display();

            //no user input needed for search all books
            string userInput = "";
            if (!(searchType == "All"))
            {
                userInput = Console.ReadLine().ToLower();
            }

            bool foundBook = false;
            for (int i = 0; i < LibraryDb.Count; i++)
            {
                bool matchingSearchFound = false;
                Book selectedBook = LibraryDb[i];
                if (searchType == "All")
                {
                    matchingSearchFound = true;
                }
                else if (searchType == "Title")
                {
                    matchingSearchFound = selectedBook.Title.ToLower().Contains(userInput);
                }
                else if (searchType == "Author")
                {
                    matchingSearchFound = selectedBook.Author.ToLower().Contains(userInput);
                }
                else if (searchType == "Genre")
                {
                    matchingSearchFound = selectedBook.Genre.ToLower().Contains(userInput);
                }
                else if (searchType == "Dewey")
                {
                    matchingSearchFound = selectedBook.DeweySystem.ToLower().Contains(userInput);
                }

                if (matchingSearchFound)
                {
                    foundBook = true;
                    BookView bv = new BookView(selectedBook);
                    bv.Display();
                }
            }
            if (!foundBook)
            {
                BookErrorView bev = new BookErrorView($"No book found based on your search parameters.");
                bev.Display();
            }

            ReturnToMainMenuPrompt();
        }

        //method for book selection, checkout and error handling
        public void CheckoutBook()
        {
            BookCheckoutListView bclv = new BookCheckoutListView(LibraryDb);
            bclv.Display();

            //int numberOfCheckedOutBooks = CheckoutBookList.Count;
            int userInput = GetIntFromUser(1, LibraryDb.Count);//Get which book to checkout
            if (userInput != -1)
            {

                Book selectedBook = LibraryDb[userInput - 1];//store the book in an easier to read variable
                bool onShelf = selectedBook.Status; //Store the checked out status in an easier to read variable
                bool onHold = selectedBook.HoldStatus;//Store the hold status in an easier to read variable

                //Check the status of selected book and display appropriate message when they try to check out a book
                if (onShelf == false && onHold == true)
                {
                    BookErrorView bev = new BookErrorView($"Sorry that book is already checked out and on hold. Check back after {selectedBook.DueDate.AddDays(14).ToShortDateString()}");
                    bev.Display();
                }
                else if (onShelf == false && onHold == false)
                {
                    bool response1 = GetYesOrNoFromUser("Do you want to place this book on hold?");
                    if (response1 == true)
                    {
                        selectedBook.HoldStatus = true;
                        Console.Clear();
                        Console.WriteLine("Your book has been placed on hold. We will notify you when available.");
                    }
                }
                else
                {
                    CheckoutBookList.Add(selectedBook);
                    selectedBook.Status = false;
                    selectedBook.DueDate = DateTime.Now.AddDays(14);
                    SaveToCSV();
                }

            }
            //Display books currently checked out.
            BookCheckoutCartListView bcclv = new BookCheckoutCartListView(LibraryDb);
            bcclv.Display();

            bool response = GetYesOrNoFromUser("Do you want to check out another book?"); //Gets yes or no from user

            //leftover books to choose from to add to cart again
            if (response == true)
            {
                CheckoutBook();
            }

            //Save the checkout status to csv
            SaveToCSV();

            ReturnToMainMenuPrompt();
        }
        //method for selecting and returning a book
        public void ReturnBook()
        {
            //lists books to return
            BookReturnView brv = new BookReturnView(LibraryDb);
            brv.Display();
            List<int> ListOfBookIndexesCheckedOut = new List<int>();//Store a list of LibraryDb indexes of the checkedout books
            bool error = false;

            if (CheckoutBookList.Count > 0)//Check to make sure we have checkedout books
            {
                for (int i = 0; i < LibraryDb.Count; i++)
                {
                    if (LibraryDb[i].Status == false)
                    {
                        ListOfBookIndexesCheckedOut.Add(i);//Add to the list of LibraryDb indexes of the checkedout books
                    }
                }

                //updates status of books and displays to patron
                int userInput = GetIntFromUser(1, ListOfBookIndexesCheckedOut.Count);
                if (!(userInput == -1))
                {
                    Book selectedBook = LibraryDb[ListOfBookIndexesCheckedOut[userInput - 1]];
                    for (int i = 0; i < CheckoutBookList.Count; i++)
                    {
                        if (CheckoutBookList[i] == selectedBook/*LibraryDb[userInput - 1]*/)
                        {
                            CheckoutBookList.RemoveAt(i);//remove from list
                        }
                    }


                    if (selectedBook.HoldStatus == true)
                    {
                        Console.Clear();
                        Console.WriteLine($"When you return {selectedBook.Title} another user checked it out.");
                        selectedBook.DueDate = DateTime.Now.AddDays(14);
                        selectedBook.HoldStatus = false;
                    }
                    else
                    {
                        selectedBook.Status = true;
                        selectedBook.DueDate = DateTime.MinValue;
                    }
                }
                else
                {
                    error = true;
                }
            }

            if (CheckoutBookList.Count > 0 && !(error))
            {
                if (GetYesOrNoFromUser("Would you like to return another book?"))
                {
                    ReturnBook();
                }
            }
            //Save after return.
            SaveToCSV();
            ReturnToMainMenuPrompt();
        }

        //method to return to main menu of library
        public void ReturnToMainMenuPrompt()
        {
            BookReturnToMainMenuView brmmv = new BookReturnToMainMenuView();
            brmmv.Display();
            Console.ReadKey();
            LibraryMenu();
        }

        //method to save csv file of checked out books
        public void SaveToCSV()
        {
            BookSaveCSV bscsv = new BookSaveCSV(LibraryDb);
            bscsv.SaveBookList();
        }

        /// <summary>
        /// gets valid response and returns value(int)
        /// </summary>
        /// <returns>int</returns>
        public static int GetIntFromUser(int min, int max)
        {
            try
            {
                string userInput = Console.ReadLine();
                int userInteger = -1;
                if (userInput.Trim().ToLower() == "cancel" || userInput.Trim().ToLower() == "c")
                {
                    return -1;
                }
                else
                {
                    userInteger = int.Parse(userInput);

                    if (userInteger >= min && userInteger <= max)
                    {
                        return userInteger;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

            }
            //error handling for mistyping
            catch (Exception e)
            {
                if (max == 1)
                {
                    Console.WriteLine($"Please select option 1 or (C)ancel");
                }
                else
                {
                    Console.WriteLine($"Please select options {min} - {max} or (C)ancel");
                }
                return GetIntFromUser(min, max);
            }
        }

        /// <summary>
        /// Pass string into method with message. Add's Yes or No to it and writes it to console. Returns true for yes and false for no
        /// </summary>
        /// <param name="prompt">string</param>
        /// <returns>bool</returns>
        private bool GetYesOrNoFromUser(string promptMessage)
        {
            BookYesNoPromptView bynpv = new BookYesNoPromptView(promptMessage);
            bynpv.Display();
            string answer = Console.ReadLine().Trim().ToLower();

            if (answer == "y" || answer == "yes")
            {
                return true;
            }
            else if (answer == "n" || answer == "no")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Invalid Input!");
                return GetYesOrNoFromUser(promptMessage);

            }
        }
    }
}
