using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Provider;

namespace WpfApp1
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            if (args.Contains("--stat"))
            {
                try
                {
                    var test = Statistics.LoadBooks();
                }
                catch (Exception errormsg)
                {
                    Console.WriteLine("Nem sikerült kapcsolódni az adatbázishoz. Hiba: " + errormsg);
                    Environment.Exit(1);
                }

                Statistics stat = new Statistics();

                //Feladat01
                Console.WriteLine("500 oldalnál hosszabb könyvek száma: " + stat.BooksLongerThan500Pages());

                //Feladat02
                if (stat.IsThereABookThatIsOlderThan1950()==true)
                {
                    Console.WriteLine("Van 1950-nél régebbi könyv");
                }
                else
                {
                    Console.WriteLine("Nincs 1950-nél régebbi könyv");
                }

                //Feladat03
                stat.LongestBookData();

                //Feladat04
                stat.AuthorWithTheMostBooksWritten();

                //Feladat05
                Console.WriteLine("Adjon meg egy könyv címet: ");
                string title = Console.ReadLine().ToString();
                stat.AuthorOfAGivenBook(title);

                Console.ReadKey();
            }
            else
            {
                new Application().Run(new MainWindow());
            }
        }
    }
}
