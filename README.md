# Pet Travel Insurance API

Uma API RESTful desenvolvida em .NET 8 para gerenciar seguros de viagem para pets, permitindo o controle de tutores, pets, planos de seguro e contratos.

## 📋 Funcionalidades

- **Gestão de Tutores**: CRUD completo para tutores de pets
- **Gestão de Pets**: Cadastro e gerenciamento de pets vinculados aos tutores
- **Planos de Seguro**: Gerenciamento de planos de seguro para pets
- **Contratos**: Criação e gestão de contratos de seguro entre tutores, pets e planos
- **Validações**: Regras de negócio para garantir integridade dos dados

## 🛠️ Tecnologias Utilizadas

- **.NET 8**: Framework principal
- **ASP.NET Core Web API**: Para criação da API REST
- **Entity Framework Core 9.0.9**: ORM para acesso ao banco de dados
- **SQL Server**: Banco de dados relacional
- **Swagger/OpenAPI**: Documentação automática da API
- **Repository Pattern**: Padrão para acesso aos dados
- **Service Layer**: Camada de serviços com regras de negócio

## 🏗️ Arquitetura

## Diagrama de Arquitetura
![Diagrama Azure](./docs/azure-diagram.png)

## Componentes Azure que podem ser Utilizados

### 1. Azure API Management
- Porta de entrada para todas as requisições da API.
- Aplica políticas de segurança, autenticação e limite de requisições.
- Facilita versionamento e exposição controlada da API.

### 2. Azure Active Directory
- Responsável pela autenticação e autorização dos usuários.
- Garante acesso seguro via tokens OAuth2/JWT.

### 3. Azure App Service (API C# .NET)
- Hospeda a API desenvolvida em C# com .NET.
- Serviço PaaS que garante escalabilidade e alta disponibilidade.
- Integração nativa com Key Vault e SQL Database.

### 4. Azure SQL Database
- Banco relacional compatível com SQL Server.
- Armazena tutores, pets, planos e contratos.
- Oferece backups automáticos e escalabilidade.

### 5. Azure Key Vault
- Armazena credenciais, secrets e strings de conexão.
- Evita exposição de senhas no código.

### 6. Azure Monitor / Application Insights
- Monitora performance da aplicação e coleta logs.
- Detecta falhas, gargalos e gera alertas em tempo real.

---

## Fluxo Resumido
1. O usuário envia requisições → **API Management**.  
2. O **Azure AD** autentica e autoriza o usuário.  
3. O **App Service** processa a lógica da API.  
4. Dados são armazenados e lidos do **SQL Database**.  
5. Segredos da aplicação vêm do **Key Vault**.  
6. Toda a operação é monitorada pelo **App Insights**.  

O projeto segue uma arquitetura em camadas:

```
PetTravelInsurance/
├── Controllers/          # Controladores da API
├── Models/              # Entidades do domínio
├── DTO/                 # Data Transfer Objects
├── Services/            # Camada de serviços (regras de negócio)
├── Repositories/        # Camada de acesso aos dados
├── Data/               # Contexto do banco e configurações
└── Migrations/         # Migrações do Entity Framework
```

## 📊 Modelo de Dados

### Entidades Principais

- **Tutor**: Responsável pelos pets
  - Nome, Email (único), Telefone
- **Pet**: Animal de estimação
  - Nome, Raça, Idade, TutorId (FK)
- **PlanoPet**: Planos de seguro disponíveis
  - Nome, Preço, Cobertura, Descrição, Status (Ativo/Inativo)
- **Contrato**: Contrato de seguro
  - Relaciona Tutor, Pet e Plano
  - Datas de início e fim
  - Informações do plano contratado

## 🚀 Como Executar

### Pré-requisitos

- .NET 8 SDK
- SQL Server ou SQL Server Express
- Visual Studio 2022 ou VS Code

### Passos para Execução

1. **Clone o repositório**
   ```bash
   git clone <url-do-repositorio>
   cd PetTravelInsurance
   ```

2. **Configure a string de conexão**
   
   Edite o arquivo `appsettings.json` com sua string de conexão do SQL Server:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=SEU_SERVIDOR;Database=PetTravelInsuranceDB;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```

3. **Execute as migrações**
   ```bash
   dotnet ef database update
   ```

4. **Execute a aplicação**
   ```bash
   dotnet run
   ```

5. **Acesse a documentação**
   
   A API estará disponível em `https://localhost:7103` ou `http://localhost:5284`
   
   Documentação Swagger: `https://localhost:7103/swagger`

## 📚 Endpoints da API

### Tutores (`/api/tutores`)
- `GET /api/tutores` - Lista todos os tutores
- `GET /api/tutores/{id}` - Busca tutor por ID
- `POST /api/tutores` - Cria novo tutor
- `PUT /api/tutores/{id}` - Atualiza tutor
- `DELETE /api/tutores/{id}` - Remove tutor

### Pets (`/api/pets`)
- `GET /api/pets` - Lista todos os pets
- `GET /api/pets/{id}` - Busca pet por ID
- `POST /api/pets` - Cria novo pet
- `PUT /api/pets/{id}` - Atualiza pet
- `DELETE /api/pets/{id}` - Remove pet

### Planos (`/api/planos`)
- `GET /api/planos` - Lista todos os planos
- `GET /api/planos/{id}` - Busca plano por ID
- `GET /api/planos/ativos` - Lista apenas planos ativos
- `POST /api/planos` - Cria novo plano
- `PUT /api/planos/{id}` - Atualiza plano
- `DELETE /api/planos/{id}` - Remove plano

### Contratos (`/api/contratos`)
- `GET /api/contratos` - Lista todos os contratos
- `GET /api/contratos/{id}` - Busca contrato por ID
- `GET /api/contratos/tutor/{tutorId}` - Lista contratos por tutor
- `POST /api/contratos` - Cria novo contrato
- `PUT /api/contratos/{id}` - Atualiza contrato
- `DELETE /api/contratos/{id}` - Remove contrato

## 💡 Exemplos de Uso

### Criando um Tutor
```json
POST /api/tutores
{
  "nome": "João Silva",
  "email": "joao@email.com",
  "telefone": "(11) 99999-9999"
}
```

### Criando um Pet
```json
POST /api/pets
{
  "nome": "Rex",
  "raca": "Labrador",
  "idade": "3 anos",
  "tutorId": 1
}
```

### Criando um Contrato
```json
POST /api/contratos
{
  "tutorId": 1,
  "petId": 1,
  "planoPetId": 1,
  "dataInicio": "2024-01-01",
  "dataFim": "2024-12-31",
  "planoNome": "Plano Premium",
  "planoPreco": 299.90,
  "planoCobertura": "Cobertura completa para viagens internacionais"
}
```

## 🔒 Validações

### Regras de Negócio Implementadas

- **Tutor**: Nome e email obrigatórios, email único
- **Pet**: Nome e raça obrigatórios
- **Plano**: Nome, preço (> 0) e cobertura obrigatórios
- **Contrato**: 
  - Data fim deve ser posterior à data início
  - Data início não pode ser no passado
  - Tutor, Pet e Plano devem existir

## 🧪 Testes

Para testar a API, você pode usar:
- **Swagger UI**: Interface web automática
- **Postman**: Importar a collection do Swagger
