using System;

namespace BazhkoTarchyla.BookLibrary.DAO
{
    [Serializable]
    class LibraryDBFile : Interfaces.ILibrary
    {
        public string UUID { get; set; }
        public string Name { get; set; }
    }
}
