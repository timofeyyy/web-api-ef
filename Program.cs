using app.Context;
using app.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using app.Entities;
using System.Text.Json;
using Microsoft.Extensions.FileProviders;

//ñòğîêà ïîäêëş÷åíèé
var builder = WebApplication.CreateBuilder();

//builder.WebHost.UseWebRoot("D:\\work\\datasheets");
var connectionString = builder.Configuration.GetConnectionString("sql_oim");
var logPath = builder.Configuration["Logging:FilePath:Value"];
var pdfPath = builder.Configuration["Pdf:DirPath"];
//var alliasPath = builder.Configuration["Allias"];

//ëîãèğîâàíèå
var loggerFactory = LoggerFactory.Create(builder => {
    builder.AddConsole();
});

loggerFactory.AddFile(logPath);
var logger = loggerFactory.CreateLogger("FileLogger");

//êîíòåêñò áàçû äàííûõ
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

//ñâàãåğ
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

app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(
		   pdfPath
		   ),
	RequestPath = "/datasheets"
});
//app.UseStaticFiles();


app.MapGet("/", (HttpContext context) =>
{
    context.Response.Redirect("/swagger/index.html");
});
app.MapGet("/api/components", async (DataBase db, HttpContext context, string? ruComponentType, string? ruComponentKind, string? manufacturerName) =>
{
	var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = db.ComponentTypes
	.SelectMany(c => db.Microchips
		.Where(m => m.Type.RuComponentType == c.RuComponentType)
		.Select(m => new
		{
			m.Manufacturer.ManufacturerName,
			m.Kind.RuComponentKind,
			m.Kind.EnComponentKind,
			m.Type.RuComponentType,
			m.Type.EnComponentType,
			m.ComponentName
		}))
	.Concat(db.Transistors
		.Select(t => new
		{
			t.Manufacturer.ManufacturerName,
			t.Kind.RuComponentKind,
			t.Kind.EnComponentKind,
			t.Type.RuComponentType,
			t.Type.EnComponentType,
			t.ComponentName
		}))
	.Concat(db.Resistors
		.Select(r => new
		{
			r.Manufacturer.ManufacturerName,
			r.Kind.RuComponentKind,
			r.Kind.EnComponentKind,
			r.Type.RuComponentType,
			r.Type.EnComponentType,
			r.ComponentName
		}))
	.Concat(db.Capacitors
		.Select(ca => new
		{
			ca.Manufacturer.ManufacturerName,
			ca.Kind.RuComponentKind,
			ca.Kind.EnComponentKind,
			ca.Type.RuComponentType,
			ca.Type.EnComponentType,
			ca.ComponentName
		}))
	.Concat(db.Diods
		.Select(d => new
		{
			d.Manufacturer.ManufacturerName,
			d.Kind.RuComponentKind,
			d.Kind.EnComponentKind,
			d.Type.RuComponentType,
			d.Type.EnComponentType,
			d.ComponentName
		}));

	if (ruComponentType != null)
	{
		items = items.Where(t => t.RuComponentType == ruComponentType);
	}
	if (ruComponentKind != null)
	{
		items = items.Where(t => t.RuComponentKind == ruComponentKind);
	}
	if (manufacturerName != null)
	{
		items = items.Where(t => t.ManufacturerName == manufacturerName);
	}
	
	items.ToList();

	response.ContentType = "application/json";
	await response.WriteAsJsonAsync(items);
});

app.MapGet("/api/resistors", async (DataBase db, HttpContext context, string? componentName) =>
{
    var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = db.Resistors
	.Select(r => new
	{
		r.ID,
		r.DocID,
		r.Kind.RuComponentKind,
		r.Kind.EnComponentKind,
		r.Type.RuComponentType,
		r.Type.EnComponentType,
		r.Manufacturer.ManufacturerName,
		r.PowerRating,
		r.ComponentName,
		r.MinVoltage,
		r.MaxVoltage,
		r.MinRatedResistance,
		r.MaxRatedResistance,
		r.ResistanceTolerance,
		r.MinOperatingTemperature,
		r.MaxOperatingTemperature,
		r.CurrentLimit,
		r.QualicationSG,
		r.QualicationÅÑ,
		r.Package,
		r.Remark1,
		r.Remark2
	});
	if (!componentName.IsNullOrEmpty())
	{
		items = items.Where(r => r.ComponentName == componentName);
	}

    response.ContentType = "application/json";
    await response.WriteAsJsonAsync(items.ToList());
});

