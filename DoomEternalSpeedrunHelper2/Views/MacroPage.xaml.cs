using System;
using Windows.System;
using CommunityToolkit.Mvvm.DependencyInjection;

using DoomEternalSpeedrunHelper2.ViewModels;
using Microsoft.UI.Xaml.Controls;
using RoutedEventArgs = Microsoft.UI.Xaml.RoutedEventArgs;
using KeyRoutedEventArgs = Microsoft.UI.Xaml.Input.KeyRoutedEventArgs;
using Helper.Views.Utils;
using static Helper.Views.Utils.MacroRunner;

namespace DoomEternalSpeedrunHelper2.Views {
    public sealed partial class MacroPage : Page {

        private static readonly string MACRO_ENABLED_KEY = "macroEnabled";
        private static readonly string SCROLL_DIRECTION_KEY = "scrollDirection";
        private static readonly string KEYBIND_KEY = "keybind";

        private Windows.Storage.ApplicationDataContainer _localSettings =
            Windows.Storage.ApplicationData.Current.LocalSettings;

        public MacroViewModel ViewModel { get; }

        public MacroPage() {
            ViewModel = Ioc.Default.GetService<MacroViewModel>();
            InitializeComponent();
            InitializePage();
        }

        private void InitializePage() {
            if ((bool)_localSettings.Values[MACRO_ENABLED_KEY] == true) {
                isEnabledButton.Content = "Enabled";
                isEnabledButton.IsChecked = true;
                MacroRunner.EnableMacro();
            }
            if (_localSettings.Values.ContainsKey(KEYBIND_KEY)) {
                string keybind = (string)_localSettings.Values[KEYBIND_KEY];
                VirtualKey virtualKey;
                if (Enum.TryParse(keybind, out virtualKey)) {
                    macroKeybind.Text = keybind;
                    MacroRunner.SetKeybind(virtualKey);
                }
            }
            if (_localSettings.Values.ContainsKey(SCROLL_DIRECTION_KEY)) {
                ScrollDirection savedScrollDirection;
                if (Enum.TryParse((string)_localSettings.Values[SCROLL_DIRECTION_KEY], out savedScrollDirection)) {
                    if (savedScrollDirection == ScrollDirection.SCROLL_UP) {
                        scrollUp.IsChecked = true;
                    } else if (savedScrollDirection == ScrollDirection.SCROLL_DOWN) {
                        scrollDown.IsChecked = true;
                    }
                    MacroRunner.SetScrollDirection(savedScrollDirection);
                }
            }
        }

        private void IsEnabledButton_OnChecked(object sender, RoutedEventArgs routedEventArgs) {
            _localSettings.Values[MACRO_ENABLED_KEY] = true;
            isEnabledButton.Content = "Enabled";
            MacroRunner.EnableMacro();
        }

        private void IsEnabledButton_OnUnchecked(object sender, RoutedEventArgs routedEventArgs) {
            _localSettings.Values[MACRO_ENABLED_KEY] = false;
            isEnabledButton.Content = "Disabled";
            MacroRunner.DisableMacro();
        }

        private void MacroKeybind_OnGotFocus(object sender, RoutedEventArgs routedEventArgs) {
            macroKeybind.Text = "";
        }

        private void MacroKeybind_OnPreviewKeyDown(object sender, KeyRoutedEventArgs e) {
            if (e.Key == VirtualKey.Escape) {
                macroKeybind.Text = MacroRunner.GetKeybind().ToString();
            } else {
                macroKeybind.Text = e.Key.ToString();
            }
            isEnabledButton.RemoveFocusEngagement();
            _localSettings.Values[KEYBIND_KEY] = macroKeybind.Text;
            MacroRunner.SetKeybind(e.Key);
            e.Handled = true;
        }

        private void ScrollUp_OnChecked(object sender, RoutedEventArgs routedEventArgs) {
            MacroRunner.SetScrollDirection(ScrollDirection.SCROLL_UP);
            _localSettings.Values[SCROLL_DIRECTION_KEY] = ScrollDirection.SCROLL_UP.ToString();
        }

        private void ScrollDown_OnChecked(object sender, RoutedEventArgs routedEventArgs) {
            MacroRunner.SetScrollDirection(ScrollDirection.SCROLL_DOWN);
            _localSettings.Values[SCROLL_DIRECTION_KEY] = ScrollDirection.SCROLL_DOWN.ToString();
        }
    }
}
