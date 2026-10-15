
using Microsoft.Data.SqlClient;

public class LivroRepository
{
    public void Cadastrar(string titulo, string autor)
    {
        using SqlConnection connection = Conexao.CriarConexao();
        connection.Open();

        string sql = @"
            INSERT INTO Livros (Titulo, Autor, Disponivel)
            VALUES (@Titulo, @Autor, 1)";

        using SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@Titulo", titulo);
        cmd.Parameters.AddWithValue("@Autor", autor);

        cmd.ExecuteNonQuery();
    }

    public List<Livro> Listar()
    {
        List<Livro> livros = new();

        using SqlConnection connection = Conexao.CriarConexao();
        connection.Open();

        string sql = "SELECT Id, Titulo, Autor, Disponivel FROM Livros";

        using SqlCommand cmd = new SqlCommand(sql, connection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            livros.Add(new Livro
            {
                Id = reader.GetInt32(0),
                Titulo = reader.GetString(1),
                Autor = reader.GetString(2),
                Disponivel = reader.GetBoolean(3)
            });
        }

        return livros;
    }
}
