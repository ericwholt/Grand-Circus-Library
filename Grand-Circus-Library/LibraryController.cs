﻿using System;
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
            ReturnToMainMenuPrompt();
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
            ReturnToMainMenuPrompt();
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
            ReturnToMainMenuPrompt();
        }

        public void CheckoutBook()
        {
            //List<Book> filteredList = LibraryDb.Where(x => x.Status ==  true).ToList();//Filter out checked out books. Won't work we don't want to create a list without the book. We just want to not display it.
            BookCheckoutListView bclv = new BookCheckoutListView(LibraryDb);
            bclv.Display();//Display the books not checked out
            BookCheckoutView bcv = new BookCheckoutView();
            bcv.Display();

            int userInput = GetIntFromUser(1, 12);

            CheckoutBookList.Add(LibraryDb[userInput - 1]);
            //blv.BookList.RemoveAt(userInput);
            LibraryDb[userInput - 1].Status = false;
            LibraryDb[userInput - 1].DueDate = DateTime.Now.AddDays(14);

            //blv.Display();




            //Might want to add to its own view so it can be used here and in return view to list books currently checked out.
            Console.WriteLine("Books in your check out cart:");
            //Console.WriteLine(CheckoutBookList.Count);
            for (int i = 0; i < LibraryDb.Count; i++)
            {
                if (LibraryDb[i].Status == false)
                {
                    Console.WriteLine($"{i + 1}. {LibraryDb[i].Title} due on {LibraryDb[i].DueDate.ToShortDateString()}");
                }
            }

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
            if (CheckoutBookList.Count > 0)
            {
                for (int i = 0; i < LibraryDb.Count; i++)
                {
                    if (LibraryDb[i].Status == false)
                    {
                        ListOfBookIndexesCheckedOut.Add(i + 1);
                    }
                }

                int userInput = GetSpecificIntFromUser(ListOfBookIndexesCheckedOut);
                if (!(userInput == -1))
                {

                    for (int i = 0; i < CheckoutBookList.Count; i++)
                    {
                        if (CheckoutBookList[i] == LibraryDb[userInput - 1])
                        {
                            CheckoutBookList.RemoveAt(i);
                        }
                    }
                    LibraryDb[userInput - 1].Status = true;
                    LibraryDb[userInput - 1].DueDate = DateTime.MinValue;
                }
            }

            if (CheckoutBookList.Count > 0)
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

        /// <summary>
        /// Pass string into method with message. Add's Yes or No to it and writes it to console. Returns true for yes and false for no
        /// </summary>
        /// <param name="prompt">string</param>
        /// <returns>bool</returns>
        private bool GetYesOrNoFromUser(string promptMessage)
        {
            BookYesNoPromptView bynpv = new BookYesNoPromptView(promptMessage);
            bynpv.Display();
            string answer = Console.ReadLine().ToLower();

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
                            message += $", or {specificIntsAllowed[i]}.";
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

