using FilaFacil.Models;
using Microsoft.EntityFrameworkCore;

namespace FilaFacil.Data;

public class AppDbContext : DbContext
{
    public DbSet<Coleta> Coletas => Set<Coleta>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // O arquivo filafacil.db é criado na pasta de onde o programa é executado.
        options.UseSqlite("Data Source=filafacil.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var coleta = modelBuilder.Entity<Coleta>();

        coleta.ToTable("coletas");
        coleta.HasKey(c => c.Id);

        coleta.Property(c => c.NomeCliente).IsRequired().HasMaxLength(120);
        coleta.Property(c => c.Endereco).IsRequired().HasMaxLength(250);
        coleta.Property(c => c.QuantidadePacotes).IsRequired();

        // Enums salvos como texto: o banco fica legível ("Alta", "Pendente").
        coleta.Property(c => c.Prioridade).IsRequired().HasConversion<string>().HasMaxLength(20);
        coleta.Property(c => c.Status).IsRequired().HasConversion<string>().HasMaxLength(20);

        coleta.Property(c => c.DataCriacao).IsRequired();
    }
}
