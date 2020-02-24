using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using labOneAssignment.StoreItem;

/// <summary>
/// 
/// Developer: Lauren Dominic Bolo-Dave (819088693)
/// CompE 561 - Professor Shams Al Ajrawi
/// 
/// Lab 1 [Check point]
/// Goal: Create a simple graphical user interface for a book store order form.
///         ~PART #1:
///                (Done) -Book Class 
///                             a) Parameters: Author, ISBN, Price, and Title
///                (Done) -Hard code 3 book objects; loaded into combo box
///                (Done) -Text boxes populated when book title selected from combo box
///                (Done) -Order added into a DataGridView WHEN user types in a quantity(#)
///         
///         ~PART #2:
///                (Done) -Calculate the subtotal, 10% tax, and over all total of user's order
///                (...)  -Additionally:
///                            [Done] a) Prompt message box when user presses add title button without
///                                         a book selected.
///                            [Done] b) Prompt message box when user does not enter a valid number
///                                         of books to order when press add title.
///                            [Done] c) Prompt users to select a book when confirmed button is pressed
///                                         without any books selected.
///                            [Done] d) If cancel order button, ask user if they are sure about canceling
///                                         said order. If yes, clear DataGridView and textboxes (tax, subtotal, total)
///                                         will be set back to zero. If no, nothing will happen.
/// 
/// *******************************************************************************************************************
/// 
/// Lab 2 [Check points]
/// Goal: Implementation of writing into a text file
/// 
///         ~PART #3:
///                 () -Add using System.IO
///                 () -Create text file called book.txt to hold book objects
///                 () -Write a loop to process all books into an ArrayList that comes from the IO
///                 () -Write statements to load the Combo box with book items
///                 () -Use a try/catch to handle IO errors
///                 () -Write order to a file called orders.txt when the complete order button is click add proper IO
///                     error handling to ensure that no errors will be displayed when writing the information to the file 
/// 
/// </summary>

namespace labOneAssignment
{
    public partial class Form1 : Form
    {
        ///**Initializing Parameters**
        //List of Book objects
        List<Book> booksList = new List<Book>();

        public Form1()
        {
            InitializeComponent();

            //Populating Textboxes to use for later calculation
            textBoxSubtotal.Text = "0.0";
            textBoxTax.Text = "0.0";
            textBoxTotal.Text = "0.0";
        }

        /// <summary>
        /// Populating Combo box with 3 hardcoded objects of type Book
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            //Initializing new Book objects
            Book book1 = new Book("Gary", "Keller", "9781885167774", 15.00, "The One Thing");
            Book book2 = new Book("Greg", "McKeown", "9780753550281", 20.00, "Essentialism: The Disciplined Pursuit of Less");
            Book book3 = new Book("John", "Flanagan", "9781436123990", 10.00, "Ranger's Apprentice: The Ruins of Gorlan");

            //Adding newly made Book objects into a list
            booksList.Add(book1);
            booksList.Add(book2);
            booksList.Add(book3);

            foreach (Book oBooks in booksList)
            {
                comboBox1.Items.Add(oBooks.bookTitle);
            }
        }

        /// <summary>
        /// Event: User selects a book title in drop down menu
        /// </summary>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Book oBook = (Book)booksList[comboBox1.SelectedIndex];

            textBoxAuthor.Text = oBook.bookAuthor;
            textBoxISBN.Text = oBook.bookISBN;
            textBoxPrice.Text = oBook.bookPrice.ToString("C");
        }

        /// <summary>
        /// Event: User presses "Add Title" button
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            ///Account for basic user errors
            //If no books are selected by user
            if (textBoxAuthor.Text == "")
            {
                //Prompt user
                MessageBox.Show("Please select a book!");
                comboBox1.Focus();
            }
            //If text box (Quantity) empty or value < 1
            else if (textBoxQuantity.Text == "" || Convert.ToInt32(textBoxQuantity.Text) < 1)
            {
                //Prompt user
                MessageBox.Show("Please enter a quantity #!");
                textBoxQuantity.Focus();
            }
            else
            {
                //Populate the rows with parameters in terms with the user
                Book oBook = (Book)booksList[comboBox1.SelectedIndex];
                double LineTotal = oBook.bookPrice * Convert.ToInt32(textBoxQuantity.Text);
                dataGridView1.Rows.Add(oBook.bookTitle, oBook.bookPrice, textBoxQuantity.Text, LineTotal.ToString("C"));

                //Errors will arise when "$" is not replaced (Pertaining to string-double conversion)
                string sSubTotal = textBoxSubtotal.Text.Replace("$", "");  //Removes "$"
                if (sSubTotal.Equals(""))   //If txtBox empty, initialize with string "0"
                {
                    sSubTotal = "0";
                }

                //Calculating Sub-Total
                double dSubTotal = Convert.ToDouble(sSubTotal);
                dSubTotal = LineTotal + dSubTotal;  //Adding up the consecutive prices on the list that the user selected
                //Calculating 10% tax
                double tax = dSubTotal * 0.1;
                //Adding up total
                double total = dSubTotal + tax;

                //Populate the Subtotal, text, and total text boxes
                textBoxSubtotal.Text = dSubTotal.ToString("C");
                textBoxTax.Text = tax.ToString("C");
                textBoxTotal.Text = total.ToString("C");
            }
        }

        /// <summary>
        /// Event: User presses cancel button.
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Do you really want to cancel the order?", "Cancel Order", MessageBoxButtons.YesNo))
            {
                textBoxAuthor.Text = "";
                textBoxISBN.Text = "";
                textBoxPrice.Text = "";
                textBoxQuantity.Text = "";
                textBoxTotal.Text = "";
                textBoxTax.Text = "";
                textBoxSubtotal.Text = "";
                dataGridView1.Rows.Clear();
            }
        }

        /// <summary>
        /// Event: User presses Confirm button.
        ///        Confirm button is N/A at the moment, until further labs.
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount >= 1)
            {
                MessageBox.Show("You have placed an order!");
                textBoxAuthor.Text = "";
                textBoxISBN.Text = "";
                textBoxPrice.Text = "";
                textBoxQuantity.Text = "";
                textBoxTotal.Text = "";
                textBoxTax.Text = "";
                textBoxSubtotal.Text = "";
                dataGridView1.Rows.Clear();
            }
            //Prompt user error message when button pressed with none of the books selected
            else
            {
                MessageBox.Show("Error! Please select a book.");
                comboBox1.Focus();
            }
        }
    }
}
