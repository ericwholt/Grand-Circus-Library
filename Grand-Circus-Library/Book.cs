using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grand_Circus_Library
{
    class Book
    {
    //Book properties
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public string Synopsis { get; set; }
        public bool Status { get; set; }
        public DateTime DueDate { get; set; }
        public string DeweySystem { get; set; }
        public bool HoldStatus { get; set; }
    //used for csv
        public override string ToString()
        {
            return $"\"{Title}\",{Author},{Genre},{Status},{HoldStatus},{DueDate},{DeweySystem},\"{Synopsis}\"";
        }
    }
}
