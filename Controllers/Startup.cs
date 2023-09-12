using APIAnnuaire;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Ajoutez le service de base de données pour SQL Server avec la chaîne de connexion
        services.AddDbContext<APIDbContext>(options =>
            options.UseSqlite("Data Source=data.db"));


        // Autres configurations et services...
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            // Configuration pour l'environnement de production
            // ...
        }

        // Le code suivant doit être ajouté ici
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<APIDbContext>();
            dbContext.Database.EnsureCreated();
        }

        // Configuration des endpoints
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }


    // Autres méthodes de configuration...
}
