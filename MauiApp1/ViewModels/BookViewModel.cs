using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharedP.Program.Shared.MessageBox;
using Maui.Client;
using Maui.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SharedP.Services.BookService.Token;
using SharedP.Books;
using Newtonsoft.Json.Linq;


namespace Maui.Client.ViewModels
{
   
 public partial class BookViewModel : ObservableObject
    {
        private readonly IBookService _bookService;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IConnectivity _connectivity;
        public ObservableCollection<Book> Books { get; set; }

        [ObservableProperty]
        private Book selectedBook;

        [ObservableProperty]
        public string username;

        [ObservableProperty]
        public string loadingText;

        [ObservableProperty]
        public string methodType = "Login";

        public string _Token;

        public BookViewModel(IBookService bookService, IMessageDialogService messageDialogService,
            IConnectivity connectivity)
        {
            _messageDialogService = messageDialogService;
            _bookService = bookService;
            _connectivity = connectivity;
            Books = new ObservableCollection<Book>();
            _Token = string.Empty;
            GetBooks();
        }

        public async Task GetBooks()
        {
            LoadingText = "Loading books...";
            Books.Clear();
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }

            var booksResult = await _bookService.ReadBooksAsync();
            LoadingText = "";
            if (booksResult == null)
            {
                _messageDialogService.ShowMessage("There is no connection with api");
                return;
            }
            if (booksResult.Success)
            {
                foreach (var p in booksResult.Data)
                {
                    Books.Add(p);
                }
            }
            else
            {
                _messageDialogService.ShowMessage(booksResult.Message);
            }
        }

        [RelayCommand]
        public async Task Login()
        {
            if (MethodType == "Login")
            {
                await Shell.Current.GoToAsync(nameof(Login));
            }
            else
            {
                MethodType = "Login";
                _Token = string.Empty;
                Username = null;
            }
        }

        [RelayCommand]
        public async Task ShowDetails(Book book)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }
            SelectedBook = book;
            await Shell.Current.GoToAsync(nameof(BookDetailsView), true, new Dictionary<string, object>
            {
                {"LocalId", SelectedBook.Id },
                {"Id", SelectedBook.Id },
                {"Author", SelectedBook.Author },
                {"Title", SelectedBook.Title },
                {nameof(BookViewModel), this }
            });
        }

        [RelayCommand]
        public async Task New()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }

            SelectedBook = new Book();
            await Shell.Current.GoToAsync(nameof(BookDetailsView), true, new Dictionary<string, object>
            {
                {nameof(BookViewModel), this }
            });
        }

    }
}
