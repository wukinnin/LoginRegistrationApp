using Microsoft.Maui.Controls;

namespace LoginRegistrationApp
{
    public partial class EditUserPage : ContentPage
    {
        private readonly DatabaseHelper _databaseHelper;
        private User _user;

        public EditUserPage(DatabaseHelper databaseHelper, User user = null)
        {
            InitializeComponent();
            _databaseHelper = databaseHelper;
            _user = user ?? new User();

            UsernameEntry.Text = _user.Username;
            EmailEntry.Text = _user.Email;
            PasswordEntry.Text = _user.Password;
            SaveButton.Text = user == null ? "Add" : "Update";
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            _user.Username = UsernameEntry.Text;
            _user.Email = EmailEntry.Text;
            _user.Password = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(_user.Username) || string.IsNullOrWhiteSpace(_user.Password))
            {
                await DisplayAlert("Error", "Username and password are required.", "OK");
                return;
            }

            if (_user.Id == 0)
            {
                await _databaseHelper.RegisterUser(_user);
            }
            else
            {
                await _databaseHelper.UpdateUser(_user);
            }

            await DisplayAlert("Success", "User saved successfully!", "OK");
            await Navigation.PopAsync();
        }
    }
}