using System.ComponentModel;

namespace BazhkoTarchyla.BookLibrary.UI.ViewModels
{
    public class LibraryViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        private Interfaces.ILibrary library;

        public LibraryViewModel(Interfaces.ILibrary _library)
        {
            library = _library;
        }

        public string LibraryUUID
        {
            get => library.UUID;
            set
            {
                library.UUID = value;
                RaisePropertyChanged(nameof(LibraryUUID));
            }
        }

        public string LibraryName
        {
            get => library.Name;
            set
            {
                library.Name = value;
                RaisePropertyChanged(nameof(LibraryName));
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
