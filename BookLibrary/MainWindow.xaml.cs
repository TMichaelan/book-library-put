using MaterialDesignThemes.Wpf;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BazhkoTarchyla.BookLibrary.UI
{
    public partial class MainWindow : Window
    {
        public ViewModels.LibraryListViewModel LibraryLVM { get; } = new ViewModels.LibraryListViewModel();
        private ViewModels.LibraryViewModel selectedLibrary = null;

        public ViewModels.BookListViewModel BookLVM { get; } = new ViewModels.BookListViewModel();
        private ViewModels.BookViewModel selectedBook = null;

        private readonly BL.BLC blc;

        private string selectedDataSource = "BookLibraryDBMock.dll";

        public MainWindow()
        {
            blc = new();
            blc.LoadLibrary(selectedDataSource);

            LibraryLVM.RefreshList(blc.GetAllLibraries());
            BookLVM.RefreshList(blc.GetAllBooks());

            InitializeComponent();

        }

        private void ApplyNewDataSource(object sender, RoutedEventArgs e) {
            try
            {
                blc.LoadLibrary(datasource.Text);
                LibraryLVM.RefreshList(blc.GetAllLibraries());
                BookLVM.RefreshList(blc.GetAllBooks());
                selectedDataSource = datasource.Text;
            }
            catch {
                MessageBox.Show("Error occurred, check your input values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                blc.LoadLibrary(selectedDataSource);
            }
        }

        private void ApplyBookSearch(object sender, RoutedEventArgs e)
        {
            if (bookSearchField.Text == "") {
                BookLVM.RefreshList(blc.GetAllBooks());
            } else {
                BookLVM.RefreshList(blc.GetBook(bookYearFilterField.Text));
            }
        }
        private void ApplyLibrarySearch(object sender, RoutedEventArgs e)
        {
            if (libraryUUIDSearchField.Text == "")
            {
                LibraryLVM.RefreshList(blc.GetAllLibraries());
            } else {
                LibraryLVM.RefreshList(blc.GetLibrary(libraryUUIDSearchField.Text));
            }
        }

        private void ApplyBookFilter(object sender, RoutedEventArgs e)
        {
            if (bookYearFilterField.Text == "")
            {
                BookLVM.RefreshList(blc.GetAllBooks());
            }
            else {
                try
                {
                    BookLVM.RefreshList(blc.GetBooks(int.Parse(bookYearFilterField.Text)));
                }
                catch
                {
                    MessageBox.Show("Bad filter provided");
                }
            }
        }
        private void ApplyLibraryFilter(object sender, RoutedEventArgs e)
        {
            if (libraryNameFilterField.Text == "")
            {
                LibraryLVM.RefreshList(blc.GetAllLibraries());
            }
            else {
                LibraryLVM.RefreshList(blc.GetLibraries(libraryNameFilterField.Text));
            }
        }

        private void AddBook(object sender, RoutedEventArgs e)
        {
            var allLibrariesNames = blc.GetAllLibrariesNames();
            BookDialog bookInputDialog = new(allLibrariesNames);

            if (bookInputDialog.ShowDialog() == true)
            {
                DAO.BookDBMock book;
                try
                {
                    book = new DAO.BookDBMock()
                    {
                        Title = bookInputDialog.BookTitle,
                        Author = bookInputDialog.BookAuthor,
                        Library = blc.GetLibraries(bookInputDialog.Library).First(),
                        Genre = bookInputDialog.BookGenre,
                        Year = bookInputDialog.BookYear
                    };
                }
                catch
                {
                    MessageBox.Show("Error occurred, check your input values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                blc.CreateOrReplaceBook(book);
                BookLVM.RefreshList(blc.GetAllBooks());
            }
        }
        private void AddLibrary(object sender, RoutedEventArgs e)
        {
            LibraryDialog libraryInputDialog = new();

            if (libraryInputDialog.ShowDialog() == true)
            {
                DAO.LibraryDBMock libraryDBMock;
                try
                {
                    libraryDBMock = new DAO.LibraryDBMock() { Name = libraryInputDialog.LibraryName };
                }
                catch
                {
                    MessageBox.Show("Error occurred, check your input values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                blc.CreateOrReplaceLibrary(libraryDBMock);
                LibraryLVM.RefreshList(blc.GetAllLibraries());
            }
        }

        private void EditBook(object sender, RoutedEventArgs e)
        {
            if (selectedBook != null)
            {
                BookDialog bookEditDialog = new(
                    blc.GetAllLibrariesNames(),
                    blc.GetBook(selectedBook.BookUUID).First()
                );

                if (bookEditDialog.ShowDialog() == true)
                {
                    blc.CreateOrReplaceBook(new DAO.BookDBMock()
                    {
                        UUID = selectedBook.BookUUID,
                        Title = bookEditDialog.BookTitle,
                        Author = bookEditDialog.BookAuthor,
                        Genre = bookEditDialog.BookGenre,
                        Library = blc.GetLibraries(bookEditDialog.Library).First(),
                        Year = bookEditDialog.BookYear
                    });

                    BookLVM.RefreshList(blc.GetAllBooks());
                    ChangeSelectedBook(null);
                }
            }
            else
            {
                MessageBox.Show("Book is not selected!");
            }
        }
        private void EditLibrary(object sender, RoutedEventArgs e)
        {
            if (selectedLibrary != null)
            {
                LibraryDialog libraryEditDialog = new(
                    blc.GetLibrary(selectedLibrary.LibraryUUID).First()
                );

                if (libraryEditDialog.ShowDialog() == true)
                {
                    blc.CreateOrReplaceLibrary(new DAO.LibraryDBMock()
                    {
                        UUID = selectedLibrary.LibraryUUID,
                        Name = libraryEditDialog.LibraryName
                    });

                    LibraryLVM.RefreshList(blc.GetAllLibraries());
                    BookLVM.RefreshList(blc.GetAllBooks());
                    ChangeSelectedLibrary(null);
                }
            }
            else
            {
                MessageBox.Show("Library is not selected!");
            }
        }

        private void RemoveBook(object sender, RoutedEventArgs e)
        {
            if (selectedBook != null)
            {
                blc.RemoveBook(selectedBook.BookUUID);
                BookLVM.RefreshList(blc.GetAllBooks());
                selectedBook = null;
            }
            else
            {
                MessageBox.Show("Book is not selected!");
            }
        }
        private void RemoveLibrary(object sender, RoutedEventArgs e)
        {
            if (selectedLibrary != null)
            {
                blc.RemoveLibrary(selectedLibrary.LibraryUUID);
                LibraryLVM.RefreshList(blc.GetAllLibraries());
                ChangeSelectedLibrary(null);
                selectedLibrary = null;
            }
            else
            {
                MessageBox.Show("Library is not selected!");
            }
        }

        private void ChangeSelectedBook(ViewModels.BookViewModel bookViewModel)
        {
            selectedBook = bookViewModel;
            DataContext = selectedBook;
        }
        private void ChangeSelectedLibrary(ViewModels.LibraryViewModel libraryViewModel)
        {
            selectedLibrary = libraryViewModel;
            DataContext = selectedLibrary;
        }

        private void BookList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                ChangeSelectedBook((ViewModels.BookViewModel)e.AddedItems[0]);
            }
        }
        private void LibraryList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                ChangeSelectedLibrary((ViewModels.LibraryViewModel)e.AddedItems[0]);
            }
        }
    }
}