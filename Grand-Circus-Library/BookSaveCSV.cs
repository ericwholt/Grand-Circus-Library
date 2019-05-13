using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grand_Circus_Library
{
    class BookSaveCSV
    {
        public List<Book> BookList { get; set; }
        private string Path { get { return ConfigurationManager.AppSettings["Path"];  } }
        
        public BookSaveCSV(List<Book> BookList)
        {
            this.BookList = new List<Book>(BookList);
        }
        //creates a csv of books with status/duedates/holds
        public void SaveBookList()
        {
            StringBuilder sb = new StringBuilder();
            string header = "Title,Author,Genre,Status,HoldStatus,DueDate,CallNumber,Synopsis";

            sb.AppendLine(header);

            foreach (Book book in BookList)
            {
                sb.AppendLine(book.ToString());
            }
        try
        {
          File.WriteAllText(Path, sb.ToString());
        }
        catch (Exception e)
        {
        Console.WriteLine("Unable to save file.");
        }
            
        }
    }
}
