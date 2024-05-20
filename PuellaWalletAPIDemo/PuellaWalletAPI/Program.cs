using PuellaWalletData.Data;
using PuellaWalletData.Repositories.Wallets;
using PuellaWalletData.Repositories.Users;
using PuellaWalletData.Repositories.Transactions;
using FluentValidation;
using PuellaWalletData.Models;
using PuellaWalletData.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IDbDataAccess, DbDataAccess>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();

//validations
builder.Services.AddScoped<IValidator<TransactionModel>, TransactionValidator>();
builder.Services.AddScoped<IValidator<UserModel>, UserValidator>();
builder.Services.AddScoped<IValidator<WalletModel>, WalletValidator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
