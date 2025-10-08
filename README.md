# 🧩 ContractManagement

> Um projeto de estudos sobre **Domain-Driven Design (DDD)** e **Clean Architecture**, desenvolvido em **ASP.NET Core** com **Entity Framework Core**.  
> O objetivo é aprender a estruturar aplicações escaláveis e desacopladas, com foco em **boas práticas de arquitetura** e **organização de domínio**.

---

## 🎯 Objetivos do projeto

- Aplicar **DDD** na modelagem do domínio (entidades, agregados e repositórios).  
- Implementar **Clean Architecture** para separar responsabilidades.  
- Demonstrar o uso de **Entity Framework Core** como camada de persistência.  
- Organizar módulos de domínio em **Bounded Contexts** (ex: Contratos, Pedidos, Clientes).  
- Explorar boas práticas de **injeção de dependência**, **validação de domínio** e **mapeamento fluente**.

---

## 🏗️ Estrutura do projeto

```bash
src/
 ├─ ContractManagement.Api/              # Camada de apresentação (Controllers, DTOs, Swagger)
 ├─ ContractManagement.Application/      # Casos de uso (Services, Commands, Handlers)
 ├─ ContractManagement.Domain/           # Entidades e lógica de negócio
 ├─ ContractManagement.Infrastructure/   # Persistência, Repositórios, DbContext, Mapeamentos
