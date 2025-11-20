using Microsoft.Data.Sqlite;
using API_Product.Repositories;
using System.Data;
using Dapper;
using NuGet.DependencyResolver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();


// Configure Dapper and SQLite repository
builder.Services.AddTransient<IDbConnection>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new SqliteConnection(connectionString);
});

// Register the ProductRepository
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Create the SQLite database and table if it doesn't exist
if (app.Environment.IsDevelopment() && builder.Configuration.GetConnectionString("DefaultConnection").Contains(".db")) // Detecta si es un archivo SQLite
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var connection = services.GetRequiredService<IDbConnection>();
        try
        {
            connection.Open();
            string createTableSql = @"
                CREATE TABLE IF NOT EXISTS Products (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    Price REAL NOT NULL,
                    Category TEXT NOT NULL
                );";
            connection.Execute(createTableSql); // Requiere 'using Dapper;'
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while creating the SQLite DB.");
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
