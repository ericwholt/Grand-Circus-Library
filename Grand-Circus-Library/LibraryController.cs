﻿using System;
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
            LibraryDb = new List<Book>(BookDatabase.GetBookList());

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
            Thread.Sleep(5000);
        }

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

        public void SearchMenu()
        {
            BookSearchView smv = new BookSearchView();
            smv.Display();
            int userInput = GetIntFromUser(1, 5);

            if (userInput == 1)
            {
                //SearchBookAll();
                SearchBookAll();
            }
            else if (userInput == 2)
            {
                SearchBookTitle();
            }
            else if (userInput == 3)
            {
                SearchBookAuthor();
            }
            else if (userInput == 4)
            {
                SearchBookGenre();
            }
            else if (userInput == 5)
            {
                SearchBookDewey();
            }
            else
            {
                Console.WriteLine("No book found based on your search parameters.");
                LibraryMenu();
            }
        }

        public void SearchBookAll()
        {
            BookSearchAllView bsav = new BookSearchAllView();
            bsav.Display();

            for (int i = 0; i < LibraryDb.Count; i++)
            {
                BookView bv = new BookView(LibraryDb[i]);
                bv.Display();
            }

            ReturnToMainMenuPrompt();
        }

        public void SearchBookTitle()
        {
            BookSearchTitleView bstv = new BookSearchTitleView();
            bstv.Display();

            string userInput = Console.ReadLine().ToLower();
            bool foundBook = false;
            for (int i = 0; i < LibraryDb.Count; i++)
            {
                if (LibraryDb[i].Title.ToLower().Contains(userInput))
                {
                    foundBook = true;
                    BookView bv = new BookView(LibraryDb[i]);
                    bv.Display();
                }
            }
            if (!foundBook)
            {
                BookErrorView bev = new BookErrorView("No book title was found based on your search parameters.");
                bev.Display();
            }

            ReturnToMainMenuPrompt();
        }

        public void SearchBookAuthor()
        {
            BookSearchAuthorView bsav = new BookSearchAuthorView();
            bsav.Display();

            string userInput = Console.ReadLine().ToLower();
            bool foundBook = false;
            for (int i = 0; i < LibraryDb.Count; i++)
            {
                if (LibraryDb[i].Author.ToLower().Contains(userInput))
                {
                    //Console.WriteLine($"{i}. {LibraryDb[i].Title} written by {LibraryDb[i].Author}");
                    foundBook = true;
                    BookView bv = new BookView(LibraryDb[i]);
                    bv.Display();
                }
                else
                {

                }
            }
            if (!foundBook)
            {
                BookErrorView bev = new BookErrorView("No author was found based on your search parameters.");
                bev.Display();
            }
            ReturnToMainMenuPrompt();
        }

        public void SearchBookGenre()
        {
            BookSearchGenreView bsgv = new BookSearchGenreView();
            bsgv.Display();

            string userInput = Console.ReadLine().ToLower();
            bool foundBook = false;
            for (int i = 0; i < LibraryDb.Count; i++)
            {
                Book selectedBook = LibraryDb[i];
                foundBook = selectedBook.Genre.ToLower().Contains(userInput);
                if (selectedBook.Genre.ToLower().Contains(userInput))
                {
                    BookView bv = new BookView(LibraryDb[i]);
                    bv.Display();
                }
            }
            if (!foundBook)
            {
                BookErrorView bev = new BookErrorView("No book genre was found based on your search parameters.");
                bev.Display();
            }
            ReturnToMainMenuPrompt();
        }
        public void SearchBookDewey()
        {
            BookSearchDeweyView bsdv = new BookSearchDeweyView();
            bsdv.Display();

            string userInput = Console.ReadLine().ToLower();
            bool foundBook = false;

            for (int i = 0; i < LibraryDb.Count; i++)
            {
                Book selectedBook = LibraryDb[i];
                foundBook = selectedBook.DeweySystem.ToLower().Contains(userInput);

                if (foundBook)
                {
                    Console.WriteLine($"{i + 1}. {selectedBook.Title} call number {selectedBook.DeweySystem}");
                }
            }

            if (!foundBook)
            {
                BookErrorView bev = new BookErrorView("No category was found based on your search parameters.");
                bev.Display();
            }

            ReturnToMainMenuPrompt();
        }

        //public void SearchBook(string searchType)
        //{
        //    IView view;
        //    searchType = searchType.ToLower().Trim();

        //    if (searchType == "title")
        //    {
        //        view = new BookSearchTitleView();
        //        view.Display();
        //    }
        //    else if (searchType == "genre")
        //    {
        //        view = new BookSearchGenreView();
        //        view.Display();
        //    }
        //    else if (searchType == "author")
        //    {
        //        view = new BookSearchAuthorView();
        //        view.Display();
        //    }
        //    else if (searchType == "dewey")
        //    {
        //        view = new BookSearchDeweyView();
        //        view.Display();
        //    }
        //    else
        //    {
        //        view = new BookSearchAllView();
        //        view.Display();
        //    }

        //    string message = "";
        //    string errorMessage = "";
        //    string userInput = Console.ReadLine().ToLower();
        //    bool foundBook = false;

        //    for (int i = 0; i < LibraryDb.Count; i++)
        //    {
        //        Book selectedBook = LibraryDb[i];

        //        if (searchType == "title")
        //        {
        //            foundBook = selectedBook.Title.ToLower().Contains(userInput);
        //            errorMessage = "No book title was found based on your search parameters.";
        //        }
        //        else if (searchType == "genre")
        //        {
        //            foundBook = selectedBook.Author.ToLower().Contains(userInput);
        //            errorMessage = "No book genre was found based on your search parameters.";
        //        }
        //        else if (searchType == "author")
        //        {
        //            foundBook = selectedBook.Genre.ToLower().Contains(userInput);
        //            errorMessage = "No author was found based on your search parameters.";
        //        }
        //        else if (searchType == "dewey")
        //        {
        //            foundBook = selectedBook.DeweySystem.ToLower().Contains(userInput);
        //            message = $"{i + 1}. {selectedBook.Title} call number {selectedBook.DeweySystem}";
        //            errorMessage = "No category was found based on your search parameters.";
        //        }
        //        else
        //        {
        //            foundBook = true;
        //        }


        //        if (foundBook)
        //        {
        //            if (searchType == "dewey")
        //            {
        //                Console.WriteLine(message);
        //            }
        //            else
        //            {
        //                BookView bv = new BookView(selectedBook);
        //                bv.Display();
        //            }
        //        }
        //    }

        //    if (!foundBook)
        //    {
        //        BookErrorView bev = new BookErrorView(errorMessage);
        //        bev.Display();
        //    }

        //    ReturnToMainMenuPrompt();
        //}

        public void CheckoutBook()
        {
            BookCheckoutListView bclv = new BookCheckoutListView(LibraryDb);
            bclv.Display();

            //int numberOfCheckedOutBooks = CheckoutBookList.Count;
            int userInput = GetIntFromUser(1, LibraryDb.Count);//Get which book to checkout

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

        public void ReturnBook()
        {
            BookReturnView brv = new BookReturnView(LibraryDb);
            brv.Display();
            List<int> ListOfBookIndexesCheckedOut = new List<int>();
            bool error = false;
            if (CheckoutBookList.Count > 0)
            {
                for (int i = 0; i < LibraryDb.Count; i++)
                {
                    if (LibraryDb[i].Status == false)
                    {
                        ListOfBookIndexesCheckedOut.Add(i);
                    }
                }


                int userInput = GetIntFromUser(1, ListOfBookIndexesCheckedOut.Count);
                if (!(userInput == -1))
                {
                    for (int i = 0; i < CheckoutBookList.Count; i++)
                    {
                        if (CheckoutBookList[i] == LibraryDb[userInput - 1])
                        {
                            CheckoutBookList.RemoveAt(i);
                        }
                    }
                    if (LibraryDb[ListOfBookIndexesCheckedOut[userInput - 1]].HoldStatus == true)
                    {
                        Console.Clear();
                        Console.WriteLine($"When you return {LibraryDb[ListOfBookIndexesCheckedOut[userInput - 1]].Title} another user checked it out.");
                        LibraryDb[ListOfBookIndexesCheckedOut[userInput - 1]].DueDate = DateTime.Now.AddDays(14);
                        LibraryDb[ListOfBookIndexesCheckedOut[userInput - 1]].HoldStatus = false;
                    }
                    else
                    {
                        LibraryDb[ListOfBookIndexesCheckedOut[userInput - 1]].Status = true;
                        LibraryDb[ListOfBookIndexesCheckedOut[userInput - 1]].DueDate = DateTime.MinValue;
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

        public void ReturnToMainMenuPrompt()
        {
            BookReturnToMainMenuView brmmv = new BookReturnToMainMenuView();
            brmmv.Display();
            Console.ReadKey();
            LibraryMenu();
        }

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
            catch (Exception e)
            {
                Console.WriteLine($"Please select options {min} - {max} or (C)ancel");
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

        /// <summary>
        /// gets valid response and returns value(int). If int array empty it will return -1 as error
        /// </summary>
        /// <returns>int</returns>
        private int GetSpecificIntFromUser(List<int> specificIntsAllowed)
        {
            if (!(specificIntsAllowed.Count == 0))
            {


                try
                {
                    string message = "Please select number";
                    for (int i = 0; i < specificIntsAllowed.Count; i++)
                    {
                        if (i == 0)
                        {
                            message += $" {specificIntsAllowed[i]}";
                        }
                        else if (i == specificIntsAllowed.Count - 1)
                        {
                            message += $", {specificIntsAllowed[i]}";
                        }
                        else if (i == specificIntsAllowed.Count)
                        {
                            //do nothing
                        }
                        else
                        {
                            message += $", {specificIntsAllowed[i]}";
                        }
                    }
                    message += ", or (C)ancel";
                    Console.WriteLine(message);
                    string userInput = Console.ReadLine();
                    if (userInput.Trim().ToLower() == "cancel" || userInput.Trim().ToLower() == "c")
                    {
                        return -1;
                    }
                    int userInt = int.Parse(userInput);
                    foreach (int integer in specificIntsAllowed)
                    {
                        if (integer == userInt)
                        {
                            return userInt;
                        }
                    }
                    throw new Exception();

                }
                catch (Exception e)
                {
                    //string message = "Please select number";
                    //for (int i = 0; i < specificIntsAllowed.Count; i++)
                    //{
                    //    if (i == specificIntsAllowed.Count - 1)
                    //    {
                    //        message += $", or {specificIntsAllowed[i]}.";
                    //    }
                    //    message += $", {specificIntsAllowed[i]}";
                    //}
                    Console.WriteLine("Invalid input!");
                    return GetSpecificIntFromUser(specificIntsAllowed);
                }
            }
            return -1;
        }
    }
}

