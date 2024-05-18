using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    internal class Book
    {
        public Book(int id, string title, string author, int publishYear, int pageCount)
        {
            Id = id;
            Title = title;
            Author = author;
            PublishYear = publishYear;
            PageCount = pageCount;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublishYear { get; set; }
        public int PageCount { get; set; }
    }
}
