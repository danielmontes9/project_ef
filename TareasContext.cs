using Microsoft.EntityFrameworkCore;
using project_ef.Models;

namespace project_ef;

public class TareasContext: DbContext
{

	public DbSet<Categoria> Categorias {get;set;}

	public DbSet<Tarea> Tareas {get;set;}

	public TareasContext(DbContextOptions<TareasContext> options): base (options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		List<Categoria> categoriasInit = new List<Categoria>();
		categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("c4e0d0e7-5f06-48c7-9246-11fe12f2c100"), Nombre = "Actividades pendientes", Peso = 20 } );
		categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("c4e0d0e7-5f06-48c7-9246-11fe12f2c657"), Nombre = "Actividades personales", Peso = 50 } );

		modelBuilder.Entity<Categoria>(categoria => {

			categoria.ToTable("Categoria");
			categoria.HasKey(p => p.CategoriaId);

			categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);

			categoria.Property(p => p.Descripcion).IsRequired(false);

			categoria.Property(p => p.Peso);

			categoria.HasData(categoriasInit);

		});


		List<Tarea> tareasInit = new List<Tarea>();
		tareasInit.Add(new Tarea() { TareaId = Guid.Parse("c4e0d0e7-5f06-48c7-9246-11fe12f2c101"), CategoriaId = Guid.Parse("c4e0d0e7-5f06-48c7-9246-11fe12f2c100"), PrioridadTarea = Prioridad.Media, Titulo = "Pago de servicios públicos", FechaCreacion = DateTime.UtcNow } );
		tareasInit.Add(new Tarea() { TareaId = Guid.Parse("c4e0d0e7-5f06-48c7-9246-11fe12f2c602"), CategoriaId = Guid.Parse("c4e0d0e7-5f06-48c7-9246-11fe12f2c657"), PrioridadTarea = Prioridad.Baja, Titulo = "Terminar de ver pelicula en Netflix", FechaCreacion = DateTime.UtcNow } );

		modelBuilder.Entity<Tarea>(tarea => {

			tarea.ToTable("Tarea");
			tarea.HasKey(p => p.TareaId);

			tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);

			tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);

			tarea.Property(p => p.Descripcion).IsRequired(false);

			tarea.Property(p => p.PrioridadTarea);

			tarea.Property(p => p.FechaCreacion);

			tarea.Ignore(p => p.Resumen);

			tarea.HasData(tareasInit);

		});
	}

}