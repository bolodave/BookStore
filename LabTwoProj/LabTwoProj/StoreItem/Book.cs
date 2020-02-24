using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labOneAssignment.StoreItem
{
    class Book
    {
        // // Field Variables
        //private string _bookAuthor; //Lastname, Firstname
        //private string _authorFirst;
        //private string _authorLast;
        //private string _ISBN;
        //private decimal _bookPrice;
        //private string _bookTitle;

        // Accessors
        public string bookAuthor { get; set; }
        public string authorFirst { get; set; }
        public string authorLast { get; set; }
        public string bookISBN { get; set; }
        public double bookPrice { get; set; }
        public string bookTitle { get; set; }
        //Using idea of auto-property instead of setting them as private

        // Default Constructor
        public Book()
        {
            authorFirst = "First_Name";
            authorLast = "Last_Name";
            bookAuthor = authorLast + ", " + authorFirst;
            bookISBN = "0" + "-" + "0000" + "-" + "00000";
            bookPrice = 0;
            bookTitle = "Book_Title";
        }
            // Constructor is called when object initialized without parameters

        // Parameterized Constructor (Overloader)
        public Book(string authorFirst, string authorLast, string bookISBN, double bookPrice, string bookTitle)
        {
            this.authorFirst = authorFirst;
            this.authorLast = authorLast;
            this.bookISBN = bookISBN;
            this.bookPrice = bookPrice;
            this.bookTitle = bookTitle;
            this.bookAuthor = this.authorLast + ", " + this.authorFirst;
        }
        // Something Something - from Github
    }
}
