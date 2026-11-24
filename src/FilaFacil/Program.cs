using FilaFacil.Controllers;
using FilaFacil.Data;
using FilaFacil.Repositories;
using FilaFacil.Services;
using Microsoft.EntityFrameworkCore;

using var db = new AppDbContext();

// Garante que o banco existe e que a tabela "coletas" está atualizada.
db.Database.Migrate();

// Montagem manual das camadas: controller -> service -> repository -> banco.
var repository = new ColetaRepository(db);
var service = new ColetaService(repository);
var controller = new ColetaController(service);

controller.Executar();