app.MapGet("/api/transistors", async (DataBase db, HttpContext context, string? componentName) =>
{
    var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = db.Transistors
	.Select(t => new
	{
		t.ID,
		t.DocID,
		t.Kind.RuComponentKind,
		t.Kind.EnComponentKind,
		t.Type.RuComponentType,
		t.Type.EnComponentType,
		t.Manufacturer.ManufacturerName,
		t.MaxPermissibleDCVoltage,
		t.ComponentName,
		t.MinOperatingTemperature,
		t.MaxOperatingTemperature,
		t.MaxPermissibleDCCollectorCurrent,
		t.RadiationResistance,
		t.RadiationResistanceI,
		t.QualicationSG,
		t.QualicationÅÑ,
		t.Package,
		t.Remark1,
		t.Remark2
	});
	if (!componentName.IsNullOrEmpty())
	{
		items = items.Where(r => r.ComponentName == componentName);
	}
	response.ContentType = "application/json";
    await response.WriteAsJsonAsync(items.ToList());
});

app.MapGet("/api/diods", async (DataBase db, HttpContext context, string? componentName) =>
{
    var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = db.Diods
	.Select(d => new {
		d.ID,
		d.DocID,
		d.Kind.RuComponentKind,
		d.Kind.EnComponentKind,
		d.Type.RuComponentType,
		d.Type.EnComponentType,
		d.Manufacturer.ManufacturerName,
		d.MaxPermissibleDCVoltage,
		d.ComponentName,
		d.MinOperatingTemperature,
		d.MaxOperatingTemperature,
		d.MaxPermissibleAverageDirectCurrent,
		d.MaxiPermissibleDirectCurrent,
		d.RadiationResistance,
		d.RadiationResistanceI,
		d.QualicationSG,
		d.QualicationÅÑ,
		d.Package,
		d.Remark1,
		d.Remark2
	});
	if (!componentName.IsNullOrEmpty())
	{
		items = items.Where(r => r.ComponentName == componentName);
	}
	response.ContentType = "application/json";
	await response.WriteAsJsonAsync(items.ToList());
});

app.MapGet("/api/microchips", async (DataBase db, HttpContext context, string? componentName) =>
{
    var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = db.Microchips
	.Select(m => new
	{
		m.ID,
		m.DocID,
		m.Kind.RuComponentKind,
		m.Kind.EnComponentKind,
		m.Type.RuComponentType,
		m.Type.EnComponentType,
		m.Manufacturer.ManufacturerName,
		m.BitDepthValue,
		m.ComponentName,
		m.ConsumptionCurrent,
		m.Interfaces,
		m.MinVoltage,
		m.MaxVoltage,
		m.Frequency,
		m.Technology.EnTechnologyName,
		m.Technology.RuTechnologyName,
		m.MinOperatingTemperature,
		m.MaxOperatingTemperature,
		m.RadiationResistance,
		m.RadiationResistanceI,
		m.MemoryFormat,
		m.SamplingTime,
		m.Qualication,
		m.Remark1
	});
	if (!componentName.IsNullOrEmpty())
	{
		items = items.Where(r => r.ComponentName == componentName);
	}
	response.ContentType = "application/json";
    await response.WriteAsJsonAsync(items.ToList());
});

app.MapGet("/api/capacitors", async (DataBase db, HttpContext context, string? componentName) =>
{
    var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = db.Capacitors
	.Select(c => new {
		c.ID,
		c.DocID,
		c.Kind.RuComponentKind,
		c.Kind.EnComponentKind,
		c.Type.RuComponentType,
		c.Type.EnComponentType,
		c.Manufacturer.ManufacturerName,
		c.OutputType,
		c.ComponentName,
		c.MinVoltage,
		c.MaxVoltage,
		c.MaxCapacity,
		c.MinCapacity,
		c.MinOperatingTemperature,
		c.MaxOperatingTemperature,
		c.AcceptableCapacityIncrease,
		c.AcceptableÑapacityReduction,
		c.QualicationSG,
		c.QualicationÅÑ,
		c.Remark1,
		c.Remark2
	});
	if (!componentName.IsNullOrEmpty())
	{
		items = items.Where(r => r.ComponentName == componentName);
	}
	response.ContentType = "application/json";
    await response.WriteAsJsonAsync(items.ToList());
});

app.MapGet("/api/microchips/bitdepthvalue", async (DataBase db, HttpContext context, string? manufacturerName, string? ruComponentKind, string? componentName, string? bitdepthvalue) =>
{
	var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = db.Microchips.Select(m => new { m.Manufacturer.ManufacturerName, m.BitDepthValue, m.ComponentName, m.Kind.RuComponentKind });
	if(!manufacturerName.IsNullOrEmpty())
	{
		items = items.Where(m => m.ManufacturerName == manufacturerName);
	}
	if (!ruComponentKind.IsNullOrEmpty())
	{
		items = items.Where(m => m.RuComponentKind == ruComponentKind);
	}
	if (!componentName.IsNullOrEmpty())
	{
		items = items.Where(m => m.ComponentName == componentName);
	}
	if (!bitdepthvalue.IsNullOrEmpty())
	{
		items = items.Where(m => m.BitDepthValue == bitdepthvalue);
	}
	response.ContentType = "application/json";
	await response.WriteAsJsonAsync(items.ToList());
});

app.Run();

