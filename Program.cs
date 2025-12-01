//using app.Db.Context;
//using app.Db.Uow;
//using app.Services.Common.Logger;
//using app.Services.PdfReport;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.FileProviders;
//using MigraDoc;
//using PdfSharp.Fonts;
//using PdfSharp.Quality;

//var builder = WebApplication.CreateBuilder();

////builder.WebHost.UseWebRoot("D:\\work\\datasheets");
//var connectionString = builder.Configuration.GetConnectionString("sql_oim");
//var logPath = builder.Configuration.GetSection("Logging:FilePath:Value").Get<string>();
//var pdfPath = builder.Configuration.GetSection("Pdf:DirPath").Get<string>();
//var allowedVisitHosts = builder.Configuration.GetSection("AllowedVisitHosts").Get<List<string>>()!.ToArray();

//GlobalFontSettings.FontResolver = new FontResolver(@"C:\work\web-api-ef\wwwroot\tnr.ttf");

////логирование
//var loggerFactory = LoggerFactory.Create(builder => {
//    builder.AddConsole();
//});

//loggerFactory.AddFile($"{logPath}");
//var logger = loggerFactory.CreateLogger("FileLogger");

////контекст базы данных
////builder.Services.AddDbContext<DataBase>(options =>
////{
////	options.UseSqlServer(connectionString);
////	options.UseLoggerFactory(loggerFactory);
////});

//builder.Services.AddDbContextFactory<DataBase>(options =>
//{
//	options.UseSqlServer(connectionString);
//	options.UseLoggerFactory(loggerFactory);
//});

//builder.Services.AddScoped<UnitOfWork1>();



////cors
//builder.Services.AddCors(options =>
//{
//	options.AddDefaultPolicy(policy =>
//	{
//		policy
//		.WithOrigins(allowedVisitHosts)
//		.AllowAnyMethod()
//		.AllowAnyHeader();
//	});
//});

////свагер
//builder.Services.AddMvcCore()
//		.AddApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddControllers(); // контроллеры без представлений
//var app = builder.Build();
//app.UseCors();
//app.UseSwagger();
//app.UseSwaggerUI(options =>
//{
//    options.SwaggerEndpoint("/swagger/v1/swagger.json", "api");
//});
//app.UseRouting();
//app.UseEndpoints(endpoints =>
//{
//	endpoints.MapControllers();
//});
//app.UseStaticFiles(new StaticFileOptions
//{
//	FileProvider = new PhysicalFileProvider(
//		   pdfPath
//		   ),
//	RequestPath = "/datasheets"
//});
//app.UseStaticFiles();
//app.MapGet("/", (HttpContext context) =>
//{
//    context.Response.Redirect("/swagger/index.html");
//});

//app.Run();




using app.Db.Context;
using app.Db.Uow;
using app.Services.Common.Logger;
using app.Services.Common.Ref;
using app.Services.Common.Ref.Models;
using app.Services.Component;
using app.Services.Component.Models;
using app.Services.ComponentService;
using app.Services.ComponentType;
using app.Services.Manufacturer;
using app.Services.PdfReport;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using PdfSharp.Fonts;

//строка подключений
var builder = WebApplication.CreateBuilder();

//builder.WebHost.UseWebRoot("D:\\work\\datasheets");
var connectionString = builder.Configuration.GetConnectionString("sql_oim");
var logPath = builder.Configuration.GetSection("Logging:FilePath:Value").Get<string>();
var pdfPath = builder.Configuration.GetSection("Pdf:DirPath").Get<string>();
var pdfDefaultFontPath = builder.Configuration.GetSection("Pdf:DefaultFont").Get<string>();
//var allowedVisitHosts = builder.Configuration.GetSection("AllowedVisitHosts").Get<List<string>>()!.ToArray();

GlobalFontSettings.FontResolver = new FontResolver(pdfDefaultFontPath);

//логирование
var loggerFactory = LoggerFactory.Create(builder => {
	builder.AddConsole();
});

loggerFactory.AddFile($"{logPath}");
var logger = loggerFactory.CreateLogger("FileLogger");

//контекст базы данных
//builder.Services.AddDbContext<DataBase>(options =>
//{
//	options.UseSqlServer(connectionString);
//	options.UseLoggerFactory(loggerFactory);
//});

builder.Services.AddDbContextFactory<DataBase>(options =>
{
	options.UseSqlServer(connectionString);
	options.UseLoggerFactory(loggerFactory);
});

builder.Services.AddScoped<UnitOfWork1>();
builder.Services.AddScoped<RefPropsSelected>();
builder.Services.AddScoped<RefDataModel>();
builder.Services.AddScoped<IRefHelper, RefHelper>();
builder.Services.AddScoped<ComponentDataModel>();
builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
builder.Services.AddScoped<IComponentTypeService, ComponentTypeService>();
builder.Services.AddScoped<IComponentService, ComponentService>();
builder.Services.AddScoped<IPdfReportService, PdfReportService>();



//cors
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy
		.WithOrigins("*")
		.AllowAnyMethod()
		.AllowAnyHeader();
	});
});

//свагер
builder.Services.AddMvcCore()
		.AddApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers(); // используем контроллеры без представлений
var app = builder.Build();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
	options.SwaggerEndpoint("/swagger/v1/swagger.json", "api");
});
app.UseRouting();
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});
app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(
		   pdfPath
		   ),
	RequestPath = "/datasheets"
});
app.UseStaticFiles();
app.MapGet("/", (HttpContext context) =>
{
	context.Response.Redirect("/swagger/index.html");
});

app.Run();



