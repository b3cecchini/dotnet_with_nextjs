using MongoDB.Driver;
using MongoDB.Entities;
using svc_search;
using svc_search.Data;
using svc_search.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddHttpClient<AuctionClient>();
var app = builder.Build();


// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();
app.MapControllers();
try
{
    await DbInitializer.InitDb(app);
}
catch( Exception e)
{
    Console.WriteLine("*** ERROR IN DATABASE INITIALIZTION ***");
    Console.WriteLine(e);
}


app.Run();

