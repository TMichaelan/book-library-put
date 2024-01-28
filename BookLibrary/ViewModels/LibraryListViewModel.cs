using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BazhkoTarchyla.BookLibrary.UI.ViewModels
{
    public class LibraryListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<LibraryViewModel> Libraries { get; set; } = new ObservableCollection<LibraryViewModel>();

        public void RefreshList(System.Collections.Generic.IEnumerable<Interfaces.ILibrary> libraries)
        {
            Libraries.Clear();

            foreach (var library in libraries)
            {
                Libraries.Add(new LibraryViewModel(library));
            }

            RaisePropertyChanged(nameof(Libraries));
        }

        private void RaisePropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
