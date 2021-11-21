namespace web.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public int AuthorID { get; set; }
        public int BookstoreId { get; set; }
        public int GenreID { get; set; }
        public string Title { get; set; }

        public Bookstore? Bookstore { get; set; }
        public Author? Author { get; set; }
        public ICollection<Genre>? Genres { get; set; }
    }
}