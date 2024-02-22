using Maui.Client.ViewModels;
using SharedP.Program.Shared.MessageBox;
using System.Text.RegularExpressions;

namespace Maui.Client;

public partial class Login : ContentPage
{
    private readonly IMessageDialogService _messageDialogService;
    private LoginViewModel _viewModel;
    public Login(LoginViewModel loginViewModel, IMessageDialogService messageDialogService)
	{
        _messageDialogService = messageDialogService;
        _viewModel = loginViewModel;
        loginViewModel.Visibility = false;
        BindingContext = loginViewModel;
        InitializeComponent();
#if WINDOWS
        Google.IsVisible = true;
        Microsoft.IsVisible = true;
        Facebook.IsVisible = true;
#endif
    }

    private async void jsExampleWebView_Navigated(object sender, WebNavigatedEventArgs e)
    {
        string input = await jsExampleWebView.EvaluateJavaScriptAsync($"document.documentElement.innerHTML");
        string pattern = @"\\u003Ch5>(.*?)\\u003C/h5>";
        Match match = Regex.Match(input, pattern);
        if (match.Success)
        {
            string extractedValue = match.Groups[1].Value;
            _viewModel.Token = extractedValue;
            _viewModel.Visibility = false;
            _viewModel.Url = "";
            _viewModel._bookViewModel.MethodType = "Logout";
            await Shell.Current.GoToAsync("../", true);
            _messageDialogService.ShowMessage("Login succesful");
            return;
        }
    }
}