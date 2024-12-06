# API DeviceHub

## Descrição

A **API DeviceHub** é uma aplicação que tem como objetivo gerenciar ativos de empresas, incluindo **notebooks, desktops, monitores e periféricos**. Através desta API, é possível cadastrar e realizar operações em departamentos, funcionários, ativos e seus respectivos dados como fornecedor, garantia, manutenção e licença.

A aplicação foi desenvolvida com **ASP.NET Core** e utiliza o **Entity Framework** para gerenciamento de banco de dados, com suporte para SQL Server ou banco de dados em memória.

## Funcionalidades

- Cadastro de **Departamentos**.
- Cadastro de **Funcionários**.
- Cadastro de **Ativos** (Notebooks, Desktops, Monitores, etc.).
- Associação de **Ativos** a **Funcionários** e **Departamentos**.
- Gestão de **Garantias**, **Licenças** e **Manutenções** dos ativos.
- Consulta e atualização dos registros.

## Tecnologias Utilizadas

- **ASP.NET Core 6** ou superior
- **Entity Framework Core** (para persistência de dados)
- **Banco de Dados em Memória** (para desenvolvimento)
- **Swagger** (para documentação e testes da API)

## Requisitos

Antes de rodar a aplicação, você precisa ter o seguinte instalado:

- **.NET SDK** (versão 6 ou superior) - [Download](https://dotnet.microsoft.com/download/dotnet)
- **SQL Server** ou banco de dados em memória (para desenvolvimento)
- **Visual Studio** ou **Visual Studio Code** (opcional, mas recomendado para desenvolvimento)

## Como Executar a Aplicação

### Passo 1: Clonar o repositório

Clone o repositório da API:

```bash
git clone https://github.com/usuario/repo-devicehub.git
```

### Passo 2: Restaurar as dependências
Navegue até o diretório do projeto e execute o comando para restaurar as dependências:
```bash
cd repo-devicehub
dotnet restore
```

### Passo 3: Executar a aplicação
Para rodar a aplicação, execute o comando:
```bash
dotnet run
```
Isso iniciará o servidor da API, e você poderá acessar os endpoints através de http://localhost:5175.

### Passo 4: Testar os Endpoints
A API está documentada com o Swagger, que pode ser acessado na URL:
```bash
http://localhost:5175/swagger
```
Isso permite que você veja todos os endpoints disponíveis e realize testes diretamente no navegador.
