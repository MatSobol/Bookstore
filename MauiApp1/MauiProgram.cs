using Microsoft.Extensions.Logging;
using Configuration;
using SharedP.Services.BookService.Token;
using Services.BookServices;
using Maui.Client.ViewModels;
using Maui.Client;
using SharedP.Program.Shared.MessageBox;
using Maui.Client.MessageBox;
using Microsoft.Extensions.Options;
using CommunityToolkit.Maui;
using SharedP.Auth;
using SharedP.Services.AuthService;

namespace Maui.Client
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            ConfigureServices(builder.Services);
            return builder.Build();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = ConfigureAppSettings(services);
            ConfigureAppServices(services, appSettingsSection);
            ConfigureViewModels(services);
            ConfigureViews(services);
            ConfigureHttpClients(services, appSettingsSection);
        }

        private static AppSettings ConfigureAppSettings(IServiceCollection services)
        {
            var appSettingsSection = new AppSettings()
            {
                BaseAPIUrl = "http://booker.eastus.cloudapp.azure.com:5093",
                BaseBookEndpoint = new BaseBookEndpoint()
                {
                    Base_url= "api/Book/",
                    AddBookAsync= "createBook",
                    DeleteBookAsync= "DeleteBook",
                    GetBooksAsync= "ReadBooks",
                    UpdateBookAsync= "UpdateBook",
                },
            };
            services.AddSingleton<IOptions<AppSettings>>(new OptionsWrapper<AppSettings>(appSettingsSection));

            return appSettingsSection;
        }

        private static void ConfigureAppServices(IServiceCollection services, AppSettings appSettings)
        {
            services.AddSingleton<IConnectivity>(Connectivity.Current);
            services.AddSingleton<IGeolocation>(Geolocation.Default);
            services.AddSingleton<IMap>(Map.Default);

            services.AddSingleton<IBookService, BookService>();
            services.AddSingleton<IMessageDialogService, MauiMessageDialogService>();
        }

        private static void ConfigureViewModels(IServiceCollection services)
        {

            services.AddSingleton<BookViewModel>();
            services.AddTransient<BookDetailsViewModel>();
            services.AddTransient<LoginViewModel>();
        }

        private static void ConfigureViews(IServiceCollection services)
        {
            services.AddSingleton<MainPage>();
            services.AddTransient<BookDetailsView>();
            services.AddSingleton<Login>();
        }

        private static void ConfigureHttpClients(IServiceCollection services, AppSettings appSettingsSection)
        {
            var uriBuilder = new UriBuilder(appSettingsSection.BaseAPIUrl)
            {
                Path = appSettingsSection.BaseBookEndpoint.Base_url,
            };
            services.AddHttpClient<IBookService, BookService>(client => client.BaseAddress = uriBuilder.Uri);
            var uriBuilder2 = new UriBuilder(appSettingsSection.BaseAPIUrl)
            {
            };
            services.AddHttpClient<IAuthService, AuthService>(client => client.BaseAddress = uriBuilder2.Uri);
        }
    }
}