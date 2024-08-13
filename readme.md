using sendGrid to send emails to user:
```csharp
dotnet add package SendGrid --version 9.28.1
```

Adding and updating migration:
```csharp
dotnet ef migrations add InitialCreate --output-dir Data\Migrations
dotnet ef database update
```

Resolvendo bug do database:
Criei campos dinamicos para um banco de dados ja existente. Ao tentar atualizar o banco de dados, essas novas colunas (campos) nao estavam sendo inseridos na tabela.
Resolucao do bug:

1. Resetar o DB: Quando você pode perder os dados existentes e deseja sincronizar completamente as migrações com o banco de dados.
```csharp
dotnet ef database drop --force
dotnet ef migrations add InitialCreate --output-dir Data/Migrations
dotnet ef database update
```

2. Ajustar Migrações Sem Excluir o Banco de Dados: Quando você precisa preservar os dados existentes no banco de dados.
```csharp
dotnet ef migrations add AddFirstAndLastName --output-dir Data/Migrations
dotnet ef database update

```