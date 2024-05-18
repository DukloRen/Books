using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadDataGrid();
        }

        public void LoadDataGrid()
        {
            try
            {
                dtGrid.ItemsSource = Statistics.LoadBooks();
            }
            catch (Exception errormsg)
            {
                MessageBox.Show(errormsg.Message);
                Environment.Exit(1);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Statistics stat = new Statistics();
            var selectedBook = dtGrid.SelectedItem as Book;
            if (selectedBook == null)
            {
                MessageBox.Show("Törléshez előbb válasszon ki könyvet");
            }
            else
            {
                MessageBoxResult choice = MessageBox.Show("Biztos szeretné törölni a kiválasztott könyvet?", "Könyv törlése", MessageBoxButton.YesNo);
                if (choice == MessageBoxResult.Yes)
                {
                    if (stat.DeleteBook(selectedBook.Id))
                    {
                        MessageBox.Show("Sikeres törlés!");
                    }
                    else
                    {
                        MessageBox.Show("Sikertelen törlés!");
                    }
                }
                LoadDataGrid();
            }
        }
    }
}
