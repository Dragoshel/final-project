using FinalProject.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEngine(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    builder.Configuration["DATA SOURCE"],
    builder.Configuration["INITIAL CATALOG"],
    builder.Configuration["USER ID"],
    builder.Configuration["PASSWORD"]
);
builder.Services.AddRepos();
builder.Services.AddServices();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.SeedDatabaseFromFiles("Data/SQL");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();
