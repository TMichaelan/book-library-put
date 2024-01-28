using Microsoft.AspNetCore.Mvc;
using BazhkoTarchyla.BookLibrary.BL;
using BazhkoTarchyla.BookLibrary.Core;
using Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private BLC blc;
        //private readonly BLC blc = new BLC();


        public HomeController()
        {
            blc = new BLC();
            blc.LoadLibrary("BookLibraryDBMock002.dll");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Books()
        {
            var books = blc.GetAllBooks();
            return View(books);
        }

        public IActionResult Libraries()
        {
            var libraries = blc.GetAllLibraries();
            return View(libraries);
        }


        public IActionResult CreateBook()
        {
            ViewBag.Genres = GetGenreSelectList();
            ViewBag.Libraries = blc.GetAllLibraries().Select(lib =>
                new SelectListItem
                {
                    Text = lib.Name,
                    Value = lib.UUID.ToString()
                }).ToList();

            return View(new Book());
        }

        public IActionResult CreateLibrary()
        {

            return View();
        }

        public IActionResult EditBook(Guid id)
        {

            var book = blc.GetBook(id.ToString()).FirstOrDefault();
            var library = blc.GetLibrary(book.Library.UUID).FirstOrDefault();

            var bookModel = new Book
            {
                UUID = book.UUID,
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                Genre = book.Genre,
                LibraryId = library.UUID,
            };

            ViewBag.Genres = GetGenreSelectList();
            ViewBag.Libraries = blc.GetAllLibraries().Select(lib =>
                new SelectListItem
                {
                    Text = lib.Name,
                    Value = lib.UUID.ToString()
                }).ToList();

            return View(bookModel);
        }

        public IActionResult EditLibrary(Guid id)
        {
            var libraryMock = blc.GetLibrary(id.ToString()).FirstOrDefault();

            var libraryModel = new Library
            {
                UUID = libraryMock.UUID,
                Name = libraryMock.Name,
            };

            return View(libraryModel);
        }

        [HttpPost]
        public IActionResult ChangeDll(string dllPath)
        {
            try
            {
                //blc = new BLC();
                blc.LoadLibrary(dllPath);

                TempData["Message"] = "DLL successfully loaded.";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
                TempData["Error"] = "Error loading DLL: " + ex.Message;
            }
            return RedirectToAction("Index");
        }



        [HttpPost]
        public IActionResult CreateBook(Book model)
        {

            var library = blc.GetLibrary(model.LibraryId).FirstOrDefault();
            if (library == null)
            {
                ModelState.AddModelError("LibraryId", "Selected library does not exist.");
                return View(model);
            }

            var bookToAdd = new BazhkoTarchyla.BookLibrary.DAO.BookDBMock
            {
                Title = model.Title,
                Author = model.Author,
                Year = model.Year,
                Genre = model.Genre,
                Library = library
            };

            blc.CreateOrReplaceBook(bookToAdd);
            return RedirectToAction("Books");
        }

        [HttpPost]
        public IActionResult CreateLibrary(Library model)
        {

            var libraryToAdd = new BazhkoTarchyla.BookLibrary.DAO.LibraryDBMock
            {
                Name = model.Name,
            };

            blc.CreateOrReplaceLibrary(libraryToAdd);
            return RedirectToAction("Libraries");
        }

        [HttpPost]
        public IActionResult DeleteLibrary(Guid id)
        {

            blc.RemoveLibrary(id.ToString());
            return RedirectToAction("Libraries");
        }

        [HttpPost]
        public IActionResult DeleteBook(Guid id)
        {

            blc.RemoveBook(id.ToString());
            return RedirectToAction("Books");
        }

        [HttpPost]
        public IActionResult EditBook(Book model)
        {

            var library = blc.GetLibrary(model.LibraryId).FirstOrDefault();

            if (library == null)
            {
                ModelState.AddModelError("LibraryId", "Selected library does not exist.");
                return View(model);
            }

            var bookToEdit = new BazhkoTarchyla.BookLibrary.DAO.BookDBMock
            {
                UUID = model.UUID,
                Title = model.Title,
                Author = model.Author,
                Year = model.Year,
                Genre = model.Genre,
                Library = library
            };

            blc.CreateOrReplaceBook(bookToEdit);
            return RedirectToAction("Books");
        }

        [HttpPost]
        public IActionResult EditLibrary(Library model)
        {

            var libraryToEdit = new BazhkoTarchyla.BookLibrary.DAO.LibraryDBMock
            {
                UUID = model.UUID,
                Name = model.Name,
            };

            blc.CreateOrReplaceLibrary(libraryToEdit);
            return RedirectToAction("Libraries");
        }

        public IEnumerable<SelectListItem> GetGenreSelectList()
        {
            return Enum.GetValues(typeof(GenreType))
                .Cast<GenreType>()
                .Select(g => new SelectListItem
                {
                    Text = g.ToString(),
                    Value = ((int)g).ToString()
                })
                .ToList();
        }


        [HttpGet]
        public IActionResult SearchLibraries(string searchString)
        {
            var libraries = blc.GetAllLibraries();

            if (!String.IsNullOrEmpty(searchString))
            {
                libraries = libraries.Where(l => l.Name.Contains(searchString));
            }

            return View("Libraries", libraries);
        }

        [HttpGet]
        public IActionResult SearchTitle(string title)
        {
            var books = blc.GetAllBooks();

            if (!String.IsNullOrEmpty(title))
            {
                books = books.Where(b => b.Title.Contains(title));
            }

            return View("Books", books);
        }

        [HttpGet]
        public IActionResult SearchAuthor(string author)
        {
            var books = blc.GetAllBooks();

            if (!String.IsNullOrEmpty(author))
            {
                books = books.Where(b => b.Author.Contains(author));
            }

            return View("Books", books);
        }

        [HttpGet]
        public IActionResult SearchYear(string year)
        {
            var books = blc.GetAllBooks();

            if (!String.IsNullOrEmpty(year) && int.TryParse(year, out int yearInt))
            {
                books = books.Where(b => b.Year == yearInt);
            }

            return View("Books", books);
        }


    }
}
