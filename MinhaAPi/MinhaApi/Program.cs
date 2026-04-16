using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Lista em memória
var produtos = new List<Produto>();

// ENDPOINTS

// Criar produto
app.MapPost("/produtos", (Produto produto) =>
{
    produtos.Add(produto);
    return Results.Created($"/produtos/{produto.Id}", produto);
});

// Listar todos
app.MapGet("/produtos", () =>
{
    return produtos;
});

// Buscar por ID
app.MapGet("/produtos/{id}", (int id) =>
{
    var produto = produtos.FirstOrDefault(p => p.Id == id);

    if (produto == null)
        return Results.NotFound();

    return Results.Ok(produto);
});

// Atualizar
app.MapPut("/produtos/{id}", (int id, Produto produtoAtualizado) =>
{
    var produto = produtos.FirstOrDefault(p => p.Id == id);

    if (produto == null)
        return Results.NotFound();

    produto.Nome = produtoAtualizado.Nome;
    produto.Preco = produtoAtualizado.Preco;

    return Results.Ok(produto);
});

// Deletar
app.MapDelete("/produtos/{id}", (int id) =>
{
    var produto = produtos.FirstOrDefault(p => p.Id == id);

    if (produto == null)
        return Results.NotFound();

    produtos.Remove(produto);
    return Results.NoContent();
});

app.Run();

// MODELO (SEMPRE NO FINAL)

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public decimal Preco { get; set; }
}