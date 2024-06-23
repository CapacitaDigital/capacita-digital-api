using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using UserContext.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext para o Entity Framework Core com MySQL
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 4, 0)) // Substitua pela versão do seu servidor MySQL
    ));

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

// Configuração do CORS para permitir qualquer origem
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

// Habilita o CORS
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
