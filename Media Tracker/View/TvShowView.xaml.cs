using Media_Tracker.ViewModel;
using Microsoft.Maui.Controls;
using System;

namespace Media_Tracker.View;

public partial class TvShowView : ContentPage
{
    private readonly TvShowViewModel _viewModel;

    public TvShowView(TvShowViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        this.BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadTvShowsAsync();

        // Call CreateTestTvShowsAsync only if there are no TV shows in AllTvShows
        if (_viewModel.AllTvShows.Count == 0)
        {
            await _viewModel.CreateTestTvShowsAsync();
        }
    }
}
