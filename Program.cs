using app.Context;
using app.db;
using app.Logger;
using app.PdfReport;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MigraDoc;
using PdfSharp.Fonts;
using PdfSharp.Quality;

//строка подключений
var builder = WebApplication.CreateBuilder();

//builder.WebHost.UseWebRoot("D:\\work\\datasheets");
var connectionString = builder.Configuration.GetConnectionString("sql_oim");
var logPath = builder.Configuration.GetSection("Logging:FilePath:Value").Get<string>();
var pdfPath = builder.Configuration.GetSection("Pdf:DirPath").Get<string>();
var allowedVisitHosts = builder.Configuration.GetSection("AllowedVisitHosts").Get<List<string>>()!.ToArray();

GlobalFontSettings.FontResolver = new FontResolver(@"C:\work\web-api-ef\wwwroot\tnr.ttf");

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

builder.Services.AddScoped<UnitOfWork>();



//cors
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy
		.WithOrigins(allowedVisitHosts)
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

