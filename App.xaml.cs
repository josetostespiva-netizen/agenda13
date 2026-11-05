namespace agenda13;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        UserAppTheme = AppTheme.Light;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var navigation = new NavigationPage(new Views.ContratacaoHospedagem())
        {
            BarBackgroundColor = Color.FromArgb("#163F46"), BarTextColor = Colors.White
        };
        return new Window(navigation)
        {
            Title = "Agenda 13 · App Hotel", Width = 560, Height = 850,
            MinimumWidth = 360, MinimumHeight = 600
        };
    }
}
