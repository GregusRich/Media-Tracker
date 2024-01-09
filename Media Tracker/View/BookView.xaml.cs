using Media_Tracker.ViewModel;
using Microsoft.Maui.Controls;
using System;

namespace Media_Tracker.View;

public partial class BookView : ContentPage
{
	public BookView(BookViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}