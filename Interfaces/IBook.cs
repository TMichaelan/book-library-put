namespace Interfaces
{
    public interface IBook
    {
        string UUID { get; set; }
        string Title { get; set; }
        string Author { get; set; }
        ILibrary Library { get; set; }
        BazhkoTarchyla.BookLibrary.Core.GenreType Genre { get; set; }
        int Year { get; set; }
    }
}
