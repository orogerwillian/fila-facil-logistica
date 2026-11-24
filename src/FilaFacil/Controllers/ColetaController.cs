using FilaFacil.Models;
using FilaFacil.Services;

namespace FilaFacil.Controllers;

/// <summary>
/// Menu do terminal: lê a entrada, chama o service e mostra o resultado.
/// Regra do projeto: NÃO acessa o banco nem o repository, só o service.
/// </summary>
public class ColetaController
{
    private readonly ColetaService _service;

    public ColetaController(ColetaService service)
    {
        _service = service;
    }

    /// <summary>Loop principal do menu. (Pronto: não precisa mexer.)</summary>
    public void Executar()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("===== FilaFácil Logística =====");
            Console.WriteLine("1 - Cadastrar coleta");
            Console.WriteLine("2 - Listar coletas");
            Console.WriteLine("3 - Filtrar coletas        (Fase 3)");
            Console.WriteLine("4 - Atualizar status       (Fase 3)");
            Console.WriteLine("5 - Remover coleta         (Fase 3)");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha: ");

            var opcao = Console.ReadLine()?.Trim();
            if (opcao == "0")
            {
                Console.WriteLine("Até logo!");
                return;
            }

            try
            {
                switch (opcao)
                {
                    case "1": CadastrarColeta(); break;
                    case "2": ListarColetas(); break;
                    case "3":
                    case "4":
                    case "5": Console.WriteLine("Disponível na Fase 3."); break;
                    default: Console.WriteLine("Opção inválida."); break;
                }
            }
            catch (ArgumentException ex)
            {
                // Erros de validação vindos do service.
                Console.WriteLine($"Erro: {ex.Message}");
            }
            catch (NotImplementedException ex)
            {
                Console.WriteLine($"[TODO] {ex.Message}");
            }
        }
    }

    // ===================== FASE 2 =====================

    private void CadastrarColeta()
    {
        Console.WriteLine();
        Console.WriteLine("--- Nova coleta ---");

        var nome = LerTexto("Nome do cliente");
        var endereco = LerTexto("Endereço");
        var pacotes = LerInteiro("Quantidade de pacotes");
        var prioridade = LerEnum<Prioridade>("Prioridade");

        // Se algum dado for inválido, o service lança ArgumentException
        // e o catch do Executar() mostra a mensagem.
        var coleta = _service.Cadastrar(nome, endereco, pacotes, prioridade);

        Console.WriteLine($"Coleta #{coleta.Id} cadastrada com sucesso!");
    }

    private void ListarColetas()
    {
        var coletas = _service.ListarTodas();

        Console.WriteLine();
        if (coletas.Count == 0)
        {
            Console.WriteLine("Nenhuma coleta cadastrada.");
            return;
        }

        Console.WriteLine($"--- {coletas.Count} coleta(s) ---");
        foreach (var coleta in coletas)
            Console.WriteLine(Formatar(coleta));
    }

    // ===================== Helpers de leitura (prontos) =====================

    private static string LerTexto(string rotulo)
    {
        Console.Write($"{rotulo}: ");
        return Console.ReadLine() ?? string.Empty;
    }

    private static int LerInteiro(string rotulo)
    {
        while (true)
        {
            Console.Write($"{rotulo}: ");
            if (int.TryParse(Console.ReadLine(), out var valor))
                return valor;
            Console.WriteLine("Digite um número inteiro válido.");
        }
    }

    /// <summary>Mostra as opções do enum numeradas e lê a escolha.</summary>
    private static T LerEnum<T>(string rotulo) where T : struct, Enum
    {
        var valores = Enum.GetValues<T>();
        while (true)
        {
            Console.WriteLine($"{rotulo}:");
            foreach (var v in valores)
                Console.WriteLine($"  {Convert.ToInt32(v)} - {v}");
            Console.Write("Escolha: ");

            if (int.TryParse(Console.ReadLine(), out var numero) && Enum.IsDefined(typeof(T), numero))
                return (T)Enum.ToObject(typeof(T), numero);

            Console.WriteLine("Opção inválida.");
        }
    }

    private static string Formatar(Coleta c) =>
        $"#{c.Id} | {c.NomeCliente} | {c.Endereco} | {c.QuantidadePacotes} pct | " +
        $"{c.Prioridade} | {c.Status} | {c.DataCriacao:dd/MM/yyyy HH:mm}";
}
