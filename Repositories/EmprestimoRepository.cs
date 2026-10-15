using Microsoft.Data.SqlClient;

public class EmprestimoRepository
{
    public bool UsuarioExiste(int usuarioId)
    {
        using SqlConnection connection = Conexao.CriarConexao();
        connection.Open();

        string sql = "SELECT COUNT(1) FROM Usuarios WHERE Id = @UsuarioId";

        using SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);

        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
    }

    public bool LivroEstaDisponivel(int livroId)
    {
        using SqlConnection connection = Conexao.CriarConexao();
        connection.Open();

        string sql = "SELECT Disponivel FROM Livros WHERE Id = @LivroId";

        using SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@LivroId", livroId);

        object? resultado = cmd.ExecuteScalar();

        return resultado != null
            && resultado != DBNull.Value
            && (bool)resultado;
    }

    public bool RegistrarEmprestimo(int usuarioId, int livroId)
    {
        using SqlConnection connection = Conexao.CriarConexao();
        connection.Open();

        using SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            string sqlLivro = @"
                UPDATE Livros
                SET Disponivel = 0
                WHERE Id = @LivroId
                  AND Disponivel = 1";

            int linhasAfetadas;

            using (SqlCommand cmd = new SqlCommand(
                sqlLivro, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@LivroId", livroId);
                linhasAfetadas = cmd.ExecuteNonQuery();
            }

            if (linhasAfetadas != 1)
            {
                transaction.Rollback();
                return false;
            }

            string sqlEmprestimo = @"
                INSERT INTO Emprestimos (UsuarioId, LivroId)
                VALUES (@UsuarioId, @LivroId)";

            using (SqlCommand cmd = new SqlCommand(
                sqlEmprestimo, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                cmd.Parameters.AddWithValue("@LivroId", livroId);
                cmd.ExecuteNonQuery();
            }

            transaction.Commit();
            return true;
        }
        catch
        {
            try
            {
                transaction.Rollback();
            }
            catch
            {
                // Preserva a exceção original caso o rollback falhe.
            }

            throw;
        }
    }

    public bool DevolverLivro(int livroId)
    {
        using SqlConnection connection = Conexao.CriarConexao();
        connection.Open();

        using SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            string sqlEmprestimo =
                "DELETE FROM Emprestimos WHERE LivroId = @LivroId";

            int linhasAfetadas;

            using (SqlCommand cmd = new SqlCommand(
                sqlEmprestimo, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@LivroId", livroId);
                linhasAfetadas = cmd.ExecuteNonQuery();
            }

            if (linhasAfetadas == 0)
            {
                transaction.Rollback();
                return false;
            }

            string sqlLivro = @"
                UPDATE Livros
                SET Disponivel = 1
                WHERE Id = @LivroId";

            using (SqlCommand cmd = new SqlCommand(
                sqlLivro, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@LivroId", livroId);
                cmd.ExecuteNonQuery();
            }

            transaction.Commit();
            return true;
        }
        catch
        {
            try
            {
                transaction.Rollback();
            }
            catch
            {
                // Preserva a exceção original caso o rollback falhe.
            }

            throw;
        }
    }

    public List<Emprestimo> Listar()
    {
        List<Emprestimo> emprestimos = new();

        using SqlConnection connection = Conexao.CriarConexao();
        connection.Open();

        string sql = @"
            SELECT
                U.Id AS UsuarioId,
                U.Nome AS Usuario,
                L.Id AS LivroId,
                L.Titulo AS Livro
            FROM Emprestimos E
            INNER JOIN Usuarios U ON E.UsuarioId = U.Id
            INNER JOIN Livros L ON E.LivroId = L.Id";

        using SqlCommand cmd = new SqlCommand(sql, connection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            emprestimos.Add(new Emprestimo
            {
                UsuarioId = reader.GetInt32(0),
                Usuario = reader.GetString(1),
                LivroId = reader.GetInt32(2),
                Livro = reader.GetString(3)
            });
        }

        return emprestimos;
    }
}