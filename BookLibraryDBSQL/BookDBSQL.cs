using Interfaces;
using BazhkoTarchyla.BookLibrary.Core;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace BazhkoTarchyla.BookLibrary.DAO
{
    public class BookDBSQL
    {
        [Key]
        public string UUID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string LibraryUUID { get; set; }
        public GenreType Genre { get; set; }
        public int Year { get; set; }

        public IBook ToIBook(List<LibraryDBSQL> libraries) {
            return new Book() { UUID = UUID, Author = Author, Library = libraries.Single(f => f.UUID.Equals(LibraryUUID)).ToILibrary(), Genre = Genre, Year = Year };
        }
    }

    class Book : IBook
    {
        public string UUID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public ILibrary Library { get; set; }
        public GenreType Genre { get; set; }
        public int Year { get; set; }
    }
}
