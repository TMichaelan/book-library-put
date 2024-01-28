using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BazhkoTarchyla.BookLibrary.UI.ViewModels
{
    public class BookListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<BookViewModel> Books { get; set; } = new ObservableCollection<BookViewModel>();

        public void RefreshList(System.Collections.Generic.IEnumerable<Interfaces.IBook> books)
        {
            Books.Clear();

            foreach (var book in books)
            {
                Books.Add(new BookViewModel(book));
            }

            RaisePropertyChanged(nameof(Books));
        }

        private void RaisePropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
