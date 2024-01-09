using Media_Tracker.ViewModel;

namespace Media_Tracker.View;

public partial class AddBookView : ContentPage
{
	public AddBookView(BookViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        // Use after Bookadded variable added.        viewModel.BookAdded += OnBookAdded;
    }

    private async void OnBookAdded(object sender, string bookTitle)
    {
        await DisplayAlert("Book Added", $"Book \"{bookTitle}\" has been added", "OK");
    }

}