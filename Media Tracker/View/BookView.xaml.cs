using Media_Tracker.ViewModel;
using Microsoft.Maui.Controls;
using System;

namespace Media_Tracker.View;

public partial class BookView : ContentPage
{
    private readonly BookViewModel _viewModel;

    public BookView(BookViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        this.BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadBooksAsync();

        // Call CreateTestBooksAsync only if there are no book in AllBooks
        if (_viewModel.AllBooks.Count == 0)
        {
            await _viewModel.CreateTestBooksAsync();
        }
    }
}