using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Models.BookstoreViewModels
{
    public class BookstoreIndexData
    {
        public IEnumerable<Bookstore> Bookstores { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}