using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class fnPostDataStorage
    {
        private readonly ILogger<fnPostDataStorage> _logger;

        public fnPostDataStorage(ILogger<fnPostDataStorage> logger)
        {
            _logger = logger;
        }

        [Function("dataStorage")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            _logger.LogInformation("Processando imagem no Storage...");

            // ✅ Verifica se o cabeçalho "file-type" está presente
            if (!req.Headers.TryGetValue("file-type", out var fileTypeHeader))
            {
                return new BadRequestObjectResult("O cabeçalho 'file-type' é obrigatório.");
            }
            string fileType = fileTypeHeader.ToString();

            // ✅ Obtém o arquivo enviado
            var form = await req.ReadFormAsync();
            var file = form.Files["file"];

            if (file == null || file.Length == 0)
            {
                return new BadRequestObjectResult("O arquivo não foi enviado.");
            }

            // ✅ Obtém a conexão do Azure Storage do ambiente
            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            string containerName = fileType.ToLower(); // Nome do container baseado no "file-type"

            // ✅ Cria o cliente do contêiner
            var containerClient = new BlobContainerClient(connectionString, containerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            // ✅ Cria o cliente do blob
            string blobName = file.FileName;
            var blobClient = containerClient.GetBlobClient(blobName);

            // ✅ Faz o upload do arquivo
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            _logger.LogInformation($"Arquivo {file.FileName} enviado com sucesso.");

            return new OkObjectResult(new
            {
                Message = "Arquivo enviado com sucesso!",
                BlobUri = blobClient.Uri.ToString()
            });
        }
    }
}
