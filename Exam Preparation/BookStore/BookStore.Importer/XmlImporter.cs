namespace BookStore.Importer
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Xml.Linq;

    using BookStore.Data;
    using BookStore.Models;

    class XmlImporter
    {
        private static BookStoreDbContex db;

        static void Main(string[] args)
        {
            string pathToXml = @"..\..\..\complex-books.xml";

            //ImportFromXml(pathToXml);

            string pathToXmlQuery = @"..\..\..\reviews-queries.xml";
            Search(pathToXmlQuery);
        }

        public static void Search(string path)
        {
            db = new BookStoreDbContex();

            var result = new XElement("search-results");
            var xmlQueries = XElement.Load(path).Elements();

            foreach (var xmlQuery in xmlQueries)
            {
                var queryInReviews = db.Reviews.AsQueryable();

                if (xmlQuery.Attribute("type").Value == "by-period")
                {
                    var startDate = DateTime.Parse(xmlQuery.Element("start-date").Value);
                    var endDate = DateTime.Parse(xmlQuery.Element("end-date").Value);

                    queryInReviews = queryInReviews.Where(r => r.CreatedOn >= startDate && r.CreatedOn <= endDate);
                }

                if (xmlQuery.Attribute("type").Value == "by-author")
                {
                    var authorName = xmlQuery.Element("author-name").Value;

                    queryInReviews = queryInReviews.Where(r => r.Author.Name == authorName);
                }

                var xmlResultSet = queryInReviews.OrderBy(r => r.CreatedOn).ThenBy(r => r.Content)
                    .Select(r => new
                    {
                        Date = r.CreatedOn,
                        Content = r.Content,
                        Book = new
                        {
                            Title = r.Book.Title,
                            Authors = r.Book.Authors
                                .OrderBy(a => a.Name)
                                .Select(a => a.Name),
                            ISBN = r.Book.ISBN,
                            URL = r.Book.WebSite
                        }
                    }).ToList();

                var xmlResultSets = new XElement("results-set");
                foreach (var reviewInResult in xmlResultSet)
                {
                    var xmlReview = new XElement("review");
                    xmlReview.Add(new XElement("date", reviewInResult.Date.ToString("d-MMM-yyyy")));
                    xmlReview.Add(new XElement("content", reviewInResult.Content));
                    var xmlBookTitle = new XElement("book", reviewInResult.Book.Title);
                    var xmlBookAuthors = new XElement("authors", string.Join(", ", reviewInResult.Book.Authors));
                    var xmlBookIsbn = new XElement("isbn", reviewInResult.Book.ISBN);
                    var xmlBookUrl = new XElement("url", reviewInResult.Book.URL);
                    var xmlBook = new XElement("book");
                    xmlBook.Add(xmlBookTitle);
                    if (xmlBookAuthors != null)
                    {
                        xmlBook.Add(xmlBookAuthors);
                    }
                    if (xmlBookIsbn != null)
                    {
                        xmlBook.Add(xmlBookIsbn);
                    }
                    if (xmlBookUrl != null)
                    {
                        xmlBook.Add(xmlBookUrl);
                    }
                    xmlReview.Add(xmlBook);
                    xmlResultSets.Add(xmlReview);
                }

                result.Add(xmlResultSets);
            }

            result.Save(@"..\..\..\reviews-search-results.xml");
        }

        private static void ImportFromXml(string path)
        {
            db = new BookStoreDbContex();

            var xmlBooks = XElement.Load(path).Elements();

            foreach (var book in xmlBooks)
            {
                var currentBook = new Book();
                currentBook.Title = book.Element("title").Value;

                var isbn = book.Element("isbn");
                if (isbn != null)
                {
                    var bookExists = db.Books.Any(b => b.ISBN == isbn.Value);
                    if (bookExists)
                    {
                        throw new ArgumentException("ISBN exists");
                    }
                    currentBook.ISBN = isbn.Value;
                }
                var price = book.Element("price");
                if (price != null)
                {
                    currentBook.Price = decimal.Parse(price.Value);
                }
                var webSite = book.Element("web-site");
                if (webSite != null)
                {
                    currentBook.WebSite = webSite.Value;
                }

                var xmlAuthors = book.Element("authors");
                if (xmlAuthors != null)
                {
                    foreach (var xmlAuthor in xmlAuthors.Elements("author"))
                    {
                        var authorName = xmlAuthor.Value;

                        currentBook.Authors.Add(GetAuthor(authorName));
                    }
                }

                var xmlReviews = book.Element("reviews");
                if (xmlReviews != null)
                {
                    foreach (var xmlReview in xmlReviews.Elements("review"))
                    {
                        var reviewDate = xmlReview.Attribute("date");
                        var authorName = xmlReview.Attribute("author");

                        var review = new Review
                        {
                            Content = xmlReview.Value,
                            CreatedOn = reviewDate == null ? DateTime.Now : DateTime.Parse(reviewDate.Value)
                        };

                        if (authorName != null)
                        {
                            review.Author = GetAuthor(authorName.Value);
                        }

                        currentBook.Reviews.Add(review);
                    }
                }

                db.Books.Add(currentBook);
                db.SaveChanges();
            }
        }

        public static Author GetAuthor(string authorName)
        {
            var author = db.Authors.FirstOrDefault(a => a.Name == authorName);
            if (author == null)
            {
                author = new Author
                {
                    Name = authorName
                };
            }

            return author;
        }
    }
}
