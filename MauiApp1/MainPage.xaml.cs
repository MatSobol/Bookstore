using Maui.Client.ViewModels;
using Microsoft.Maui.Controls.StyleSheets;

namespace Maui.Client
{
    public partial class MainPage : ContentPage
    {
       
        public MainPage(BookViewModel bookViewModel)
        {
            
            InitializeComponent();
            BindingContext = bookViewModel;
        }

       
    }
}