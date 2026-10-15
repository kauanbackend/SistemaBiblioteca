
public class UsuarioService
{
    private readonly UsuarioRepository _repository = new();

    public void Cadastrar(string nome, int idade)
    {
        _repository.Cadastrar(nome, idade);
    }

    public List<Usuario> Listar()
    {
        return _repository.Listar();
    }
}
