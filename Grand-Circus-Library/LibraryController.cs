using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grand_Circus_Library
{
 
    class LibraryController
    {
        public List<Book> LibraryDb { get; }

        public LibraryController(List<Book> LibraryDb)
        {
            this.LibraryDb = LibraryDb;
        }

        public void LibraryMenu()
        {
            BookMenuView bmv = new BookMenuView();
            bmv.Display();
        }

    }
}
