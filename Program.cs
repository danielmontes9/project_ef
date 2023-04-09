using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_ef;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDb"));
builder.Services.AddNpgsql<TareasContext>(builder.Configuration.GetConnectionString("cnTareas"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext) => 
{
	dbContext.Database.EnsureCreated();
	return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());

});


app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContext) => 
{
	return Results.Ok(dbContext.Tareas.Include(p => p.Categoria).Where(p => p.PrioridadTarea == project_ef.Models.Prioridad.Baja));
});

app.Run();
