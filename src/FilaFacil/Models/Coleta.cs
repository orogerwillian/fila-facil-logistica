namespace FilaFacil.Models;

public enum Prioridade
{
    Baixa = 1,
    Media = 2,
    Alta = 3
}

public enum StatusColeta
{
    Pendente = 1,
    EmRota = 2,
    Coletada = 3,
    Cancelada = 4
}

public class Coleta
{
    public int Id { get; set; }
    public string NomeCliente { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public int QuantidadePacotes { get; set; }
    public Prioridade Prioridade { get; set; }
    public StatusColeta Status { get; set; } = StatusColeta.Pendente;
    public DateTime DataCriacao { get; set; } = DateTime.Now;
}
