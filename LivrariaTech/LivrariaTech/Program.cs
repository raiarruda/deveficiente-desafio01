using FluentValidation;
using LivrariaTech;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<LivrariaBaseDados>(options => options.UseInMemoryDatabase("autores"));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IValidator<AutorModel>, AutorModelValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapPost("/autores", async (AutorModel autor, LivrariaBaseDados db, IValidator<AutorModel> validacao) =>
{
    var resultadoValidacao = await validacao.ValidateAsync(autor);
    if (!resultadoValidacao.IsValid)
    {
        return Results.BadRequest(resultadoValidacao.Errors.Select(e => e.ErrorMessage));
    }

    await db.Autor.AddAsync(autor);
    await db.SaveChangesAsync();

    return Results.Created($"autorAtualizado/{autor.Id}", autor);
}
)
.WithOpenApi();

app.MapGet("/autores", async (LivrariaBaseDados db) => await db.Autor.ToListAsync()).WithOpenApi();


app.MapGet("/autores/{id}", async (LivrariaBaseDados db, int id) => await db.Autor.FindAsync(id)).WithOpenApi();

app.MapPut("/autores/{id}", async (LivrariaBaseDados db, AutorModel autorAtualizado, IValidator < AutorModel > validacao, int id) =>
{
    var autor = await db.Autor.FindAsync(id);
    if (autor is null) return Results.NotFound();
    autor.Nome = autorAtualizado.Nome ?? autor.Nome;
    autor.Email = autorAtualizado.Email ?? autor.Email;
    autor.Descricao = autorAtualizado.Descricao ?? autor.Descricao;
    autor.DataAtualizacao = DateTime.Now;
    var resultadoValidacao = await validacao.ValidateAsync(autor);

    if (!resultadoValidacao.IsValid)
    {
        return Results.BadRequest(resultadoValidacao.Errors.Select(e => e.ErrorMessage));
    }
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();

