using Avalonia.Controls;
using Avalonia.VisualTree;
using ZdaszToApp.ViewModels;

namespace ZdaszToApp.Views;

public partial class Inf02View : UserControl
{
    public Inf02View()
    {
        InitializeComponent();
        DataContext = new Inf02();
    }

    public Inf02View(int collectionId)
    {
        InitializeComponent();
        DataContext = new Inf02(collectionId);
    }

    private void OnBackClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var window = this.GetVisualRoot() as Window;
        if (window != null)
        {
            var testView = window.FindControl<Inf02View>("Test");
            var mainDock = window.FindControl<DockPanel>("Main");
            if (testView != null && mainDock != null)
            {
                testView.IsVisible = false;
                mainDock.IsVisible = true;
            }
        }
    }
}
