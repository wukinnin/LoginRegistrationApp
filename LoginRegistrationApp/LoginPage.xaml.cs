using Microsoft.Maui.Controls;

namespace LoginRegistrationApp
{
    public partial class LoginPage : ContentPage
    {
        private readonly DatabaseHelper _databaseHelper;

        public LoginPage(DatabaseHelper databaseHelper)
        {
            InitializeComponent();
            _databaseHelper = databaseHelper;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Please fill all fields.", "OK");
                return;
            }

            var user = await _databaseHelper.GetUserByUsernameAndPassword(username, password);
            if (user == null)
            {
                await DisplayAlert("Error", "Invalid username or password.", "OK");
                return;
            }

            await DisplayAlert("Success", "Login successful!", "OK");
            await Navigation.PushAsync(new UserManagementPage(_databaseHelper));
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage(_databaseHelper));
        }
    }
}