using MaterialDesignThemes.Wpf;
using System.Windows;

namespace BazhkoTarchyla.BookLibrary.UI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            theme.SetBaseTheme(Theme.Dark);
            paletteHelper.SetTheme(theme);
        }
    }
}
