using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharedP.Auth;
using SharedP.Program.Shared.MessageBox;
using SharedP.Services.AuthService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Maui.Client.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {

        private readonly IConnectivity _connectivity;
        private readonly IAuthService _authService;
        private readonly IMessageDialogService _messageDialogService;
        public BookViewModel _bookViewModel;
        public LoginViewModel(IConnectivity connectivity, IAuthService authService, IMessageDialogService messageDialogService, BookViewModel bookViewModel)
        {
            _bookViewModel = bookViewModel;
            _messageDialogService = messageDialogService;
            UserLoginDTO = new UserLoginDTO();
            _authService = authService;
            _connectivity = connectivity;
            visibility = false;
        }
        public string Token
        {
            get { return _bookViewModel._Token; }
            set
            {
                if (value != string.Empty)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var jsonToken = tokenHandler.ReadToken(value) as JwtSecurityToken;
                    if (jsonToken != null)
                    {
                        foreach (var claim in jsonToken.Claims)
                        {
                            if (claim.Type == ClaimTypes.Name)
                            {
                                _bookViewModel.Username = claim.Value;
                            }
                        }
                    }
                }
                _bookViewModel._Token = value;
            }
        }

        [ObservableProperty]
        private UserLoginDTO userLoginDTO;

        [ObservableProperty]
        private string url;

        [ObservableProperty]
        private bool visibility = false;

        [RelayCommand]
        public async void Login()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }
            var response = await _authService.Login(UserLoginDTO);
            if (response.Success)
            {
                _bookViewModel.MethodType = "Logout";
                Token = response.Data;
                await Shell.Current.GoToAsync("../", true);
                _messageDialogService.ShowMessage(response.Message);
                return;
            }
            else
            {
                _messageDialogService.ShowMessage(response.Message);
                return;
            }

        }

        [RelayCommand]
        public void LoginGoogle()
        {
            Url = "http://localhost:5093/api/Auth/loginGoogle?requestId=2";
            Visibility = true;
            return;
        }
        [RelayCommand]
        public void LoginMicrosoft()
        {
            Url = "http://localhost:5093/api/Auth/loginMicrosoft?requestId=2";
            Visibility = true;
            return;
        }
        [RelayCommand]
        public void LoginFacebook()
        {
            Url = "http://localhost:5093/api/Auth/loginFacebook?requestId=2";
            Visibility = true;
            return;
        }
    }
}
