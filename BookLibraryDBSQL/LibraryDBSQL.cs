using Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BazhkoTarchyla.BookLibrary.DAO
{
    public class LibraryDBSQL
    {
        [Key]
        public string UUID { get; set; }
        public string Name { get; set; }
        public ILibrary ToILibrary() {
            return new Library() { UUID = UUID, Name = Name };
        }
    }

    class Library : ILibrary {
        public string UUID { get; set; }
        public string Name { get; set; }
    }
}
