using Media_Tracker.ViewModel;


namespace Media_Tracker.View;

public partial class MovieView : ContentPage
{
	public MovieView(MovieViewModel viewModel)
	{
        InitializeComponent();
		this.BindingContext = viewModel;
	}
}