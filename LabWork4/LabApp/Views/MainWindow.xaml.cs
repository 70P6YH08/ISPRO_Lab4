using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isDesc = false;
        private string _sortBy = "Login";
        private int _pageSize = 3;
        private int _currentPage = 1;
        private string _searchField = "";

        public MainWindow()
        {
            InitializeComponent();
            LoadUsersAsync();
        }

        private async void LoadUsersAsync()
            => await GetUsersAsync();

        private async Task GetUsersAsync()
        {
        }

        private async void searchFieldTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void nextPageButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void prevPageButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}