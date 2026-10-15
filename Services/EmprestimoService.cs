public class EmprestimoService
{
    private readonly EmprestimoRepository _repository = new();

    public bool UsuarioExiste(int usuarioId)
    {
        return _repository.UsuarioExiste(usuarioId);
    }

    public bool LivroEstaDisponivel(int livroId)
    {
        return _repository.LivroEstaDisponivel(livroId);
    }

    public bool RegistrarEmprestimo(int usuarioId, int livroId)
    {
        return _repository.RegistrarEmprestimo(usuarioId, livroId);
    }

    public bool DevolverLivro(int livroId)
    {
        return _repository.DevolverLivro(livroId);
    }

    public List<Emprestimo> Listar()
    {
        return _repository.Listar();
    }
}