using Media_Tracker.ViewModel;
using Microsoft.Maui.Controls;
using System;


namespace Media_Tracker.View;

public partial class MainPage : ContentPage
{
    public MainPage(BaseViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }

    private async void OnViewMediaClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MovieView");
    }
}