using System;
using System.Collections.Generic;
using Interfaces;

namespace BazhkoTarchyla.BookLibrary.DAO
{
    public class DAOMock : IDAO
    {
        private static List<ILibrary> libraries = new List<ILibrary>();
        private static List<IBook> books = new List<IBook>();
        private static bool isInitialized = false;

        public DAOMock()
        {
            if (!isInitialized)
            {
                InitializeLibraries();
                InitializeBooks();
                isInitialized = true;
            }
        }

        private void InitializeLibraries()
        {
            libraries.Add(new LibraryDBMock() { UUID = Guid.NewGuid().ToString(), Name = "Arcadia Literary Haven" });
            libraries.Add(new LibraryDBMock() { UUID = Guid.NewGuid().ToString(), Name = "Olympus Book Realm" });
            libraries.Add(new LibraryDBMock() { UUID = Guid.NewGuid().ToString(), Name = "Serendipity Archives" });
        }

        private void InitializeBooks()
        {
            books.Add(new BookDBMock() { UUID = Guid.NewGuid().ToString(), Author = "Eleanor Voss", Title = "1234", Library = libraries[0], Genre = Core.GenreType.Fantasy, Year = 2019 });
            books.Add(new BookDBMock() { UUID = Guid.NewGuid().ToString(), Author = "Marcus Reed", Title = "1234", Library = libraries[0], Genre = Core.GenreType.Mystery, Year = 2021 });
            books.Add(new BookDBMock() { UUID = Guid.NewGuid().ToString(), Author = "Lila Ashton", Title = "1234", Library = libraries[1], Genre = Core.GenreType.Romance, Year = 2018 });
            books.Add(new BookDBMock() { UUID = Guid.NewGuid().ToString(), Author = "Thomas K. Young", Title = "1234", Library = libraries[2], Genre = Core.GenreType.Horror, Year = 2020 });
        }

         
        public IBook AddNewBook(IBook book)
        {
            books.Add(book);
            return book;
        }

        public ILibrary AddNewLibrary(ILibrary library)
        {
            libraries.Add(library);
            return library;
        }

        public IEnumerable<IBook> GetAllBooks()
        {
            return books;
        }

        public IEnumerable<ILibrary> GetAllLibraries()
        {
            return libraries;
        }

        public void RemoveBook(string uuid)
        {
            foreach (IBook book in books)
            {
                if (book.UUID.Equals(uuid))
                {
                    books.Remove(book);
                    return;
                }
            }
        }

        public void RemoveLibrary(string uuid)
        {
            foreach (ILibrary library in libraries)
            {
                if (library.UUID.Equals(uuid))
                {
                    libraries.Remove(library);
                    return;
                }
            }
        }

        public void ReplaceBook(IBook newBook)
        {
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].UUID.Equals(newBook.UUID))
                {
                    books[i] = newBook;
                    break;
                }
            }
        }

        public void ReplaceLibrary(ILibrary newLibrary)
        {
            for (int i = 0; i < libraries.Count; i++)
            {
                if (libraries[i].UUID.Equals(newLibrary.UUID))
                {
                    libraries[i].Name = newLibrary.Name;
                    break;
                }
            }
        }
    }
}
