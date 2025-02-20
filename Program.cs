using app.Context;
using app.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

//строка подключений
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IDE");
var logPath = builder.Configuration["Logging:FilePath:Value"];

//логирование
var loggerFactory = LoggerFactory.Create(builder => {
    builder.AddConsole();
});

loggerFactory.AddFile(logPath);
var logger = loggerFactory.CreateLogger("FileLogger");

//контекст базы данных
builder.Services.AddDbContext<DataBase>(options =>
{
	options.UseSqlServer(connectionString);
	options.UseLoggerFactory(loggerFactory);
});


//cors
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy
		.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader();
	});
});

//свагер
builder.Services.AddMvcCore()
		.AddApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();
app.UseCors();


app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "api");
});




app.MapGet("/", (HttpContext context) =>
{
    context.Response.Redirect("/swagger/index.html");
    //await TestSQL(context);
});


app.MapGet("/api/resistors", async (DataBase db, HttpContext context) =>
{
    var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = db.Resistors.ToList();
    response.ContentType = "application/json";
    await response.WriteAsJsonAsync(items);
});

app.MapGet("/api/transistors", async (DataBase db, HttpContext context) =>
{
    var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = db.Transistors.ToList();
    response.ContentType = "application/json";
    await response.WriteAsJsonAsync(items);
});

app.MapGet("/api/diods", async (DataBase db, HttpContext context) =>
{
    var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = db.Diods.ToList();
    response.ContentType = "application/json";
    await response.WriteAsJsonAsync(items);
});

app.MapGet("/api/microchips", async (DataBase db, HttpContext context) =>
{
    var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = db.Microchips.ToList();
    response.ContentType = "application/json";
    await response.WriteAsJsonAsync(items);
});

app.MapGet("/api/capacitors", async (DataBase db, HttpContext context) =>
{
    var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = db.Capacitors.ToList();
    response.ContentType = "application/json";
    await response.WriteAsJsonAsync(items);
});

app.MapGet("/api/microchips/bitdepthvalue", async (DataBase db, HttpContext context, string? manufacturername, string? bitdepthvalue) =>
{
	var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = db.Microchips.Select(m => new { m.Manufacturer.ManufacturerName, m.BitDepthValue, m.ComponentName });
	if(!manufacturername.IsNullOrEmpty())
	{
		items = items.Where(m => m.ManufacturerName == manufacturername);
	}
	if(!bitdepthvalue.IsNullOrEmpty())
	{
		items = items.Where(m => m.BitDepthValue == bitdepthvalue);
	}
	response.ContentType = "application/json";
	await response.WriteAsJsonAsync(items.ToList());
});


//async Task TestSQL(DataBase db, HttpContext context) {

//    var response = context.Response;
//    //var items = db.ComponentKinds.ToList();
//    //var items = db.ComponentTypes.ToList();
//    //var items = db.Manufacturers.ToList();
//    //var items = db.Technologies.ToList();
//    //var items = db.Resistors.ToList();
//    //var items = db.Transistors.ToList();
//    //var items = db.Diods.ToList();
//    //var items = db.Capacitors.ToList();

//    context.Response.ContentType = "application/json";
//    await context.Response.WriteAsJsonAsync(items);

//}


app.Run();

