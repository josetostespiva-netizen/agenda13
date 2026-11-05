namespace agenda13.Views;
public partial class SobrePage : ContentPage
{
    public SobrePage() => InitializeComponent();
    private async void Voltar_Clicked(object? sender, EventArgs e)
    {
        if (sender is Button button) button.IsEnabled = false;
        await Navigation.PopAsync();
    }
}
