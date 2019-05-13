using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace Grand_Circus_Library
{
    class BookLoadCSV
    {
        public List<Book> BookList { get; }

        public BookLoadCSV()
        {
            BookList = new List<Book>();
        }

        //use csv file to create a book list
        public List<Book> GetBookList()
        {
            List<List<string>> csv = new List<List<string>>();//This represents the excel sheet. The list inside are the rows and columns
            string path = ConfigurationManager.AppSettings["Path"];//Get path from the configuration file.
            string[] csvRows;//To store strings read in from File

            //Check if the path is valid
            if (File.Exists(path))
            {
                try
                {
                    csvRows = File.ReadAllLines(ConfigurationManager.AppSettings["Path"]);//Store each line of csv into a string array

                }
                catch (Exception)
                {
                    
                    List<Book> BookListOf20 = new List<Book>();
                    for (int i = 0; i < 20; i++)
                    {
                        BookListOf20.Add(new Book());
                    }
                    return BookListOf20;
                }
            }
            else
            {
                //Path wasn't valid return null.
                List<Book> EmptyBookList = new List<Book>();
                return EmptyBookList;
            }


            char sepChar = ',';
            char quoteChar = '"';
            bool skipHeader = true;
            foreach (string csvRow in csvRows)
            {
                if (skipHeader)
                {
                    //Skip first line as it is the header
                    skipHeader = false;
                    continue;
                }
                List<string> fields = new List<string>();//This is the rows of the csv
                bool inQuotes = false;
                string field = "";
                Book b = new Book();
                for (int i = 0; i < csvRow.Length; i++)
                {
                    //in order to have text with commas you have to wrap in double quotes. In order to have double quotes you have to escape it with double quotes.
                    if (inQuotes)
                    {
                        //Check if we have a escaped quote char as we go through the string
                        if (i < csvRow.Length - 1 && csvRow[i] == quoteChar && csvRow[i + 1] == quoteChar)
                        {
                            i++;
                            field += quoteChar;//Not sure the need for this
                        }
                        else if (csvRow[i] == quoteChar)
                        {
                            inQuotes = false;
                        }
                        else
                        {
                            field += csvRow[i];
                        }
                    }
                    else
                    {
                        if (csvRow[i] == quoteChar)
                        {
                            i++;//This removes beginning quote on the synopsis
                            inQuotes = true;
                        }

                        if (csvRow[i] == sepChar)
                        {
                            fields.Add(field);
                            field = "";
                        }
                        else
                        {
                            field += csvRow[i];
                        }
                    }
                }
                if (!string.IsNullOrEmpty(field))
                {
                    fields.Add(field);
                }

                csv.Add(fields);
            }

            foreach (List<string> list in csv)
            {
                Book b = new Book();
                for (int i = 0; i < list.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            b.Title = list[i];
                            break;
                        case 1:
                            b.Author = list[i];
                            break;
                        case 2:
                            b.Genre = list[i];
                            break;
                        case 3:
                            b.Status = StringToTrueOrFalse(list[i]);
                            break;
                        case 4:
                            b.HoldStatus = StringToTrueOrFalse(list[i]);
                            break;
                        case 5:
                            b.DueDate = ValidateDateTime(list[i]);
                            break;
                        case 6:
                            b.DeweySystem = list[i];
                            break;
                        case 7:
                            b.Synopsis = list[i];
                            break;
                        default:
                            break;
                    }
                }
                BookList.Add(b);
            }

            return BookList;
        }

        private DateTime ValidateDateTime(string dateTime)
        {
            try
            {
                return Convert.ToDateTime(dateTime);
            }
            catch (Exception e)
            {
                return DateTime.MinValue;
            }
        }

        private bool StringToTrueOrFalse(string boolean)
        {
            if (boolean.Trim().ToLower() == "true")
            {
                return true;
            }
            else if (boolean.Trim().ToLower() == "false")
            {
                return false;
            }
            else
                return true;//Assume it is not checked out if all else fails.
        }
    }
}
