using CommunityToolkit.Mvvm.DependencyInjection;

using DoomEternalSpeedrunHelper2.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace DoomEternalSpeedrunHelper2.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; }

        public MainPage()
        {
            ViewModel = Ioc.Default.GetService<MainViewModel>();
            InitializeComponent();
        }
    }
}
