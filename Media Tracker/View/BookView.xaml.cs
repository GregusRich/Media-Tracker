using Media_Tracker.ViewModel;

namespace Media_Tracker.View;

public partial class BookView : ContentPage
{
	public BookView(BookViewModel viewMode)
	{
		InitializeComponent();
		this.BindingContext = viewMode;
	}
}