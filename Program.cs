using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/sync", async (HttpContext http) =>
{
    using var reader = new StreamReader(http.Request.Body);
    var corpo = await reader.ReadToEndAsync();

    try
    {
        var jsonDoc = JsonDocument.Parse(corpo);
        var pretty = JsonSerializer.Serialize(jsonDoc, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine("[ServidorTesteSync] JSON recebido (formatado):");
        Console.WriteLine(pretty);
    }
    catch
    {
        Console.WriteLine("[ServidorTesteSync] Corpo recebido (não-JSON ou inválido):");
        Console.WriteLine(corpo);
    }

    return Results.Json(new { status = "sincronizado", timestamp = DateTime.Now });
});

app.Urls.Clear();
app.Urls.Add("http://localhost:6000");

Console.WriteLine("Servidor de Teste Sync rodando em http://localhost:6000");
app.Run();
