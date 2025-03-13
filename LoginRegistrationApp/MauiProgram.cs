using LoginRegistrationApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using System.IO;

namespace LoginRegistrationApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Register DatabaseHelper
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "users.db3");
            builder.Services.AddSingleton(new DatabaseHelper(dbPath));

            return builder.Build();
        }
    }
}