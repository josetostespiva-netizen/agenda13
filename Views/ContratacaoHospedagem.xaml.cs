using agenda13.Models;
namespace agenda13.Views;

public partial class ContratacaoHospedagem : ContentPage
{
    private bool navegando;
    public ContratacaoHospedagem()
    {
        InitializeComponent();
        dtpck_checkin.MinimumDate = DateTime.Today;
        dtpck_checkin.MaximumDate = DateTime.Today.AddYears(1).AddDays(-1);
        dtpck_checkout.MaximumDate = DateTime.Today.AddYears(1);
        dtpck_checkout.MinimumDate = DateTime.Today.AddDays(1);
        dtpck_checkin.Date = DateTime.Today;
        dtpck_checkout.Date = DateTime.Today.AddDays(1);
        pck_quarto.ItemsSource = Quarto.Catalogo.ToList();
        pck_quarto.SelectedIndex = 0;
    }

    private void Quarto_Changed(object? sender, EventArgs e)
    {
        if (pck_quarto.SelectedItem is not Quarto quarto) return;
        lbl_descricao.Text = quarto.Descricao;
        lbl_tarifas.Text = quarto.Tarifas;
    }

    private void CheckIn_Changed(object? sender, DateChangedEventArgs e)
    {
        if (dtpck_checkout is null || dtpck_checkin.Date is not DateTime entrada) return;
        dtpck_checkout.MinimumDate = entrada.AddDays(1);
        if (dtpck_checkout.Date is not DateTime saida || saida <= entrada)
            dtpck_checkout.Date = entrada.AddDays(1);
    }

    private async void Avancar_Clicked(object? sender, EventArgs e)
    {
        if (navegando) return;
        navegando = true;
        try
        {
            if (dtpck_checkin.Date is not DateTime entrada || dtpck_checkout.Date is not DateTime saida)
                throw new ArgumentException("Informe as datas de entrada e saída.");
            var hospedagem = new Hospedagem(pck_quarto.SelectedItem as Quarto,
                (int)stp_adultos.Value, (int)stp_criancas.Value, entrada, saida);
            await Navigation.PushAsync(new HospedagemContratada(hospedagem));
        }
        catch (ArgumentException ex) { await DisplayAlertAsync("Confira sua estadia", ex.Message, "Entendi"); }
        finally { navegando = false; }
    }

    private async void Sobre_Clicked(object? sender, EventArgs e)
    {
        if (navegando) return;
        navegando = true;
        try { await Navigation.PushAsync(new SobrePage()); }
        finally { navegando = false; }
    }
}
