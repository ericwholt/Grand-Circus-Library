using System;

namespace Grand_Circus_Library
{
    class BookSearchDeweyView : IView
    {
        public void Display()
        {
          //dewey decimal system is for category search with call numbers. first 3 digits are subject and last 3 are last name of author.
            Console.Clear();
            Console.WriteLine("Dewey Decimal System: ");
            Console.WriteLine("FIC Fiction");
            Console.WriteLine("000 Computer Science, Information & General Works");
            Console.WriteLine("100 Philosophy & Psychology");
            Console.WriteLine("200 Religion");
            Console.WriteLine("300 Social Sciences");
            Console.WriteLine("400 Language");
            Console.WriteLine("500 Science");
            Console.WriteLine("600 Technology");
            Console.WriteLine("700 Arts & recreation");
            Console.WriteLine("800 Literature");
            Console.WriteLine("900 History & Geography");

            Console.Write("Select a category FIC to 900 to search on: ");
        }
    }
}
