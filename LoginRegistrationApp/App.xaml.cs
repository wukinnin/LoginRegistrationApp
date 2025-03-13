using Microsoft.Maui.Controls;

namespace LoginRegistrationApp
{
    public partial class App : Application
    {
        public App(DatabaseHelper databaseHelper)
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage(databaseHelper));
        }
    }
}