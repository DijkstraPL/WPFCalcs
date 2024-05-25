using Prism.Ioc;
using Prism.Regions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Build_IT_Desktop.Views
{
    /// <summary>
    /// Interaction logic for HeaderView.xaml
    /// </summary>
    public partial class HeaderView : UserControl
    {
        private Window _window;
        private readonly IContainerExtension _container;
        private readonly IRegionManager _regionManager;

        public HeaderView(IContainerExtension container, IRegionManager regionManager)
        {
            InitializeComponent();
            _container = container;
            _regionManager = regionManager;

            this.Loaded += WindowControlsView_Loaded;
        }

        private void WindowControlsView_Loaded(object sender, RoutedEventArgs e)
        {
            _window = Window.GetWindow(this);
        }

        private void Special_DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (_window.WindowState == WindowState.Maximized)
                _window.WindowState = WindowState.Normal;

            _window.DragMove();
        }

        private void SpecialMinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            _window.WindowState = WindowState.Minimized;
        }

        private void AdjustWindowSizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_window.WindowState == WindowState.Maximized)
            {
                _window.WindowState = WindowState.Normal;
                Maximize.Visibility = Visibility.Visible;
                Restore.Visibility = Visibility.Collapsed;
            }
            else
            {
                _window.MaxHeight = SystemParameters.WorkArea.Height + 9;
                _window.WindowState = WindowState.Maximized;

                Maximize.Visibility = Visibility.Collapsed;
                Restore.Visibility = Visibility.Visible;
            }
        }

        private void SpecialCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            _window.Close();
        }
    }
}
