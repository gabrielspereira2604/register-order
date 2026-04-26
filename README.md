# Register Order

API REST para registro de pedidos, desenvolvida como desafio técnico.

---

## Como executar

**Pré-requisitos:** .NET 9 SDK e Docker.

> O SQL Server roda via Docker, eliminando a necessidade de instalação local. O container sobe um servidor isolado na porta `1433` pronto para uso.

```bash
# 1. Subir o banco
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Strong@Password123" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest

# 2. Aplicar migrations
dotnet ef database update --project RegisterOrder.API

# 3. API (terminal 1)
dotnet run --project RegisterOrder.API

# 4. Frontend (terminal 2)
dotnet run --project RegisterOrder.Web
```

- Testes: `dotnet test`
- API: `http://localhost:5121`
- Documentação: `http://localhost:5121/scalar/v1`
- Frontend: `http://localhost:5103`

---

## Funcionalidades

- CRUD completo de pedidos (criar, listar, buscar, editar, excluir)
- Cardápio com sanduíches, acompanhamentos e bebidas
- Cálculo automático de desconto por combinação de itens:
  - Sanduíche + Acompanhamento + Bebida → 20%
  - Sanduíche + Bebida → 15%
  - Sanduíche + Acompanhamento → 10%
- Validação de entrada com erros claros (itens duplicados, IDs inválidos, lista vazia)
- Frontend em Blazor WebAssembly com listagem paginada, formulário de pedido e confirmação de exclusão
- Testes automatizados das regras de negócio e validação (16 testes)

---

## Decisões técnicas

**N-Layer em projeto único** — o escopo do problema (3 entidades, regras de desconto bem definidas) não justifica a cerimônia de Clean Architecture. N-Layer entrega a separação necessária sem overhead.

**SQL Server** — banco relacional alinhado ao ecossistema .NET, permite demonstrar EF Core com migrations versionadas.

**FluentValidation** — validação estrutural antes de chegar ao service (lista nula, vazia, IDs inválidos, duplicados). Erros de negócio que dependem do banco (tipo duplicado, ID inexistente) ficam no service.

**Problem Details (RFC 7807)** — formato padronizado para erros da API, com `title` e `detail` consistentes em todos os endpoints.

**Blazor WebAssembly** — SPA executada no browser em .NET, sem necessidade de JavaScript customizado. Consome a API por HTTP e mantém a lógica de apresentação separada do backend.

**xUnit + NSubstitute** — testes focados no `OrderService` e no `OrderRequestValidator`, onde vivem as regras de negócio. Repositórios e controllers não são testados por não possuírem lógica própria.

---
