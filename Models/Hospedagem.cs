using System.Globalization;

namespace agenda13.Models;

public sealed class Hospedagem
{
    public static CultureInfo Cultura { get; } = CultureInfo.GetCultureInfo("pt-BR");
    public Quarto Quarto { get; }
    public int Adultos { get; }
    public int Criancas { get; }
    public DateTime CheckIn { get; }
    public DateTime CheckOut { get; }
    public int Noites => (CheckOut - CheckIn).Days;
    public decimal Diaria => Adultos * Quarto.ValorAdulto + Criancas * Quarto.ValorCrianca;
    public decimal Total => Noites * Diaria;
    public string TotalFormatado => Total.ToString("C", Cultura);
    public string DiariaFormatada => Diaria.ToString("C", Cultura);
    public string Periodo => $"{CheckIn:dd/MM/yyyy} → {CheckOut:dd/MM/yyyy}";
    public string Hospedes => $"{Adultos} adulto(s) · {Criancas} criança(s)";
    public string Duracao => $"{Noites} noite(s)";

    public Hospedagem(Quarto? quarto, int adultos, int criancas, DateTime checkIn, DateTime checkOut, DateTime? hoje = null)
    {
        if (quarto is null) throw new ArgumentException("Selecione uma acomodação.");
        if (adultos < 1 || adultos > 10) throw new ArgumentException("Informe de 1 a 10 adultos.");
        if (criancas < 0 || criancas > 10) throw new ArgumentException("Informe de 0 a 10 crianças.");
        var dataAtual = (hoje ?? DateTime.Today).Date;
        if (checkIn.Date < dataAtual) throw new ArgumentException("O check-in não pode estar no passado.");
        if (checkOut.Date <= checkIn.Date) throw new ArgumentException("O check-out deve ser posterior ao check-in.");
        if (checkOut.Date > dataAtual.AddYears(1)) throw new ArgumentException("Escolha uma estadia dentro dos próximos 12 meses.");
        if (quarto.ValorAdulto < 0 || quarto.ValorCrianca < 0) throw new ArgumentException("As tarifas não podem ser negativas.");
        Quarto = quarto; Adultos = adultos; Criancas = criancas;
        CheckIn = checkIn.Date; CheckOut = checkOut.Date;
    }
}
