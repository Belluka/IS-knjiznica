using web.Data;
using web.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace web.Data
{
    public static class DbInitializer
    {
        public static void Initialize(LibraryContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Authors.Any())
            {
                return;   // DB has been seeded
            }

            var authors = new Author[]
            {
                new Author{FirstMidName="Vladimir",LastName="Nabokov"},
                new Author{FirstMidName="J.R.R",LastName="Tolkien"},
                new Author{FirstMidName="France",LastName="Presern"},
                new Author{FirstMidName="Ivan",LastName="Cankar"},
                new Author{FirstMidName="Albert",LastName="Camus"},
                new Author{FirstMidName="Fyodor",LastName="Dostoevsky"},
                new Author{FirstMidName="George",LastName="Orwell"},
                new Author{FirstMidName="Dan",LastName="Brown"}
            };

            context.Authors.AddRange(authors);
            context.SaveChanges();

            var bookstores = new Bookstore[]
            {
                new Bookstore{BookstoreId=1050,Location="Ljubljana zalog"},
                new Bookstore{BookstoreId=4022,Location="Ljubljana rudnik"},
                new Bookstore{BookstoreId=4041,Location="Maribor"},
                new Bookstore{BookstoreId=1045,Location="Zagorje"},
                new Bookstore{BookstoreId=3141,Location="Trbovlje"},
                new Bookstore{BookstoreId=2021,Location="Litija"},
                new Bookstore{BookstoreId=2042,Location="Kranj"}
            };

            context.Bookstores.AddRange(bookstores);
            context.SaveChanges();

            var books = new Book[]
            {
                new Book{AuthorID=1,BookstoreId=1050,GenreID=1,Title="Lolita"},
                new Book{AuthorID=1,BookstoreId=4022,GenreID=10,Title="Pale Flame"},
                new Book{AuthorID=1,BookstoreId=4041,GenreID=9,Title="The Gift"},
                new Book{AuthorID=2,BookstoreId=1045,GenreID=11,Title="Lord of the Rings"},
                new Book{AuthorID=2,BookstoreId=3141,GenreID=5,Title="Lord of the Rings 2"},
                new Book{AuthorID=2,BookstoreId=2021,GenreID=3,Title="Lord of the Rings 3"},
                new Book{AuthorID=5,BookstoreId=1050,GenreID=8,Title="The Stranger"},
                new Book{AuthorID=4,BookstoreId=1050,GenreID=6,Title="Naslov knjige"},
                new Book{AuthorID=4,BookstoreId=4022,GenreID=12,Title="Naslov knjige"},
                new Book{AuthorID=5,BookstoreId=4041,GenreID=7,Title="Naslov knjige"},
                new Book{AuthorID=6,BookstoreId=1045,GenreID=4,Title="Naslov knjige"},
                new Book{AuthorID=7,BookstoreId=3141,GenreID=2,Title="Naslov knjige"},
            };

            context.Books.AddRange(books);
            context.SaveChanges();

            var employees = new Employee[]
            {
                new Employee{ BookstoreId=1050},
                new Employee{ BookstoreId=4022},
                new Employee{ BookstoreId=1045},
                new Employee{ BookstoreId=1050},
                new Employee{ BookstoreId=4022},
                new Employee{ BookstoreId=1045},
                new Employee{ BookstoreId=1050},
                new Employee{ BookstoreId=2021},
                new Employee{ BookstoreId=3141},
                new Employee{ BookstoreId=1050},
                new Employee{ BookstoreId=3141},
                new Employee{ BookstoreId=4022},
            };

            context.Employees.AddRange(employees);
            context.SaveChanges();

            var genres = new Genre[]
            {
                new Genre{GenreName="non-fiction", BookID=1},
                new Genre{GenreName="historical", BookID=2},
                new Genre{GenreName="romance", BookID=3},
                new Genre{GenreName="novella", BookID=4},
                new Genre{GenreName="poetry", BookID=5},
                new Genre{GenreName="horror", BookID=6},
                new Genre{GenreName="poetry", BookID=7},
                new Genre{GenreName="historical", BookID=8},
                new Genre{GenreName="educational", BookID=9},
                new Genre{GenreName="tragedy", BookID=10},
                new Genre{GenreName="drama", BookID=3},
                new Genre{GenreName="comedy", BookID=7},
            };

            context.Genres.AddRange(genres);
            context.SaveChanges();

             var roles = new IdentityRole[] {
                new IdentityRole{Id="1", Name="Administrator"},
                new IdentityRole{Id="2", Name="Manager"},
                new IdentityRole{Id="3", Name="Staff"}
            };

            foreach (IdentityRole r in roles)
            {
                context.Roles.Add(r);
            }

            var user = new ApplicationUser
            {
                FirstName = "Luka",
                LastName = "Belloni",
                City = "Ljubljana",
                Email = "lb@gmail.com",
                NormalizedEmail = "XXXX@GMAIL.COM",
                UserName = "lb@gmail.com",
                NormalizedUserName = "lb@gmail.com",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user,"Geslo123!");
                user.PasswordHash = hashed;
                context.Users.Add(user);
                
            }

            context.SaveChanges();
            

            var UserRoles = new IdentityUserRole<string>[]
            {
                new IdentityUserRole<string>{RoleId = roles[0].Id, UserId=user.Id},
                new IdentityUserRole<string>{RoleId = roles[1].Id, UserId=user.Id},
            };

            foreach (IdentityUserRole<string> r in UserRoles)
            {
                context.UserRoles.Add(r);
            }

        }
    }
}