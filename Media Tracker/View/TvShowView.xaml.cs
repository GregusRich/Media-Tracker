namespace Media_Tracker.View;
using Media_Tracker.ViewModel;


public partial class TvShowView : ContentPage
{
	public TvShowView(TvShowViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}