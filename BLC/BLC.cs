using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BazhkoTarchyla.BookLibrary.BL
{
    public class BLC
    {
        private Interfaces.IDAO dao;

        public void LoadLibrary(string dllPath) {
            Type typeToCreate = null;

            Type IDAOType = typeof(Interfaces.IDAO);
            foreach (var t in Assembly.UnsafeLoadFrom(dllPath).GetTypes())
            {
                if (t.IsAssignableTo(IDAOType))
                {
                    typeToCreate = t;
                    break;
                }
            }

            if (typeToCreate != null)
            {
                dao = (Interfaces.IDAO) Activator.CreateInstance(typeToCreate);
            }
            else {
                throw new Exception("incompatible dao");
            }
        }

        public virtual IEnumerable<Interfaces.IBook> GetAllBooks() {
            return dao.GetAllBooks();
        }
        public virtual IEnumerable<Interfaces.ILibrary> GetAllLibraries() {
            return dao.GetAllLibraries();
        }

        public IEnumerable<Interfaces.IBook> GetBook(string uuid)
        {
            return from a in dao.GetAllBooks() where a.UUID.Equals(uuid) select a;
        }
        public IEnumerable<Interfaces.ILibrary> GetLibrary(string uuid) {
            return from f in dao.GetAllLibraries() where f.UUID.Equals(uuid) select f;
        }

        public IEnumerable<Interfaces.IBook> GetBooks(int year) {
            return from a in dao.GetAllBooks() where a.Year == year select a;
        }
        public IEnumerable<Interfaces.ILibrary> GetLibraries(string name) {
            return from f in dao.GetAllLibraries() where f.Name.StartsWith(name) select f;
        }

        public void RemoveBook(string uuid) {
            dao.RemoveBook(uuid);
        }
        public void RemoveLibrary(string uuid) {
            dao.RemoveLibrary(uuid);
        }

        public void CreateOrReplaceBook(Interfaces.IBook book) {
            if (book.UUID == null)
            {
                book.UUID = Guid.NewGuid().ToString();
                dao.AddNewBook(book);
            }
            else {
                dao.ReplaceBook(book);
            }
        }
        public void CreateOrReplaceLibrary(Interfaces.ILibrary library)
        {
            if (library.UUID == null)
            {
                library.UUID = Guid.NewGuid().ToString();
                dao.AddNewLibrary(library);
            }
            else
            {
                dao.ReplaceLibrary(library);
            }
        }

        public IEnumerable<string> GetAllLibrariesNames() {
            return from f in dao.GetAllLibraries() select f.Name;
        }
    }
}
