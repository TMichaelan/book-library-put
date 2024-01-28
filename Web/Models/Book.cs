using BazhkoTarchyla.BookLibrary.Core;
using System.ComponentModel.DataAnnotations;


namespace Web.Models
{
    public class Book
    {
        
        public string UUID {  get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Library is required")]
        public string LibraryId { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public GenreType Genre { get; set; }

        [Required(ErrorMessage = "Year is required")]
        public int Year { get; set; }
    }
}
