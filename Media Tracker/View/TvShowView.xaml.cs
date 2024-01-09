using Media_Tracker.ViewModel;
using Microsoft.Maui.Controls;
using System;

namespace Media_Tracker.View;


public partial class TvShowView : ContentPage
{
	public TvShowView(TvShowViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}