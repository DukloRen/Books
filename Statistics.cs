using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WpfApp1
{
    internal class Statistics
    {
        public static List<Book> LoadBooks()
        {
            using var conn = new MySqlConnection("Server=localhost;Database=vizsga;Uid=root;Pwd=;");
            conn.Open();
            using var comm = conn.CreateCommand();
            comm.CommandText = "SELECT id, title, author, publish_year, page_count FROM books";
            using var reader = comm.ExecuteReader();

            var list = new List<Book>();
            while (reader.Read())
            {
                var book = new Book(
                    reader.GetInt32("id"),
                    reader.GetString("title"),
                    reader.GetString("author"),
                    reader.GetInt32("publish_year"),
                    reader.GetInt32("page_count")
                );
                list.Add(book);
            }
            return list;
        }

        public int BooksLongerThan500Pages()
        {
            int counter = 0;
            foreach (var item in LoadBooks())
            {
                if (item.PageCount>500)
                {
                    counter++;
                }
            }
            return counter;
        }
        public bool IsThereABookThatIsOlderThan1950()
        {
            bool result = false;
            foreach(var item in LoadBooks())
            {
                if (item.PublishYear<1950)
                {
                    result = true;
                }
            }
            return result;
        }
        public void LongestBookData()
        {
            int max = int.MinValue;
            int index = 0;
            foreach (var item in LoadBooks())
            {
                if (item.PageCount > max)
                {
                    max = item.PageCount;
                    index = item.Id;
                }
            }

            //Ez azért van, mert a listában az index 0-tól indul, de az adatbázisban az id 1-től
            index -= 1;

            Console.WriteLine("A leghosszabb könyv:");
            Console.WriteLine($"Szerző: {LoadBooks()[index].Author}\nCím: {LoadBooks()[index].Title}\nKiadás éve: {LoadBooks()[index].PublishYear}" +
                $"\nOldalszám: {LoadBooks()[index].PageCount}");
        }

        public void AuthorWithTheMostBooksWritten()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach (var item in LoadBooks())
            {
                if (dictionary.ContainsKey(item.Author))
                {
                    dictionary[item.Author]++;
                }
                else
                {
                    dictionary.Add(item.Author, 1);
                }
            }

            int max = int.MinValue;
            string author = "";
            foreach(var item in dictionary)
            {
                if (item.Value>max)
                {
                    max = item.Value;
                    author = item.Key;
                }
            }
            //Több is van
            Console.WriteLine($"A legtöbb könyvvel rendelkező szerző: {author}");
        }

        public void AuthorOfAGivenBook(string title)
        {
            string author = "";
            foreach(var item in LoadBooks())
            {
                if (item.Title==title)
                {
                    author = item.Author;
                }
            }

            if (author!="")
            {
                Console.WriteLine($"A megadott könyv szerzője: {author}");
            }
            else
            {
                Console.WriteLine("Nincs ilyen könyv");
            }
        }

        public bool DeleteBook(int id)
        {
            using var conn = new MySqlConnection("Server=localhost;Database=vizsga;Uid=root;Pwd=;");
            conn.Open();
            using var comm = conn.CreateCommand();
            comm.CommandText = "DELETE FROM books WHERE id = @id";
            comm.Parameters.AddWithValue("@id", id);

            int affectedRows = comm.ExecuteNonQuery();
            conn.Close();

            return affectedRows==1;
        }
    }
}
