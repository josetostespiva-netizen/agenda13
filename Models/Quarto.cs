namespace agenda13.Models;

public sealed record Quarto(string Nome, string Descricao, decimal ValorAdulto, decimal ValorCrianca)
{
    public string Tarifas => $"Adulto: {ValorAdulto.ToString("C", Hospedagem.Cultura)} · Criança: {ValorCrianca.ToString("C", Hospedagem.Cultura)} / noite";
    public static IReadOnlyList<Quarto> Catalogo { get; } = new[]
    {
        new Quarto("Suíte Standard", "Conforto e praticidade para descansar.", 120m, 60m),
        new Quarto("Suíte Executiva", "Mais espaço para uma estadia tranquila.", 180m, 90m),
        new Quarto("Suíte Premium", "Uma experiência especial para sua viagem.", 250m, 125m)
    };
}
