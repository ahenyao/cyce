using Avalonia.Controls;
using ZdaszToApp.ViewModels;
using System.Diagnostics;

namespace ZdaszToApp.Views;

public partial class AppView : UserControl
{
    public AppView()
    {
        InitializeComponent();
        Debug.WriteLine("[AppView] Zaladowano AppView (telefon)");
        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(object? sender, System.EventArgs e)
    {
        Debug.WriteLine("[AppView] DataContext ustawiony");
        if (DataContext is MainWindowViewModel vm)
        {
            Login.DataContext = vm.LoginViewModel;
            AddAccount.DataContext = vm.AddAccountViewModel;
            
            vm.LoginViewModel.OnCreateAccountClicked += () =>
            {
                Login.IsVisible = false;
                AddAccount.IsVisible = true;
                Debug.WriteLine("[AppView] Przelaczono na AddAccountView");
            };
            
            vm.LoginViewModel.PropertyChanged += (s, args) =>
            {
                if (args.PropertyName == nameof(LoginViewModel.IsLoggedIn) && vm.LoginViewModel.IsLoggedIn)
                {
                    Main.IsVisible = true;
                    Login.IsVisible = false;
                    AddAccount.IsVisible = false;
                    Debug.WriteLine("[AppView] Zaladowano MenuView (Main)");
                }
            };
        }
    }
}
