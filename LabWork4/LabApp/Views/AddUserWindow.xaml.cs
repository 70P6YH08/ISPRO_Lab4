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
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        private readonly UserService _userService;
        public AddUserWindow(UserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }

        private async void addUserButton_Click(object sender, RoutedEventArgs e)
        {
            var roles = await _userService.GetRolesAsync();
            var role = roles.FirstOrDefault(u => u.Name == roleNameTextBox.Text);

            if (role is null)
            {
                MessageBox.Show("Такой роли не существует!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if(String.IsNullOrWhiteSpace(nameTextBox.Text)
                || String.IsNullOrWhiteSpace(surnameTextBox.Text)
                || String.IsNullOrWhiteSpace(loginTextBox.Text)
                || String.IsNullOrWhiteSpace(emailTextBox.Text)
                || String.IsNullOrWhiteSpace(phoneTextBox.Text)
                || String.IsNullOrWhiteSpace(passPasswordBox.Password))
            {
                MessageBox.Show("Не все поля заполнены!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                await _userService.AddUserAsync(new User
                {
                    FirstName = nameTextBox.Text,
                    LastName = surnameTextBox.Text,
                    Login = loginTextBox.Text,
                    Email = emailTextBox.Text,
                    PhoneNumber = phoneTextBox.Text,
                    Password = passPasswordBox.Password,
                    RoleId = role.Id,
                });
                MessageBox.Show("Добавление успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow mainWindow = new(_userService);
                mainWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
