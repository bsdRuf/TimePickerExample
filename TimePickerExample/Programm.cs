using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using System;
using System.Diagnostics;


namespace TimePickerExample
{
    internal class Programm
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args, ShutdownMode.OnExplicitShutdown);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .LogToTrace()
                .UseReactiveUI();
    }
}
