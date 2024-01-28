using Interfaces;
using System;
using System.Collections.Generic;

namespace BazhkoTarchyla.BookLibrary.DAO
{
    class DAOFile : IDAO
    {
        private List<IBook> books;
        private List<ILibrary> libraries;

        private const string FILE_BOOKS = "books.bin";
        private const string FILE_LIBRARIES = "libraries.bin";

        public DAOFile()
        {
            try
            {
                books = Serializer.Deserialize<IBook>(FILE_BOOKS);
                libraries = Serializer.Deserialize<ILibrary>(FILE_LIBRARIES);
            }
            catch (Exception)
            {
                libraries = new List<ILibrary>();
                books = new List<IBook>();

                AddNewLibrary(new LibraryDBFile() { Name = "Arcadia Literary Haven" });
                AddNewLibrary(new LibraryDBFile() { Name = "Olympus Book Realm" });
                AddNewLibrary(new LibraryDBFile() { Name = "Serendipity Archives" });

                AddNewBook(new BookDBFile() { Author = "Eleanor Voss", Title = "Book1", Library = libraries[0], Genre = Core.GenreType.Fantasy, Year = 2019 });
                AddNewBook(new BookDBFile() { Author = "Marcus Reed", Title = "Book2",  Library = libraries[0], Genre = Core.GenreType.Mystery, Year = 2021 });
                AddNewBook(new BookDBFile() { Author = "Lila Ashton", Title = "Book3",  Library = libraries[1], Genre = Core.GenreType.Romance, Year = 2018 });
                AddNewBook(new BookDBFile() { Author = "Thomas K. Young", Title = "Book4", Library = libraries[2], Genre = Core.GenreType.Horror, Year = 2020 });

                Save();
            }
        }


        private void Save() {
            Serializer.Serialize(FILE_BOOKS, books);
            Serializer.Serialize(FILE_LIBRARIES, libraries);
        }

        public IBook AddNewBook(IBook book)
        {
            book.UUID = Guid.NewGuid().ToString();
            books.Add(book);
            Save();
            return book;
        }

        public ILibrary AddNewLibrary(ILibrary library)
        {
            library.UUID = Guid.NewGuid().ToString();
            libraries.Add(library);
            Save();
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
                    Save();
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
                    Save();
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
            Save();
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
            Save();
        }
    }
}
