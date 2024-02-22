using Maui.Client.ViewModels;

namespace Maui.Client;

public partial class BookDetailsView : ContentPage
{
	public BookDetailsView(BookDetailsViewModel bookDetailsViewModel)
	{
        BindingContext = bookDetailsViewModel;
        InitializeComponent();
	}
}