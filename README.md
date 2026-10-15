# Sistema Biblioteca

Aplicação de console desenvolvida em C# com .NET 10 para gerenciar usuários, livros e empréstimos de uma biblioteca, utilizando SQL Server para persistência dos dados.

## Funcionalidades

- Cadastro e listagem de usuários.
- Cadastro e listagem de livros.
- Registro de empréstimos com validação da existência do usuário e da disponibilidade do livro.
- Devolução de livros.
- Consulta dos empréstimos ativos.
- Persistência de dados em SQL Server.
- Utilização de transações para manter a consistência das operações de empréstimo e devolução.

## Tecnologias utilizadas

- C#
- .NET 10
- SQL Server
- ADO.NET
- Microsoft.Data.SqlClient
- Git e GitHub

## Estrutura do projeto

SistemaBiblioteca
├── Data
│   └── Conexao.cs
├── Models
│   ├── Usuario.cs
│   ├── Livro.cs
│   └── Emprestimo.cs
├── Repositories
│   ├── UsuarioRepository.cs
│   ├── LivroRepository.cs
│   └── EmprestimoRepository.cs
├── Services
│   ├── UsuarioService.cs
│   ├── LivroService.cs
│   └── EmprestimoService.cs
├── Program.cs
└── README.md

## Pré-requisitos

- .NET 10 SDK.
- SQL Server instalado e em execução.
- Banco de dados `BibliotecaDB` criado com as tabelas `Usuarios`, `Livros` e `Emprestimos`, incluindo os relacionamentos necessários.

O banco de dados e suas tabelas precisam estar configurados antes de executar a aplicação.

## Configuração

1. Clone o repositório e acesse a pasta do projeto.
2. Abra `Data/Conexao.cs`.
3. Confira a string de conexão e ajuste o servidor, o banco de dados e o método de autenticação conforme seu ambiente.
4. Verifique se o banco `BibliotecaDB` e as tabelas necessárias estão disponíveis.

A configuração local padrão utiliza autenticação integrada do Windows:

Server=localhost;Database=BibliotecaDB;Trusted_Connection=True;TrustServerCertificate=True;

Essa configuração é destinada ao ambiente local de desenvolvimento. Em outros ambientes, utilize as configurações apropriadas para seu servidor e sua política de segurança.

## Como executar

No terminal, dentro da pasta do projeto, execute:

dotnet restore
dotnet run

Para compilar a versão Release:

dotnet build -c Release

## Organização da aplicação

O projeto separa responsabilidades em camadas:

- **Models:** representam as entidades utilizadas pela aplicação.
- **Repositories:** concentram as operações de acesso ao banco de dados.
- **Services:** intermediam as operações entre o programa e os repositórios.
- **Data:** centraliza a criação da conexão com o SQL Server.
- **Program.cs:** apresenta o menu e coordena a interação com o usuário.

## Objetivo

Projeto de estudo voltado à prática de C#, programação orientada a objetos, operações CRUD, consultas SQL, acesso a dados com ADO.NET, validação de operações e organização de código em camadas.
