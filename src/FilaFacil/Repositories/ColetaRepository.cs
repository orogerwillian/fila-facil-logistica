using FilaFacil.Data;
using FilaFacil.Models;
using Microsoft.EntityFrameworkCore;

namespace FilaFacil.Repositories;

/// <summary>
/// ÚNICA camada que conversa com o banco.
/// Regra do projeto: nada de validação de negócio aqui, só acesso a dados via EF Core.
/// </summary>
public class ColetaRepository
{
    private readonly AppDbContext _db;

    public ColetaRepository(AppDbContext db)
    {
        _db = db;
    }

    // ===================== FASE 2 =====================

    /// <summary>INSERT: grava a coleta e devolve ela já com o Id gerado pelo banco.</summary>
    public Coleta Adicionar(Coleta coleta)
    {
        _db.Coletas.Add(coleta);   // marca a entidade como "Added" no change tracker
        _db.SaveChanges();         // gera e executa o INSERT; o Id volta preenchido
        return coleta;
    }

    /// <summary>SELECT ... ORDER BY: todas as coletas, ordenadas por Id.</summary>
    public List<Coleta> ListarTodas()
    {
        // SELECT ... FROM coletas ORDER BY Id
        // AsNoTracking: só leitura, o EF não precisa acompanhar mudanças nesses objetos.
        return _db.Coletas
            .AsNoTracking()
            .OrderBy(c => c.Id)
            .ToList();
    }

    // ===================== FASE 3 (próxima etapa) =====================
    // ObterPorId(int id)                       -> Coleta?
    // FiltrarPorStatus(StatusColeta status)    -> List<Coleta>
    // FiltrarPorPrioridade(Prioridade p)       -> List<Coleta>
    // BuscarPorNomeCliente(string trecho)      -> List<Coleta>
    // Atualizar(Coleta coleta)                 -> void
    // Remover(Coleta coleta)                   -> void
}
