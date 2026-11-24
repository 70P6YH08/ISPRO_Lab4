using LabLibrary.Models;
using LabLibrary.Services;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
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
        private int _pageSize = 3;
        private int _currentPage = 1;
        private string _searchField = "";
        private int _pageCount;
        private ObservableCollection<User> _users = new();
        private readonly UserService _userService;
        public MainWindow(UserService userService)
        {
            InitializeComponent();
            _userService = userService;
            LoadUsersAsync();
        }

        private async void LoadUsersAsync()
        {
            var users = await _userService.GetUsersAsync();
            _users = new ObservableCollection<User>(users);
            _pageCount = (int)Math.Ceiling((double)_users.Count / _pageSize);
            SetData();
        }


        private async void searchFieldTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _searchField = searchFieldTextBox.Text;
            SetData();
        }

        private void nextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if(_currentPage < _pageCount)
                _currentPage++;
            SetData();
        }

        private void prevPageButton_Click(object sender, RoutedEventArgs e)
        {
            if(_currentPage > 1)
                _currentPage--;
            SetData();
        }
        private void SetData()
        {
            var users = _users
                .Where(u => u.FirstName.Contains(_searchField)
                    || u.Login.Contains(_searchField)
                    || u.PhoneNumber.Contains(_searchField))
                .Skip((_currentPage - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();

            usersListDataGrid.ItemsSource = users;
        }

        private async void deleteUserMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = usersListDataGrid.SelectedItem as User;
            
            if(selectedUser is null)
            {
                MessageBox.Show("Пользователь не выбран", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                await _userService.RemoveUserAsync(selectedUser.Id);
                MessageBox.Show("Пользователь успешно удалён", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadUsersAsync();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void reviewUserMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = usersListDataGrid.SelectedItem as User;

            if (selectedUser is null)
            {
                MessageBox.Show("Пользователь не выбран", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            UserDetailsWindow userDetailsWindow = new(selectedUser, _userService);
            userDetailsWindow.ShowDialog();
        }
    }
}