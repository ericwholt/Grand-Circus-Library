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
            this.BookList = new List<Book>();
        }


        public List<Book> GetBookList()
        {
            List<List<string>> csv = new List<List<string>>();
            string path = ConfigurationManager.AppSettings["Path"];
            string[] csvRows;
            if (File.Exists(path))
            {
                csvRows = File.ReadAllLines(ConfigurationManager.AppSettings["Path"]);
            }
            else
            {
                Console.WriteLine("Unable to connect to csv file");
                return null;
            }


            char sepChar = ',';
            char quoteChar = '"';

            foreach (string csvRow in csvRows)
            {
                List<string> fields = new List<string>();
                bool inQuotes = false;
                string field = "";
                Book b = new Book();
                for (int i = 0; i < csvRow.Length; i++)
                {
                    if (inQuotes)
                    {
                        if (i < csvRow.Length - 1 && csvRow[i] == quoteChar && csvRow[i + 1] == quoteChar)
                        {
                            i++;
                            field += quoteChar;
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
                            b.Publisher = list[i];
                            break;
                        case 4:
                            b.Status = StringToTrueOrFalse(list[i]);
                            break;
                        case 5:
                            b.Synopsis = list[i];
                            break;
                        default:
                            break;
                    }
                }
                BookList.Add(b);
            }
            Console.WriteLine("BookList Count: "+BookList.Count);
            return BookList;
        }

        private bool StringToTrueOrFalse(string boolean)
        {
            if (boolean.ToLower() == "true")
            {
                return true;
            }
            else if (boolean.ToLower() == "false")
            {
                return false;
            }
            else
                return false;
        }
    }
}
