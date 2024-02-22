using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using SharedP.Services.BookService.Token;
using Maui.Client.ViewModels;
using SharedP.Program.Shared.MessageBox;
using SharedP.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Media;

namespace Maui.Client.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    [QueryProperty(nameof(LocalId), nameof(Id))]
    [QueryProperty(nameof(Author), nameof(Author))]
    [QueryProperty(nameof(Title), nameof(Title))]
    [QueryProperty(nameof(BookViewModel), nameof(BookViewModel))]
    public partial class BookDetailsViewModel : ObservableObject
    {
        private readonly IBookService _bookService;
        private readonly IMessageDialogService _messageDialogService;
        public BookViewModel _bookViewModel;

        public BookDetailsViewModel(IBookService bookService, IMessageDialogService messageDialogService)
        {
            _bookService = bookService;
            _messageDialogService = messageDialogService;
        }
            
        public BookViewModel BookViewModel
        {
            get
            {
                return _bookViewModel;
            }
            set
            {
                _bookViewModel = value;
            }
        }
        private int localId = -1;
        public int LocalId
        {
            get => localId;
            set
            {
                localId = value;
            }
        }

        [ObservableProperty]
        public int id = 1;
        public int _Id
        {
            get => id;
            set
            {
                SetProperty(ref id, value);
            }
        }
        [ObservableProperty]
        public string author;
        public string _Author
        {
            get => author;
            set
            {
                SetProperty(ref author, value);
            }
        }
        [ObservableProperty]
        public string title;
        public string _Title
        {
            get => title;
            set
            {
                SetProperty(ref title, value);
            }
        }

        Book book;

        public async Task DeleteBook()
        {
            var token = _bookViewModel._Token;
            if (token == string.Empty)
            {
                await Shell.Current.GoToAsync("../");
                await Shell.Current.GoToAsync(nameof(Login));
                return;
            }
            var result = await _bookService.DeleteBookAsync(book.Id, token);
            if (result == null)
            {
                _messageDialogService.ShowMessage("There is no connection with api");
                return;
            }
            if (!result.Success)
                _messageDialogService.ShowMessage(result.Message);
            else
            {
                await Shell.Current.GoToAsync("../");
                await _bookViewModel.GetBooks();
            }
        }

        public async Task CreateBook()
        {
            var token = _bookViewModel._Token;
            if (token == string.Empty)
            {
                await Shell.Current.GoToAsync("../");
                await Shell.Current.GoToAsync(nameof(Login));
                return;
            }
            var result = await _bookService.CreateBookAsync(book, token);
            if (result == null)
            {
                _messageDialogService.ShowMessage("There is no connection with api");
                return;
            }
            if (result.Success)
            {
                await Shell.Current.GoToAsync("../", true);
                await _bookViewModel.GetBooks();
            }
            else
                _messageDialogService.ShowMessage(result.Message);
        }

        public async Task UpdateBook()
        {
            var token = _bookViewModel._Token;
            if (token == string.Empty)
            {
                await Shell.Current.GoToAsync("../");
                await Shell.Current.GoToAsync(nameof(Login));
                return;
            }
            if (localId == -1)
            {
                localId = book.Id;
            }
            var result = await _bookService.UpdateBookAsync(book, localId, token);
            if (result == null)
            {
                _messageDialogService.ShowMessage("There is no connection with api");
                return;
            }
            if (!result.Success)
                _messageDialogService.ShowMessage(result.Message);
            else
            {
                await Shell.Current.GoToAsync("../", true);
                await _bookViewModel.GetBooks();
            }
        }


        [RelayCommand]
        public async Task Save()
        {
            int idBook = Id;
            book = new Book()
            {
                Id = idBook,
                Title = this.Title,
                Author = this.Author,
            };
            var bookResult = await _bookService.ReadBooksAsync();
            if (bookResult == null)
            {
                _messageDialogService.ShowMessage("There is no connection with api");
                return;
            }
            bool checker = true;
            foreach(Book item in bookResult.Data)
            {
                if(item.Id == localId || item.Id== idBook)
                {
                    checker = false;
                    UpdateBook();
                }
            }
            if (checker)
            {
                CreateBook();
            }
        }

        [RelayCommand]
        public async Task Delete()
        {
            int idBook = Id;
            book = new Book()
            {
                Id = idBook,
                Title = this.Title,
                Author = this.Author,
            };
            DeleteBook();
        }
    }
}
