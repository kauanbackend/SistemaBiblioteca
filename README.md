# 📚 Sistema de Biblioteca

Aplicação de console desenvolvida em C# com .NET 10 e SQL Server para gerenciamento de usuários, livros e empréstimos de uma biblioteca.

O projeto foi desenvolvido com foco em orientação a objetos, acesso a banco de dados e organização do código em camadas.

## 🚀 Funcionalidades

- Cadastro e listagem de usuários.
- Cadastro e listagem de livros.
- Empréstimo de livros com validação de usuário e disponibilidade.
- Devolução de livros.
- Listagem de empréstimos ativos.
- Uso de transações SQL para manter a consistência das operações de empréstimo e devolução.

## 🛠️ Tecnologias utilizadas

- C#
- .NET 10
- SQL Server
- ADO.NET
- Microsoft.Data.SqlClient
- Git e GitHub

## 📂 Estrutura do projeto

```text
SistemaBiblioteca/
├── Data/
│   └── Conexao.cs
├── Models/
│   ├── Usuario.cs
│   ├── Livro.cs
│   └── Emprestimo.cs
├── Repositories/
│   ├── UsuarioRepository.cs
│   ├── LivroRepository.cs
│   └── EmprestimoRepository.cs
├── Services/
│   ├── UsuarioService.cs
│   ├── LivroService.cs
│   └── EmprestimoService.cs
├── Program.cs
└── README.md
```

### Organização em camadas

- **Data:** configuração e conexão com o banco de dados.
- **Models:** classes que representam usuários, livros e empréstimos.
- **Repositories:** operações de acesso e persistência de dados.
- **Services:** regras de negócio e validações.
- **Program.cs:** ponto de entrada da aplicação.

## ⚙️ Pré-requisitos

Antes de executar o projeto, você precisa ter:

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- SQL Server instalado e em execução.
- Banco de dados `BibliotecaDB` criado e configurado com as tabelas `Usuarios`, `Livros` e `Emprestimos`, incluindo seus relacionamentos.

## 🔌 Configuração do banco de dados

A conexão está configurada no arquivo `Data/Conexao.cs`.

Exemplo de configuração para um ambiente local com autenticação integrada do Windows:

```csharp
Server=localhost;Database=BibliotecaDB;Trusted_Connection=True;TrustServerCertificate=True;
```

Se o seu SQL Server utilizar outra instância ou configuração de autenticação, ajuste a string de conexão conforme o seu ambiente.

## ▶️ Como executar

Clone o repositório:

```bash
git clone https://github.com/kauanbackend/SistemaBiblioteca.git
```

Entre na pasta do projeto:

```bash
cd SistemaBiblioteca
```

Restaure as dependências:

```bash
dotnet restore
```

Execute a aplicação:

```bash
dotnet run
```

Para compilar em modo Release:

```bash
dotnet build -c Release
```

## 🎯 Objetivo do projeto

Este projeto tem como objetivo praticar e consolidar conhecimentos em:

- Programação orientada a objetos com C#.
- Organização de aplicações em camadas.
- Operações CRUD e persistência de dados.
- Integração entre C# e SQL Server utilizando ADO.NET.
- Validação de regras de negócio.
- Transações SQL e consistência de dados.
- Controle de versão com Git e GitHub.

## 👨‍💻 Autor

**Kauan da Silva**

GitHub: [kauanbackend](https://github.com/kauanbackend)