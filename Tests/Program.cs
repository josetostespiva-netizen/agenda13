using agenda13.Models;
var hoje = new DateTime(2026, 11, 5);
int aprovados = 0;
void Check(bool value, string nome) { if (!value) throw new Exception(nome); Console.WriteLine($"OK: {nome}"); aprovados++; }
void Rejeita(Action action, string nome) { try { action(); } catch (ArgumentException) { Check(true,nome); return; } throw new Exception(nome); }
var q = Quarto.Catalogo[0];
var h = new Hospedagem(q, 2, 1, hoje, hoje.AddDays(3), hoje);
Check(h.Noites == 3 && h.Diaria == 300m && h.Total == 900m, "2 adultos + 1 criança, 3 noites = R$ 900,00");
Check(new Hospedagem(q,1,0,hoje,hoje.AddDays(1),hoje).Total == 120m, "uma noite e nenhuma criança");
foreach (var quarto in Quarto.Catalogo)
    Check(new Hospedagem(quarto,10,10,hoje,hoje.AddDays(2),hoje).Total == 20*(quarto.ValorAdulto+quarto.ValorCrianca), quarto.Nome);
Rejeita(() => new Hospedagem(null,1,0,hoje,hoje.AddDays(1),hoje), "quarto obrigatório");
Rejeita(() => new Hospedagem(q,0,1,hoje,hoje.AddDays(1),hoje), "adulto obrigatório");
Rejeita(() => new Hospedagem(q,11,0,hoje,hoje.AddDays(1),hoje), "limite de adultos");
Rejeita(() => new Hospedagem(q,1,-1,hoje,hoje.AddDays(1),hoje), "crianças negativas");
Rejeita(() => new Hospedagem(q,1,11,hoje,hoje.AddDays(1),hoje), "limite de crianças");
Rejeita(() => new Hospedagem(q,1,0,hoje,hoje,hoje), "datas iguais");
Rejeita(() => new Hospedagem(q,1,0,hoje.AddDays(2),hoje,hoje), "datas invertidas");
Rejeita(() => new Hospedagem(q,1,0,hoje.AddDays(-1),hoje,hoje), "entrada passada");
Rejeita(() => new Hospedagem(q,1,0,hoje,hoje.AddYears(2),hoje), "período além de 12 meses");
Check(new Hospedagem(q,1,0,new(2028,2,28),new(2028,3,1),new(2028,2,28)).Noites == 2, "ano bissexto");
Check(h.TotalFormatado.Contains("900,00"), "moeda brasileira");
Console.WriteLine($"{aprovados} verificações aprovadas.");
