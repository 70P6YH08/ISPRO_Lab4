using LabLibrary.Models;
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
        public UserDetailsWindow(User user)
        {
            InitializeComponent();

            nameTextBox.Text = user.FirstName;
            surnameTextBox.Text = user.LastName;
            loginTextBox.Text = user.Login;
            emailTextBox.Text = user.Email;
            roleNameTextBox.Text = user.Role.Name;
            phoneTextBox.Text = user.PhoneNumber;
        }
    }
}
