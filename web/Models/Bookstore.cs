using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace web.Models
{
    public class Bookstore
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BookstoreId { get; set; }
        public string? Location { get; set; }
        public DateTime? DateEdited { get; set; }
        public ApplicationUser? Owner { get; set; }
        public ICollection<Book>? Books { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}