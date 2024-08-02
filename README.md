
# Debit Manager

Projeto para Gerenciar Clientes e cobranças.


## Authors

- [@hrguedes](https://github.com/hrguedes)


## Projeto

O Projeto foi desenvolvido utilizando

- [.NET CORE 8](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0)
- [NEXTJS](https://nextjs.org/docs)
- [SHADCN](https://ui.shadcn.com)
- [SQL SERVER](https://www.microsoft.com/pt-br/sql-server/sql-server-2022)


## Appendix

Any additional information goes here


## Passo a Passo

Este monorepo contem o projeto de API na pasta `src` e o projeto WEB
na pasta `web`

### LOCAL

#### Projeto de API

Antes de tudo voçe precisa ter uma instancia do banco de Dados SQL SERVER rodando e precisa alterar a ***CONNECTION_STRING*** dentro do arquivo 
[appsettings.development.json](https://github.com/hrguedes/localize-code-testing/blob/main/src/Hrguedes.Localize.Api/appsettings.Development.json)

```JSON
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=HOSTAQUI;TrustServerCertificate=True;User ID=sa;Password=D3v3l0p3r;Initial Catalog=DebitsManager"
  },
  "AllowedHosts": "*"
}
```

# Observação:
Se preferir voce pode rodar o comando do docker-compose que sera criado ja o 
container do banco de dados e do projeto API. 

[docker-compose.yml](https://github.com/hrguedes/localize-code-testing/blob/main/docker-compose.yml)

```bash
  docker-compose up -d
```
### Execultando

Após feito a alteração da Connection String pode rodar o projeto pois o mesmo será execultado as Migrations e os Seeds

com dados basico e do usuario padrão

```
Nome = "Admin",
Email = "admin@localize.com",
Senha = "password123"
```

O projeto será execultado na porta 808 e podera ser acessado pela URL abaixo 

[localhost:8080](http://localhost:8080)


#### Projeto WEB
Dentro da pasta `web` execultar o seguinte comando para instalar as dependecias

```bash
  npm i
```

Após as dependencias instaladas o projeto podera ser rodado com o comando

```bash
  npm run dev
```

e podera ser acessado pela URL abaixo

[localhost:3000](http://localhost:3000/auth/logout)

