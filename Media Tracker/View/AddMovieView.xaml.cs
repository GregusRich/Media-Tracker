using Media_Tracker.ViewModel;
using Microsoft.Maui.Controls;
using System;
using System.Diagnostics;

namespace Media_Tracker.View
{
    public partial class AddMovieView : ContentPage
    {
        public AddMovieView(MovieViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
