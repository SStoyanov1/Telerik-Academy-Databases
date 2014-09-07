namespace BookStore.Data
{
    using System.Data.Entity;

    using BookStore.Models;
    using BookStore.Data.Migrations;

    public class BookStoreDbContex: DbContext
    {
        public BookStoreDbContex()
            :base("BookStoreConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BookStoreDbContex, Configuration>());
        }

        public IDbSet<Book> Books { get; set; }

        public IDbSet<Review> Reviews { get; set; }

        public IDbSet<Author> Authors { get; set; }
    }
}
