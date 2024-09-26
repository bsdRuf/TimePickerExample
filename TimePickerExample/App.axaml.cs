using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using TimePickerExample.ViewModels;
using TimePickerExample.Views;

namespace TimePickerExample;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new Window1
            {
                DataContext = new Window1()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
