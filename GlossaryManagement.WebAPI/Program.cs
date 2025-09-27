using Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<GlossaryDbContext>(options=>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope()) // i u produkciji
{
    var context = scope.ServiceProvider.GetRequiredService<GlossaryDbContext>();
    
    await context.Database.EnsureCreatedAsync();
    
    await DataSeeder.SeedData(context);
}


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
