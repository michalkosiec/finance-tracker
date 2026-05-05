using Api.Data;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepo, UserRepo>();

builder.Services.AddScoped<IBudgetRepo, BudgetRepo>();

builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();  

builder.Services.AddScoped<ITransactionRepo, TransactionRepo>();


builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();