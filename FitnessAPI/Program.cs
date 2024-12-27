using FitnessAPI.Data;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using FitnessAPI.Services;
using FitnessAPI.Client;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from the .env file
Env.Load();

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("FitnessDatabase")));

builder.Services.AddHttpClient<IFitnessApiClient, FitnessApiClient>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
