﻿using Media_Tracker.View;
using Media_Tracker.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Media_Tracker;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register ViewModels
        builder.Services.AddSingleton<BookViewModel>();
        builder.Services.AddSingleton<MovieViewModel>();
        builder.Services.AddSingleton<TvShowViewModel>();
        builder.Services.AddSingleton<BaseViewModel>();
        builder.Services.AddSingleton<MainPage>();


        // Register Views and use factory methods to inject ViewModels
        builder.Services.AddSingleton(s => new MainPage(s.GetRequiredService<BaseViewModel>()));
        builder.Services.AddSingleton(s => new BookView(s.GetRequiredService<BookViewModel>()));
        builder.Services.AddSingleton(s => new MovieView(s.GetRequiredService<MovieViewModel>()));
        builder.Services.AddSingleton(s => new TvShowView(s.GetRequiredService<TvShowViewModel>()));
        builder.Services.AddSingleton(s => new AddMovieView(s.GetRequiredService<MovieViewModel>()));
        builder.Services.AddSingleton(s => new AddTvShowView(s.GetRequiredService<TvShowViewModel>()));
        builder.Services.AddSingleton(s => new AddBookView(s.GetRequiredService<BookViewModel>()));

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
