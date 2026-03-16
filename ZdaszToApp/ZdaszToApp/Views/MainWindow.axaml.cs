using Avalonia.Controls;
using ZdaszToApp.ViewModels;
namespace ZdaszToApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        DataContext = new MainWindowViewModel();
        
        if (DataContext is MainWindowViewModel vm)
        {
            Login.DataContext = vm.LoginViewModel;
            AddAccount.DataContext = vm.AddAccountViewModel;
            
            vm.LoginViewModel.OnCreateAccountClicked += () =>
            {
                Login.IsVisible = false;
                AddAccount.IsVisible = true;
            };
            
            vm.LoginViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(LoginViewModel.IsLoggedIn) && vm.LoginViewModel.IsLoggedIn)
                {
                    Main.IsVisible = true;
                    Login.IsVisible = false;
                    AddAccount.IsVisible = false;
                }
            };
        }
    }
}
