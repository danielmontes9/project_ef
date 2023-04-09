using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_ef;
using project_ef.Models;

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
	//GET con condición
	//return Results.Ok(dbContext.Tareas.Include(p => p.Categoria).Where(p => p.PrioridadTarea == project_ef.Models.Prioridad.Baja));
	return Results.Ok(dbContext.Tareas.Include(p => p.Categoria));
});


app.MapPost("/api/tareas", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea) => 
{

	tarea.TareaId = Guid.NewGuid();
	tarea.FechaCreacion = DateTime.UtcNow;

	await dbContext.AddAsync(tarea);
	//await dbContext.Tareas.AddAsync(tarea);

	await dbContext.SaveChangesAsync();

	return Results.Ok();

});


app.MapPut("/api/tareas/{id}", async ([FromServices] TareasContext dbContext,[FromBody] Tarea tarea, [FromRoute] Guid id) => 
{

	var tareaActual = dbContext.Tareas.Find(id);

	if(tareaActual != null) {
		tareaActual.CategoriaId = tarea.CategoriaId;
		tareaActual.Titulo = tarea.Titulo;
		tareaActual.Descripcion = tareaActual.Descripcion;
		tareaActual.PrioridadTarea = tarea.PrioridadTarea;

		await dbContext.SaveChangesAsync();

		return Results.Ok();
	}

	return Results.NotFound();


});

app.Run();
