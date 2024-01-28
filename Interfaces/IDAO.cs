using System.Collections.Generic;

namespace Interfaces
{
    public interface IDAO
    {
        IEnumerable<IBook> GetAllBooks();
        IEnumerable<ILibrary> GetAllLibraries();

        IBook AddNewBook(IBook book);
        ILibrary AddNewLibrary(ILibrary library);
        
        void RemoveBook(string uuid);
        void RemoveLibrary(string uuid);

        void ReplaceBook(IBook newBook);
        void ReplaceLibrary(ILibrary newLibrary);
    }
}
