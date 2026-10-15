using System;
using Microsoft.Data.SqlClient;

class Program
{
    static string conexao = "Server=localhost;Database=BibliotecaDB;Trusted_Connection=True;TrustServerCertificate=True;";

    static void Main()
    {
        int opcao;

        do
        {
            Console.WriteLine("\n--- SISTEMA BIBLIOTECA ---");
            Console.WriteLine("1 - Cadastrar usuário");
            Console.WriteLine("2 - Listar usuário");
            Console.WriteLine("3 - Cadastrar livro");
            Console.WriteLine("4 - Listar livro");
            Console.WriteLine("5 - Emprestar livro");
            Console.WriteLine("6 - Devolver livro");
            Console.WriteLine("7 - Listar empréstimos");
            Console.WriteLine("0 - Sair");

            Console.WriteLine("Escolha:");
            opcao = int.Parse(Console.ReadLine() ?? "0");

            switch(opcao)
            {
                case 1:
                CadastrarUsuario();
                break;

                case 2:
                ListarUsuario();
                break;

                case 3:
                CadastrarLivro();
                break;

                case 4:
                ListarLivro();
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
        }while(opcao !=0);
    }

    static void CadastrarUsuario()
    {
        Console.Write("Nome:");
        string nome = Console.ReadLine()??"";

        Console.Write("Idade:");
        int idade = int.Parse(Console.ReadLine() ??"0");

        using(SqlConnection connection = new SqlConnection(conexao))
        {
            connection.Open();

            string sql = "INSERT INTO Usuarios(Nome, Idade) VALUES(@Nome, @Idade)";

            using(SqlCommand cmd = new SqlCommand(sql,connection))
            {
                cmd.Parameters.AddWithValue("@Nome", nome);
                cmd.Parameters.AddWithValue("@Idade", idade);

                cmd.ExecuteNonQuery();
            }
        }

        Console.WriteLine("Usuário cadastrado!");
    }

    static void ListarUsuario()
    {
        using(SqlConnection connection = new SqlConnection(conexao))
        {
            connection.Open();

            string sql = "SELECT * FROM Usuarios";

            using(SqlCommand cmd = new SqlCommand(sql,connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Console.WriteLine(
                        $"{reader["Id"]} - {reader["Nome"]} - {reader["Idade"]}"
                        );
                    }
                }
            }
        }
    }

    static void CadastrarLivro()
    {
        Console.Write("Titulo:");
        string titulo = Console.ReadLine()??"";

        Console.Write("Autor:");
        string autor = Console.ReadLine()??"";

        using(SqlConnection connection = new SqlConnection(conexao))
        {
            connection.Open();

            string sql = "INSERT INTO Livros (Titulo, Autor, Disponivel) VALUES(@Titulo, @Autor, 1)";

            using(SqlCommand cmd = new SqlCommand(sql,connection))
            {
                cmd.Parameters.AddWithValue("@Titulo", titulo);
                cmd.Parameters.AddWithValue("@Autor", autor);

                cmd.ExecuteNonQuery();
            }
        }

        Console.WriteLine("Livro Cadastrado!");
    }

    static void ListarLivro()
    {
        using(SqlConnection connection = new SqlConnection(conexao))
        {
            connection.Open();

            string sql = "SELECT * FROM Livros";

            using(SqlCommand cmd = new SqlCommand(sql,connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Console.WriteLine(
                            $"{reader["Id"]} - {reader["Titulo"]} - {reader["Autor"]}"
                        );
                    }
                }
            }
        }
    }

    static void EmprestarLivro()
    {
        Console.Write("ID do usuário: ");
        int usuarioId = int.Parse(Console.ReadLine()?? "0");

        Console.Write("ID do livro: ");
        int livroId = int.Parse(Console.ReadLine()?? "0");

        using(SqlConnection connection = new SqlConnection(conexao))
        {
            connection.Open();

            string verificarLivro = "SELECT Disponivel FROM Livros WHERE Id = @LivroId";

            using(SqlCommand verificarCmd = new SqlCommand(verificarLivro, connection))
            {
                verificarCmd.Parameters.AddWithValue("@LivroId", livroId);

                object resultado = verificarCmd.ExecuteScalar();

                if(resultado != null && !(bool)resultado)
                {
                    Console.WriteLine("Livro já emprestado.");
                    return;
                }
            }

            string sqlEmprestimo = "INSERT INTO Emprestimos (UsuarioId, LivroId) VALUES (@UsuarioId, @LivroId)";

            using(SqlCommand cmd = new SqlCommand(sqlEmprestimo, connection))
            {
                cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                cmd.Parameters.AddWithValue("@LivroId", livroId);
                cmd.ExecuteNonQuery();
            }

            string sqlLivro = "UPDATE Livros SET Disponivel = 0 WHERE Id = @LivroId ";

            using(SqlCommand cmd = new SqlCommand(sqlLivro, connection))
            {
                cmd.Parameters.AddWithValue("@LivroId", livroId);

                cmd.ExecuteNonQuery();
            }
        }

        Console.WriteLine("Livro emprestado com sucesso!");
    }

    static void DevolverLivro()
    {
        Console.Write("Id do livro: ");
        int livroId = int.Parse(Console.ReadLine()?? "0");

        using(SqlConnection connection = new SqlConnection(conexao))
        {
            connection.Open();

            string sqlEmprestimo = "DELETE FROM Emprestimos WHERE LivroId = @LivroId";

            using(SqlCommand cmd = new SqlCommand(sqlEmprestimo, connection))
            {
                cmd.Parameters.AddWithValue("@LivroId", livroId);

                cmd.ExecuteNonQuery();
            }

            string sqlLivro = "UPDATE Livros SET Disponivel = 1 WHERE Id = @LivroId";

            using(SqlCommand cmd = new SqlCommand(sqlLivro, connection))
            {
                cmd.Parameters.AddWithValue("@LivroId", livroId);

                cmd.ExecuteNonQuery();
            }
        }

        Console.WriteLine("Livro devolvido com sucesso!");
    }

    static void ListarEmprestimos()
    {
        using(SqlConnection connection = new SqlConnection(conexao))
        {

            connection.Open();

        string sql = @"
        SELECT
        U.Nome AS Usuario,
        L.Titulo AS Livro
        FROM Emprestimos E
        INNER JOIN Usuarios U ON E.UsuarioId = U.Id
        INNER JOIN Livros L ON E.LivroId = L.Id";

        using(SqlCommand cmd = new SqlCommand(sql, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Console.WriteLine(
                            $"{reader["Usuario"]} - {reader["Livro"]}"
                        );
                    }
                }
            }
        }
    }
}
