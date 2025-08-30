# API de Gestão de Clientes

API de exemplo para cadastro e consulta de clientes, desenvolvida para demonstrar a aplicação de conceitos de Clean Architecture em .NET 9.

## 🎯 Objetivo

O projeto implementa uma "feature slice" vertical que permite a criação de um novo cliente e a sua posterior consulta por ID, seguindo as melhores práticas de design e arquitetura de software.

## ✨ Princípios e Tecnologias

Este projeto foi construído sobre os seguintes pilares:

- **Linguagem/Plataforma**: C# / .NET 9
- **Arquitetura Principal**: **Clean Architecture**, garantindo a separação de responsabilidades e o baixo acoplamento entre as camadas.
- **Padrão de Comunicação**: **CQRS** (Command Query Responsibility Segregation) utilizando a biblioteca `MediatR`.
- **Design de Domínio**: Conceitos de **Domain-Driven Design (DDD)** com o uso de Entidades e Value Objects.
- **Persistência**: **Padrão Repository** com uma implementação em memória para desacoplar a lógica de negócio do acesso a dados.
- **Testes**: Testes unitários com **xUnit** e **Moq** para garantir a qualidade e o comportamento esperado da camada de aplicação.

## 🏗️ Estrutura do Projeto

A solução está organizada em projetos que representam as camadas da Clean Architecture:

-   **`GestaoClientes.Domain`**
    O núcleo da aplicação. Contém as Entidades (`Cliente`), Value Objects (`Cnpj`) e as interfaces dos repositórios (`IClienteRepository`). Não depende de nenhuma outra camada.

-   **`GestaoClientes.Application`**
    Contém a lógica de negócio e os casos de uso. Implementa o padrão CQRS com Commands (operações de escrita) e Queries (operações de leitura) e seus respectivos Handlers. Depende apenas do `Domain`.

-   **`GestaoClientes.Infrastructure`**
    Implementa as abstrações definidas em camadas mais internas. Neste projeto, encontra-se o `ClienteRepositoryEmMemoria`, que é a implementação concreta da interface `IClienteRepository`. Depende do `Application`.

-   **`GestaoClientes.API`**
    A camada de apresentação. É um projeto ASP.NET Core Web API que expõe os endpoints HTTP. Ele recebe as requisições e as delega para a camada de `Application` através do MediatR.

-   **`GestaoClientes.Tests`**
    Projeto de testes unitários que valida a lógica dos Handlers na camada de `Application`, utilizando mocks para simular as dependências externas (como o repositório).

## 🚀 Como Executar a Aplicação

### Pré-requisitos
-   [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
-   [Visual Studio 2022](https://visualstudio.microsoft.com/) ou um editor de sua preferência.

### Passos
1.  Clone este repositório.
2.  Abra a solução (`.sln`) no Visual Studio.
3.  Defina o projeto `GestaoClientes.API` como projeto de inicialização.
4.  Pressione `F5` ou clique no botão de "Play" para executar a API.
5.  O navegador será aberto automaticamente na interface do Swagger em `http://localhost:<porta>/swagger`.

## 🧪 Como Executar os Testes

Os testes unitários validam os cenários de sucesso e de falha dos casos de uso.

### Via Visual Studio
1.  No menu superior, vá para **Exibir > Gerenciador de Testes**.
2.  Na janela do Gerenciador de Testes, clique em **"Executar Todos os Testes"**.

### Via Linha de Comando
Navegue até a pasta raiz da solução e execute o seguinte comando:
```sh
dotnet test
```

## Endpoints da API

### Criar um Novo Cliente
`POST /api/clientes`

**Corpo da Requisição (Exemplo):**
```json
{
  "nomeFantasia": "Minha Empresa S.A.",
  "cnpj": "12.345.678/0001-99"
}
```

-   **Resposta de Sucesso:** `201 Created`

### Consultar Cliente por ID
`GET /api/clientes/{id}`

**Parâmetro:**
- `id` (guid): O ID do cliente a ser consultado.

-   **Resposta de Sucesso:** `200 OK` com os dados do cliente.
-   **Resposta de Falha:** `404 Not Found` se o cliente não for encontrado.
