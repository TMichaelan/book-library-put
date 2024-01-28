using System;
using System.Windows;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Interfaces;
using System.Windows.Controls;

namespace BazhkoTarchyla.BookLibrary.UI
{
    public partial class BookDialog : Window
    {
        public BookDialog(System.Collections.Generic.IEnumerable<string> libraries)
        {
            InitializeComponent();
            libraries.ToList().ForEach(f => library.Items.Add(f));
            if (library.Items.Count > 0)
            {
                library.SelectedIndex = 0;
            }

            foreach (var genre in Enum.GetValues(typeof(BazhkoTarchyla.BookLibrary.Core.GenreType)))
            {
                bookGenre.Items.Add(genre.ToString());
            }
            bookGenre.SelectedIndex = 0;
        }

        public BookDialog(System.Collections.Generic.IEnumerable<string> library, Interfaces.IBook book)
        {
            InitializeComponent();
            library.ToList().ForEach(f => this.library.Items.Add(f));

            for (int i = 0; i < library.Count(); i++)
            {
                if (library.ElementAt(i).Equals(book.Library.Name))
                {
                    this.library.SelectedIndex = i;
                    break;
                }
            }

            foreach (var genre in Enum.GetValues(typeof(BazhkoTarchyla.BookLibrary.Core.GenreType)))
            {
                bookGenre.Items.Add(genre.ToString());
            }

            bookTitle.Text = book.Title;
            bookAuthor.Text = book.Author;
            bookGenre.SelectedIndex = (int)book.Genre;
            bookYear.Text = book.Year.ToString();
        }



        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            bookAuthor.SelectAll();
            bookAuthor.Focus();
        }

        public string BookTitle
        {
            get { return bookTitle.Text; }
        }

        public string BookAuthor
        {
            get { return bookAuthor.Text; }
        }

        //public BazhkoTarchyla.BookLibrary.Core.GenreType BookGenre
        //{
          //  get { return bookGenre.SelectedIndex == 0 ? BazhkoTarchyla.BookLibrary.Core.GenreType.Horror : BazhkoTarchyla.BookLibrary.Core.GenreType.Romance; }
        //}

        public BazhkoTarchyla.BookLibrary.Core.GenreType BookGenre
        {
            get
            {
                return (BazhkoTarchyla.BookLibrary.Core.GenreType)Enum.Parse(
                    typeof(BazhkoTarchyla.BookLibrary.Core.GenreType),
                    bookGenre.SelectedItem.ToString()
                );
            }
        }

        public int BookYear
        {
            get
            {
                return int.Parse(bookYear.Text);
            }
        }

        public string Library
        {
            get
            {
                return library.Text;
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}