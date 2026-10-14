# SistemaBiblioteca

Aplicação de console desenvolvida em C# para gerenciar operações básicas de uma biblioteca, utilizando SQL Server para armazenar os dados.

## Funcionalidades

- Cadastro e listagem de usuários.
- Cadastro e listagem de livros.
- Registro de empréstimos.
- Devolução de livros.
- Consulta dos empréstimos registrados.

## Tecnologias utilizadas

- C#
- .NET 10
- SQL Server
- ADO.NET com Microsoft.Data.SqlClient

## Pré-requisitos

- .NET 10 SDK.
- SQL Server instalado e em execução.
- Banco de dados `BibliotecaDB` e suas tabelas já criados.

## Como executar

1. Clone este repositório.
2. Abra a pasta do projeto no terminal.
3. Confira a string de conexão no arquivo `Program.cs` e ajuste-a conforme sua instância do SQL Server.
4. Certifique-se de que o banco de dados e as tabelas necessárias existem.
5. Execute os comandos:

```bash
dotnet restore
dotnet run
```

## Objetivo

Projeto desenvolvido para praticar C#, operações CRUD, consultas SQL e integração com banco de dados relacional utilizando ADO.NET.
