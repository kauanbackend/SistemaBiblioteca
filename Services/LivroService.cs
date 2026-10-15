
public class LivroService
{
    private readonly LivroRepository _repository = new();

    public void Cadastrar(string titulo, string autor)
    {
        _repository.Cadastrar(titulo, autor);
    }

    public List<Livro> Listar()
    {
        return _repository.Listar();
    }
}
