using Avalonia.Controls;
using Avalonia.VisualTree;
using ZdaszToApp.ViewModels;

namespace ZdaszToApp.Views;

public partial class Inf04View : UserControl
{
    public Inf04View()
    {
        InitializeComponent();
        DataContext = new Inf04();
    }

    public Inf04View(int collectionId)
    {
        InitializeComponent();
        DataContext = new Inf04(collectionId);
    }

    private void OnBackClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var window = this.GetVisualRoot() as Window;
        if (window != null)
        {
            var testView = window.FindControl<Inf04View>("Inf04");
            var mainDock = window.FindControl<DockPanel>("Main");
            if (testView != null && mainDock != null)
            {
                testView.IsVisible = false;
                mainDock.IsVisible = true;
            }
        }
    }
}
