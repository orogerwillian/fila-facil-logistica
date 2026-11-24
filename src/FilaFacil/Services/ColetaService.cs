using FilaFacil.Models;
using FilaFacil.Repositories;

namespace FilaFacil.Services;

/// <summary>
/// Regras de negócio e validações.
/// Regra do projeto: NÃO usa AppDbContext nem DbSet aqui, só o repository.
/// </summary>
public class ColetaService
{
    private readonly ColetaRepository _repository;

    public ColetaService(ColetaRepository repository)
    {
        _repository = repository;
    }

    // ===================== FASE 2 =====================

    /// <summary>
    /// Valida os dados, monta a entidade e manda o repository gravar.
    /// Em caso de dado inválido, lança ArgumentException com uma mensagem amigável
    /// (o controller captura e mostra para o usuário).
    /// </summary>
    public Coleta Cadastrar(string nomeCliente, string endereco, int quantidadePacotes, Prioridade prioridade)
    {
        // Validações (critério do PDF: "0 campos obrigatórios aceitos vazios").
        if (string.IsNullOrWhiteSpace(nomeCliente))
            throw new ArgumentException("O nome do cliente é obrigatório.");

        if (string.IsNullOrWhiteSpace(endereco))
            throw new ArgumentException("O endereço é obrigatório.");

        if (quantidadePacotes <= 0)
            throw new ArgumentException("A quantidade de pacotes deve ser maior que zero.");

        if (!Enum.IsDefined(prioridade))
            throw new ArgumentException("Prioridade inválida.");

        var coleta = new Coleta
        {
            NomeCliente = nomeCliente.Trim(),
            Endereco = endereco.Trim(),
            QuantidadePacotes = quantidadePacotes,
            Prioridade = prioridade,
            Status = StatusColeta.Pendente,  // toda coleta nova começa pendente
            DataCriacao = DateTime.Now
        };

        return _repository.Adicionar(coleta);
    }

    public List<Coleta> ListarTodas() => _repository.ListarTodas();

    // ===================== FASE 3 (próxima etapa) =====================
    // FiltrarPorStatus / FiltrarPorPrioridade / BuscarPorNomeCliente
    // AtualizarStatus(int id, StatusColeta novo)  -> conferir se existe antes
    // Remover(int id)                             -> conferir se existe antes
}
