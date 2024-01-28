using Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BazhkoTarchyla.BookLibrary.DAO
{
    public class DAOSQL : DbContext, IDAO
    {
        public DbSet<BookDBSQL> BooksRelation { get; set; }
        public DbSet<LibraryDBSQL> LibrariesRelation { get; set; }

        public string DbPath { get; }

        public DAOSQL() {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join("C:\\Study\\PW\\my_pw", "books.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");


        public IBook AddNewBook(IBook book)
        {
            book.UUID = Guid.NewGuid().ToString();

            Add(new BookDBSQL() {
                UUID = book.UUID,
                Author = book.Author,
                Title = book.Title,
                Year = book.Year,
                Genre = book.Genre,
                LibraryUUID = book.Library.UUID
            });

            SaveChanges();

            return book;
        }

        public ILibrary AddNewLibrary(ILibrary library)
        {
            library.UUID = Guid.NewGuid().ToString();
            
            Add(new LibraryDBSQL() {
                UUID = library.UUID,
                Name = library.Name
            });

            SaveChanges();

            return library;
        }
        
        public IEnumerable<IBook> GetAllBooks()
        {
            return from a in BooksRelation select a.ToIBook(LibrariesRelation.ToList());
        }

        public IEnumerable<ILibrary> GetAllLibraries()
        {
            return from f in LibrariesRelation select f.ToILibrary();
        }

        public void RemoveBook(string uuid)
        {
            var book = (from a in BooksRelation where a.UUID == uuid select a).First();
            Remove(book);
            SaveChanges();
        }

        public void RemoveLibrary(string uuid)
        {
            var library = (from f in LibrariesRelation where f.UUID == uuid select f).First();
            Remove(library);
            SaveChanges();
        }

        public void ReplaceBook(IBook newBook)
        {
            var book = (from a in BooksRelation where a.UUID == newBook.UUID select a).First();
            book.Title = newBook.Title;
            book.Author = newBook.Author;
            book.Genre = newBook.Genre;
            book.LibraryUUID = newBook.Library.UUID;
            book.Year = newBook.Year;

            Entry(book).CurrentValues.SetValues(book);
        }

        public void ReplaceLibrary(ILibrary newLibrary)
        {
            var library = (from f in LibrariesRelation where f.UUID == newLibrary.UUID select f).First();
            library.Name = newLibrary.Name;

            Entry(library).CurrentValues.SetValues(newLibrary);
        }

    }

}
