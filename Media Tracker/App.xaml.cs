using Media_Tracker.ViewModel;
using System.Diagnostics;

namespace Media_Tracker;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }
}
