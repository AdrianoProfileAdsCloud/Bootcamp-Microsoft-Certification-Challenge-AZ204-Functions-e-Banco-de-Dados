# Bootcamp - Microsoft Certification Challenge AZ204-Functions e ComosDB.
<br>

 # 📋  **Cenário:**
Criar série de Azure Functions para gerenciar catálogos similares aos da Netflix.

![alt text](<imagens/Arquitetura do Projeto.png>)

### 🔨  Provisionamento de Recurso no Azure:

## 🎯 Provisionar - Resource Group.

![alt text](<imagens/1 passo criar um resource group.png>)

## 🎯 Provisionar - Api Management

![alt text](<imagens/2 passo criar uma Api Managent.png>)

## 🎯 Provisionar - Banco de Dados NoSql CosmosDB.

![alt text](<imagens/3 passo criar um cosmos db.png>)


## 🎯 Provisionar - Storange Account.

![alt text](<imagens/4 passo Storange Account.png>)


<p>Foram criados quatro projetos do tipo Azure Functions, independentes, representando **Microsserviços** específicos para cada função.⁣</p>
 <p>Desta forma, cada um pode ser escalado individualmente conforme a demanda.⁣/p>
 <p>O que tmabém facilita o monitoramento e ratreabilidade de gargalos ou problemas nos serviços.</>

 <br>

## 📋 Configurando o ambiente.

 <br>

* Instalação de pacotes Necessários para executar este projeto localmente:
   - [X] npm install -g azure-functions-core-tools@4 --unsafe-perm true.
   - [X] dotnet add package Azure.Storage.Blobs.


* Adição de pacotes para Azure Function - **fnPostDatabase**
   - [X] dotnet add package Microsoft.Azure.Functions.Worker.Extensions.CosmosDB
   - [X] dotnet add package Newtonsoft.Json

* Adição de pacotes para Azure Function -  **fnGetMovieDetail**
   - [X] dotnet add package Microsoft.Azure.Cosmos
   - [X] dotnet add package Newtonsoft.Json   


## Client Rest (Postman)

EndPoint para Azure Function - **fnPostDataStorange(Post)**:
   - http://localhost:7071/api/dataStorage

  Reader tipo: file-type    Value: video
<br>

EndPoint para Azure Function - **fnPostDatabase(Post)**:
   - http://localhost:7071/api/movie

  Body Tipo: raw   Value: 
  
```json

  {
  "Title": "Teste2",
  "id": "12348",
  "year": "2011",
  "video": "https://staflixprj.blob.core.windows.net/video/2025-02-02 18-00-42.mkv",
   "thumb": "https://staflixprj.blob.core.windows.net/video/Arquitetura do Projeto.png"
}

```

<br>

EndPoint para Azure Function - **fnGetMovieDetail(Get)**:
   - http://localhost:7071/api/detail?id=12345
     
   Param: id   Value:12345

<br>

EndPoint para Azure Function - **fnGetAllMovies(Get)**:
   - http://localhost:7071/api/all
  












