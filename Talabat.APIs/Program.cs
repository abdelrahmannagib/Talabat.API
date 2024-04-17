using Microsoft.EntityFrameworkCore;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using AutoMapper;

using Talabat.APIs.Dtos.Helpers;
namespace Talabat.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			
			
			var webApplicationBuilder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			#region Configure Srevice
			webApplicationBuilder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			webApplicationBuilder.Services.AddEndpointsApiExplorer();
			webApplicationBuilder.Services.AddSwaggerGen(); 

			webApplicationBuilder.Services.AddDbContext<StoreContext>(options=>
			{
				options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DeafultConnection"));
			});

			webApplicationBuilder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));

			webApplicationBuilder.Services.AddAutoMapper(typeof(MappingProfiles));

			#endregion

			var app = webApplicationBuilder.Build();

			using var scope=app.Services.CreateScope();
			var services= scope.ServiceProvider;
			var _dbContext = services.GetRequiredService<StoreContext>();

			var loggerFactory=services.GetRequiredService<ILoggerFactory>();
			try
			{
				await _dbContext.Database.MigrateAsync();
				await StoreContextSeed.SeedAsync(_dbContext);
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex,"Error tracked During apply migration");
                
            }
			//scope.Dispose();

			#region Configure Middlewares

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers(); 
			#endregion

			app.Run();
		}
	}
}
