using System;

namespace Grand_Circus_Library
{
    class SearchView : IView
    {
        private string SearchType { get; set; }
        public SearchView(string SearchType)
        {
            this.SearchType = SearchType;
        }

        public void Display()
        {
            Console.Clear();
            if (SearchType == "All")
            {
                Console.WriteLine($"All Books in Grand Circus Library: ");
            }
            else
            {
                Console.Write($"{SearchType} of the book: ");
            }
        }
    }
}
