
using Microsoft.Data.SqlClient;

public static class Conexao
{
    private static readonly string stringConexao =
        "Server=localhost;Database=BibliotecaDB;Trusted_Connection=True;TrustServerCertificate=True;";

    public static SqlConnection CriarConexao()
    {
        return new SqlConnection(stringConexao);
    }
}
