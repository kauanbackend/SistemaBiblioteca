
using Microsoft.Data.SqlClient;

public class UsuarioRepository
{
    public void Cadastrar(string nome, int idade)
    {
        using SqlConnection connection = Conexao.CriarConexao();
        connection.Open();

        string sql = "INSERT INTO Usuarios (Nome, Idade) VALUES (@Nome, @Idade)";

        using SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@Nome", nome);
        cmd.Parameters.AddWithValue("@Idade", idade);

        cmd.ExecuteNonQuery();
    }

    public List<Usuario> Listar()
    {
        List<Usuario> usuarios = new();

        using SqlConnection connection = Conexao.CriarConexao();
        connection.Open();

        string sql = "SELECT Id, Nome, Idade FROM Usuarios";

        using SqlCommand cmd = new SqlCommand(sql, connection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            usuarios.Add(new Usuario
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Idade = reader.GetInt32(2)
            });
        }

        return usuarios;
    }
}
