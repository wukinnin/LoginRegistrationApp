using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using SQLite;
using Microsoft.Maui.Controls;

namespace LoginRegistrationApp
{
    public partial class RegisterPage : ContentPage
    {
        private readonly DatabaseHelper _databaseHelper;

        public RegisterPage(DatabaseHelper databaseHelper)
        {
            InitializeComponent();
            _databaseHelper = databaseHelper;
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var email = EmailEntry.Text;
            var password = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Please fill all fields.", "OK");
                return;
            }

            var existingUser = await _databaseHelper.GetUserByUsername(username);
            if (existingUser != null)
            {
                await DisplayAlert("Error", "Username already exists.", "OK");
                return;
            }

            var user = new User
            {
                Username = username,
                Email = email,
                Password = password
            };

            await _databaseHelper.RegisterUser(user);
            await DisplayAlert("Success", "User registered successfully!", "OK");
            await Navigation.PopAsync();
        }

        private async void OnBackToLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}