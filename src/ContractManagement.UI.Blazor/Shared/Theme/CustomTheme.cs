using MudBlazor;

namespace ContractManagement.UI.Blazor.Shared.Theme
{
    public static class CustomTheme
    {
        public static MudTheme DefaultTheme = new()
        {
            PaletteLight = new PaletteLight()
            {
                Primary = Colors.Blue.Darken2,
                Secondary = Colors.DeepPurple.Accent2,
                Background = Colors.Gray.Lighten5,
                Surface = Colors.Shades.White,
                AppbarBackground = Colors.Blue.Darken3,
                DrawerBackground = Colors.BlueGray.Lighten5,
                Success = Colors.Green.Accent3,
                Error = Colors.Red.Accent2,                
            },           
        };
    }
}
