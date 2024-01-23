using Media_Tracker.ViewModel;
using Microsoft.Maui.Controls;
using System;

namespace Media_Tracker.View;


public partial class MovieView : ContentPage
{
    private readonly MovieViewModel _viewModel;

    public MovieView(MovieViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        this.BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadMoviesAsync();

        // Call CreateTestMoviesAsync only if there are no movies in AllMovies
        if (_viewModel.AllMovies.Count == 0)
        {
            await _viewModel.CreateTestMoviesAsync();
        }
    }
}