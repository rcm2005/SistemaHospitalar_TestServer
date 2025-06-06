# Servidor de Teste Sync

Este projeto em .NET 8 implementa um servidor HTTP mínimo para receber requisições de sincronização de dados. Ele serve como back-end de teste para o módulo de sincronização do sistema C# (Sistema Hospitalar) — recebe um JSON via POST em `/sync`, imprime o conteúdo no console e retorna um JSON de confirmação.

---

## Estrutura de Pastas


ServidorTesteSync/
├─ ServidorTesteSync.csproj
└─ Program.cs


- **ServidorTesteSync.csproj**  
  Define como um projeto ASP.NET Core (.NET 8).
- **Program.cs**  
  Contém o código principal que:  
  1. Mapeia o endpoint `POST /sync`.  
  2. Lê todo o corpo da requisição, tenta fazer “pretty print” se for JSON, imprime no console.  
  3. Retorna um objeto `{ "status": "sincronizado", "timestamp": "<data_hora>" }`.  
  4. Configura a URL de escuta `http://localhost:6000`.

---

## Pré-requisitos

- **.NET 8 SDK** instalado localmente.  
- Qualquer editor/IDE compatível com .NET 8 (Visual Studio 2022, Visual Studio Code, Rider).

---

## Instruções de Instalação e Execução

1. **Clone ou extraia este repositório/zip**  
   Se você baixou o ZIP, basta descompactar e entrar na pasta:

   cd ServidorTesteSync
 

2. **Restaurar pacotes NuGet**  

  dotnet restore



4. **Executar o servidor**  

  dotnet run

  Ao rodar, no console, será exibida a mensagem:

  Servidor de Teste Sync rodando em http://localhost:6000


  Isso indica que o servidor está escutando requisições HTTP em `http://localhost:6000`.

---

## Como Testar

### 1. Endpoint de Sincronização

- **URL**: `http://localhost:6000/sync`  
- **Método**: `POST`  
- **Header**: `Content-Type: application/json`  
- **Corpo**: qualquer JSON válido (por exemplo, ambiente C# envia `{ "Timestamp": "...", "Pacientes": [...], "Logs": [...] }`).

### 2. Exemplo via cURL


curl -X POST http://localhost:6000/sync      -H "Content-Type: application/json"      -d '{
           "Timestamp": "2025-06-06T10:30:00",
           "Pacientes": [
             { "Nome": "João", "CPF": "12345678901", "Idade": 30, "Diagnostico": "Teste" }
           ],
           "Logs": [
             { "Mensagem": "[Dados]", "Horario": "2025-06-06T10:29:00", "Tipo": "INFO" }
           ]
         }'


- No console do servidor, ele imprimirá:


  [ServidorTesteSync] JSON recebido (formatado):
  {
    "Timestamp": "2025-06-06T10:30:00",
    "Pacientes": [
      {
        "Nome": "João",
        "CPF": "12345678901",
        "Idade": 30,
        "Diagnostico": "Teste"
      }
    ],
    "Logs": [
      {
        "Mensagem": "[Dados]",
        "Horario": "2025-06-06T10:29:00",
        "Tipo": "INFO"
      }
    ]
  }


- E responderá ao cliente com:
  ```json
  {
    "status": "sincronizado",
    "timestamp": "2025-06-06T10:30:05.1234567"
  }
  ```

---

## Observações

- Para alterar a porta, edite em `Program.cs` a linha:

  app.Urls.Clear();
  app.Urls.Add("http://localhost:6000");

  e modifique para a porta desejada (por exemplo, `http://localhost:5001`).

- Se você estiver testando localmente com o cliente C#, garanta que o `Sincronizador.UrlSync` aponte para `http://localhost:6000/sync`.

- Este servidor não faz validações de esquema JSON — ele apenas tenta desserializar para impressões mais legíveis. Se o corpo não for JSON válido, mostrará como “não-JSON ou inválido”.

---

## Licença

Este projeto de teste está disponível para uso acadêmico/simples. Sinta-se livre para modificar conforme suas necessidades.
