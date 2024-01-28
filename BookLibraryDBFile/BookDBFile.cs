using System;
using Interfaces;
using BazhkoTarchyla.BookLibrary.Core;

namespace BazhkoTarchyla.BookLibrary.DAO
{
    [Serializable]
    public class BookDBFile : IBook
    {
        public string UUID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public ILibrary Library { get; set; }
        public GenreType Genre { get; set; }
        public int Year { get; set; }
    }

}
