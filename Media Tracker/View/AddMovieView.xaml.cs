using Media_Tracker.ViewModel;

namespace Media_Tracker.View;

    public partial class AddMovieView : ContentPage
    {
        public AddMovieView(MovieViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.MovieAdded += OnMovieAdded;
        }

        private async void OnMovieAdded(object sender, string movieTitle)
        {
            await DisplayAlert("Movie Added", $"Movie \"{movieTitle}\" has been added", "OK");
        }
    }
