namespace web.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public int BookstoreId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Position { get; set; }

        public Bookstore? Bookstore { get; set; }
    }
}