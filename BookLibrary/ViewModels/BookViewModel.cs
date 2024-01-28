using System.ComponentModel;

namespace BazhkoTarchyla.BookLibrary.UI.ViewModels
{
    public class BookViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        private Interfaces.IBook book;

        public BookViewModel(Interfaces.IBook _book)
        {
            book = _book;
        }

        public string BookUUID
        {
            get => book.UUID;
            set
            {
                book.UUID = value;
                RaisePropertyChanged(nameof(BookUUID));
            }
        }

        public string BookTitle
        {
            get => book.Title;
            set
            {
                book.Title = value;
                RaisePropertyChanged(nameof(BookTitle));
            }
        }

        public string BookAuthor
        {
            get => book.Author;
            set
            {
                book.Author = value;
                RaisePropertyChanged(nameof(BookAuthor));
            }
        }

        public int BookYear
        {
            get => book.Year;
            set
            {
                book.Year = value;
                RaisePropertyChanged(nameof(BookYear));
            }
        }

        public string BookLibrary
        {
            get => book.Library.Name;
            set
            {
                book.Library.Name = value;
                RaisePropertyChanged(nameof(BookYear));
            }
        }

        public BazhkoTarchyla.BookLibrary.Core.GenreType BookGenre
        {
            get => book.Genre;
            set
            {
                book.Genre = value;
                RaisePropertyChanged(nameof(BookGenre));
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
