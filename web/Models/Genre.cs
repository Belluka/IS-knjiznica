namespace web.Models
{
    public class Genre
    {
        public int GenreID { get; set; }
        public int BookID { get; set; }
        public string GenreName { get; set; }

        public Book? Book { get; set; }
    }
}