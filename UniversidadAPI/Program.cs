using Microsoft.EntityFrameworkCore;
using UniversidadAPI.Data;
using UniversidadAPI.Services.Interfaces;
using UniversidadAPI.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Configuración de AutoMapper 
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Configuración con el conector Mysql
builder.Services.AddDbContext<UniversidadContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 32)), // Ajusta según tu versión de MySQL
        mysqlOptions =>
        {
            mysqlOptions.EnableRetryOnFailure(
                 maxRetryCount: 5,
                 maxRetryDelay: TimeSpan.FromSeconds(30),
                 errorNumbersToAdd: null);
        }));

// Configuración del HttpClient sin validación SSL (para desarrollo) 
builder.Services.AddHttpClient("NoSSL").ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        ClientCertificateOptions = ClientCertificateOption.Manual,
        ServerCertificateCustomValidationCallback = (_, _, _, _) => true
    };
});

// Registrar servicios con sus interfaces(Aqui va Estudinate, Materia etc..)
builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<IProfesorService, ProfesorService>();
builder.Services.AddScoped<IMateriaService, MateriaService>();
builder.Services.AddScoped<IProfesorMateriaService, ProfesorMateriaService>();
builder.Services.AddScoped<IInscripcionService, InscripcionService>();


// Configuración de Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



// En app.UseCors()
app.UseCors("AllowAngularDev");

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//// Endpoint de verificación
//app.MapGet("/api/db-status", async (UniversidadContext db) =>
//{
//    try
//    {
//        var canConnect = await db.Database.CanConnectAsync();
//        return Results.Ok(new
//        {
//            Success = true,
//            Status = "Database connection successful",
//            Database = db.Database.GetDbConnection().Database,
//            Server = db.Database.GetDbConnection().DataSource,
//            IsConnected = canConnect,
//            Tables = canConnect ? await db.Database.GetAppliedMigrationsAsync() : null
//        });
//    }
//    catch (Exception ex)
//    {
//        return Results.Problem(
//            title: "Database connection failed",
//            detail: ex.Message,
//            statusCode: 500);
//    }
//});

app.Run();
