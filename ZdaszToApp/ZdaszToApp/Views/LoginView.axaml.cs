using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using ZdaszToApp.ViewModels;

namespace ZdaszToApp.Views;

public partial class LoginView : UserControl
{
    private DispatcherTimer? _spinnerTimer;
    private double _rotationAngle;

    public LoginView()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        if (DataContext is LoginViewModel vm)
        {
            vm.PropertyChanged += (s, args) =>
            {
                if (args.PropertyName == nameof(LoginViewModel.IsLoading))
                {
                    if (vm.IsLoading)
                        StartSpinner();
                    else
                        StopSpinner();
                }
            };
        }
    }

    private void StartSpinner()
    {
        _rotationAngle = 0;
        _spinnerTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(30)
        };
        _spinnerTimer.Tick += (s, e) =>
        {
            _rotationAngle = (_rotationAngle + 10) % 360;
            if (Spinner?.RenderTransform is RotateTransform rt)
            {
                rt.Angle = _rotationAngle;
            }
        };
        _spinnerTimer.Start();
    }

    private void StopSpinner()
    {
        _spinnerTimer?.Stop();
        _spinnerTimer = null;
    }
}
