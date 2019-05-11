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

        public void SaveBookList()
        {
            StringBuilder sb = new StringBuilder();
            string header = "Title,Author,Genre,Status,DueDate,CallNumber, Synopsis";

            sb.AppendLine(header);

            foreach (Book book in BookList)
            {
                sb.AppendLine(book.ToString());
            }
            //Console.WriteLine(sb.ToString());
            File.WriteAllText(Path, sb.ToString());
        }
    }
}
