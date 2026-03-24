using Avalonia.Controls;
using Avalonia.Threading;
using ZdaszToApp.ViewModels;
using ZdaszToApp.Services;
using System.Diagnostics;
using System;

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
            
            var authService = AuthService.Instance;
            if (authService.HasSavedCredentials())
            {
                vm.LoginViewModel.Username = authService.SavedUsername;
                vm.LoginViewModel.Password = authService.SavedPassword;
                
                DispatcherTimer.RunOnce(() =>
                {
                    vm.LoginViewModel.LoginCommand.Execute(null);
                }, TimeSpan.FromMilliseconds(100));
            }
            
            vm.LoginViewModel.OnCreateAccountClicked += () =>
            {
                Login.IsVisible = false;
                AddAccount.IsVisible = true;
                Debug.WriteLine("[AppView] Przelaczono na AddAccountView");
            };
            
            vm.LoginViewModel.OnLoginSuccess += () =>
            {
                Main.IsVisible = true;
                Login.IsVisible = false;
                AddAccount.IsVisible = false;
                Debug.WriteLine("[AppView] Zaladowano MenuView (Main)");
            };
            
            vm.AddAccountViewModel.OnLoginSuccess += () =>
            {
                Main.IsVisible = true;
                Login.IsVisible = false;
                AddAccount.IsVisible = false;
                Debug.WriteLine("[AppView] Zaladowano MenuView po rejestracji (Main)");
            };
            
            vm.AddAccountViewModel.OnGoBackToLogin += () =>
            {
                AddAccount.IsVisible = false;
                Login.IsVisible = true;
                Debug.WriteLine("[AppView] Przelaczono na LoginView");
            };
        }
    }
}
