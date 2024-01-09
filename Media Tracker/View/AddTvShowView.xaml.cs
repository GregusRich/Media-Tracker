using Media_Tracker.ViewModel;

namespace Media_Tracker.View;

public partial class AddTvShowView : ContentPage
{
	public AddTvShowView(TvShowViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        viewModel.TvShowAdded += OnTvShowAdded;
    }

    private async void OnTvShowAdded(object sender, string tvShowTitle)
    {
        await DisplayAlert("Tv Show Added", $"Tv Show \"{tvShowTitle}\" has been added", "OK");
    }
}