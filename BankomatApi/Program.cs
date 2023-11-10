using BankomatApi.Repositories;
using BankomatSimulator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddDbContext<EsercitazioneBancaEntities>(cfg =>
//{
//    //alla classe che si occupa di collegarsi al db

//    var connectionString = "Server=B80MI-SCM117;Database=EsercitazioneBanca;Trusted_Connection=True;";
//    ServerVersion sv = ServerVersion.AutoDetect(connectionString);
//    var serverVersion = new MySqlServerVersion(sv.Version);


//    cfg.UseMySql(connectionString, serverVersion).
//    LogTo(Console.WriteLine, LogLevel.Information)
//    .EnableSensitiveDataLogging()
//    .EnableDetailedErrors();
//});

builder.Services.AddDbContext<EsercitazioneBancaEntities>(x => x.UseSqlServer("Server=B80MI-SCM117;Database=EsercitazioneBanca;Trusted_Connection=True;"));
// Add services to the container.


builder.Services.AddScoped<IDbRepository, DbRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();


Console.ReadKey();
