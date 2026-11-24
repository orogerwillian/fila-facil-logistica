using FilaFacil.Data;
using Microsoft.EntityFrameworkCore;

// Fase 1: garante que o banco existe e que a tabela "coletas" está criada.
using (var db = new AppDbContext())
{
    db.Database.Migrate();
    var total = db.Coletas.Count();
    Console.WriteLine($"Banco conectado. Coletas cadastradas: {total}");
}

// Fase 2+: aqui entra o menu interativo (controller).
