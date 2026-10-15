using Microsoft.Data.SqlClient;
class Program
{
    static readonly UsuarioService usuarioService = new();
    static readonly LivroService livroService = new();
    static readonly EmprestimoService emprestimoService = new();

    static void Main()
    {
        int opcao;

        do
        {
            Console.WriteLine("\n--- SISTEMA BIBLIOTECA ---");
            Console.WriteLine("1 - Cadastrar usuário");
            Console.WriteLine("2 - Listar usuários");
            Console.WriteLine("3 - Cadastrar livro");
            Console.WriteLine("4 - Listar livros");
            Console.WriteLine("5 - Emprestar livro");
            Console.WriteLine("6 - Devolver livro");
            Console.WriteLine("7 - Listar empréstimos");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opção inválida.");
                continue;
            }

            switch (opcao)
            {
                case 1:
                    CadastrarUsuario();
                    break;

                case 2:
                    ListarUsuarios();
                    break;

                case 3:
                    CadastrarLivro();
                    break;

                case 4:
                    ListarLivros();
                    break;

                case 5:
                    EmprestarLivro();
                    break;

                case 6:
                    DevolverLivro();
                    break;

                case 7:
                    ListarEmprestimos();
                    break;

                case 0:
                    Console.WriteLine("Saindo...");
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

        } while (opcao != 0);
    }

    static void CadastrarUsuario()
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine() ?? "";

        Console.Write("Idade: ");

        if (!int.TryParse(Console.ReadLine(), out int idade))
        {
            Console.WriteLine("Idade inválida.");
            return;
        }

        usuarioService.Cadastrar(nome, idade);
        Console.WriteLine("Usuário cadastrado!");
    }

    static void ListarUsuarios()
    {
        List<Usuario> usuarios = usuarioService.Listar();

        foreach (Usuario usuario in usuarios)
        {
            Console.WriteLine(
                $"{usuario.Id} - {usuario.Nome} - {usuario.Idade}");
        }
    }

    static void CadastrarLivro()
    {
        Console.Write("Título: ");
        string titulo = Console.ReadLine() ?? "";

        Console.Write("Autor: ");
        string autor = Console.ReadLine() ?? "";

        livroService.Cadastrar(titulo, autor);
        Console.WriteLine("Livro cadastrado!");
    }

    static void ListarLivros()
    {
        List<Livro> livros = livroService.Listar();

        foreach (Livro livro in livros)
        {
            string disponibilidade = livro.Disponivel
                ? "Disponível"
                : "Emprestado";

            Console.WriteLine(
                $"{livro.Id} - {livro.Titulo} - {livro.Autor} - {disponibilidade}");
        }
    }
static void EmprestarLivro()
{
    Console.Write("ID do usuário: ");

    if (!int.TryParse(Console.ReadLine(), out int usuarioId))
    {
        Console.WriteLine("ID de usuário inválido.");
        return;
    }

    Console.Write("ID do livro: ");

    if (!int.TryParse(Console.ReadLine(), out int livroId))
    {
        Console.WriteLine("ID de livro inválido.");
        return;
    }

    try
    {
        if (!emprestimoService.UsuarioExiste(usuarioId))
        {
            Console.WriteLine("Usuário não encontrado.");
            return;
        }

        if (!emprestimoService.LivroEstaDisponivel(livroId))
        {
            Console.WriteLine("Livro indisponível ou não encontrado.");
            return;
        }

        bool emprestado = emprestimoService.RegistrarEmprestimo(
            usuarioId, livroId);

        Console.WriteLine(emprestado
            ? "Livro emprestado com sucesso!"
            : "Não foi possível emprestar. Verifique a disponibilidade do livro.");
    }
    catch (SqlException)
    {
        Console.WriteLine(
            "Erro ao acessar o banco de dados. Nenhum sucesso foi confirmado.");
    }
    catch (Exception)
    {
        Console.WriteLine("Ocorreu um erro inesperado ao realizar o empréstimo.");
    }
}
    static void DevolverLivro()
    {
        Console.Write("ID do livro: ");

        if (!int.TryParse(Console.ReadLine(), out int livroId))
        {
            Console.WriteLine("ID de livro inválido.");
            return;
        }

        bool devolvido = emprestimoService.DevolverLivro(livroId);

        Console.WriteLine(devolvido
            ? "Livro devolvido com sucesso!"
            : "Nenhum empréstimo encontrado para esse livro.");
    }

    static void ListarEmprestimos()
    {
        List<Emprestimo> emprestimos = emprestimoService.Listar();

        foreach (Emprestimo emprestimo in emprestimos)
        {
            Console.WriteLine(
                $"{emprestimo.Usuario} - {emprestimo.Livro}");
        }
    }
}
