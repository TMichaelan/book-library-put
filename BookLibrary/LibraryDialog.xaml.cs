using System;
using System.Windows;

namespace BazhkoTarchyla.BookLibrary.UI
{
    public partial class LibraryDialog : Window
    {
        public LibraryDialog()
        {
            InitializeComponent();
        }

        public LibraryDialog(Interfaces.ILibrary library)
        {
            InitializeComponent();
            libraryName.Text = library.Name;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            libraryName.SelectAll();
            libraryName.Focus();
        }

        public string LibraryName
        {
            get { return libraryName.Text; }
        }
    }
}