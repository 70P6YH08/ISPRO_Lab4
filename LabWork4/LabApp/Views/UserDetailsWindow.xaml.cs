using LabLibrary.Models;
using LabLibrary.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LabApp.Views
{
    /// <summary>
    /// Логика взаимодействия для UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {
        private User _user;
        private readonly UserService _userService;
        public UserDetailsWindow(User user, UserService userService)
        {
            InitializeComponent();

            _user = user;
            _userService = userService;
            SetData();
        }

        private async void SetData()
        {
            nameTextBox.Text = _user.FirstName;
            surnameTextBox.Text = _user.LastName;
            loginTextBox.Text = _user.Login;
            emailTextBox.Text = _user.Email;
            phoneTextBox.Text = _user.PhoneNumber;

            var roles = await _userService.GetRolesAsync();
            var userRole = roles.FirstOrDefault(u => u.Id == _user.RoleId);
            roleNameTextBox.Text = userRole?.Name;
        }

        private async void updateUserButton_Click(object sender, RoutedEventArgs e)
        {
            var roles = await _userService.GetRolesAsync();
            var role = roles.FirstOrDefault(u => u.Name == roleNameTextBox.Text);

            if(role is null)
            {
                MessageBox.Show("Такой роли не существует!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                await _userService.UpdateUserAsync(new User
                {
                    Id = _user.Id,
                    FirstName = nameTextBox.Text,
                    LastName = surnameTextBox.Text,
                    Login = loginTextBox.Text,
                    Email = emailTextBox.Text,
                    PhoneNumber = phoneTextBox.Text,
                    Password = _user.Password,
                    RoleId = role.Id,
                });
                MessageBox.Show("Изменение успешно", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow mainWindow = new(_userService);
                mainWindow.Show();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
