using Bogus;
using SharedP.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSeeder
{
    public class BookSeeder
    {
        public static List<Book> GenerateBookData()
        {
            int bookId = 1;
            var bookFaker = new Faker<Book>()
                .UseSeed(123)
                .RuleFor(x => x.Title, x => x.Lorem.Word())
                .RuleFor(x => x.Author, x => x.Name.FullName())
                .RuleFor(x => x.Id, x => bookId++);

            return bookFaker.Generate(11).ToList();
        }
    }
}
