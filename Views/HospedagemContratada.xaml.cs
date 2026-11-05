using agenda13.Models;
namespace agenda13.Views;
public partial class HospedagemContratada : ContentPage
{
    public HospedagemContratada(Hospedagem hospedagem)
    {
        InitializeComponent();
        BindingContext = hospedagem;
    }
    private async void Voltar_Clicked(object? sender, EventArgs e)
    {
        if (sender is Button button) button.IsEnabled = false;
        await Navigation.PopAsync();
    }
}
