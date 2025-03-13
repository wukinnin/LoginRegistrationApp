using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginRegistrationApp
{
    public partial class UserManagementPage : ContentPage
    {
        private readonly DatabaseHelper _databaseHelper;
        private List<User> _users;

        public UserManagementPage(DatabaseHelper databaseHelper)
        {
            InitializeComponent();
            _databaseHelper = databaseHelper;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _users = await _databaseHelper.GetAllUsers();
            UsersCollectionView.ItemsSource = _users;
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            var user = (sender as Button).BindingContext as User;
            await Navigation.PushAsync(new EditUserPage(_databaseHelper, user));
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var user = (sender as Button).BindingContext as User;
            bool confirm = await DisplayAlert("Confirm", $"Delete {user.Username}?", "Yes", "No");
            if (confirm)
            {
                await _databaseHelper.DeleteUser(user);
                await DisplayAlert("Success", "User deleted.", "OK");
                UsersCollectionView.ItemsSource = await _databaseHelper.GetAllUsers();
            }
        }

        private async void OnAddNewUserClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditUserPage(_databaseHelper));
        }
    }
}