using System;

namespace BazhkoTarchyla.BookLibrary.DAO
{
    [Serializable]
    public class LibraryDBMock : Interfaces.ILibrary
    {
        public string UUID { get; set; }
        public string Name { get; set; }
    }
   
}
