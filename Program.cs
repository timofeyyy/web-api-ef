using app.Context;
using app.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using app.Entities;

//ñòğîêà ïîäêëş÷åíèé
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("container_sql_oim1");
var logPath = builder.Configuration["Logging:FilePath:Value"];

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




app.MapGet("/", (HttpContext context) =>
{
    context.Response.Redirect("/swagger/index.html");
    //await TestSQL(context);
});

app.MapGet("/api/componentTypes", async (DataBase db, HttpContext context) =>
{
	var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = db.ComponentTypes.ToList();
	response.ContentType = "application/json";
	await response.WriteAsJsonAsync(items);
});

app.MapGet("/api/componentKinds", async (DataBase db, HttpContext context) =>
{
	var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = db.ComponentKinds.ToList();
	response.ContentType = "application/json";
	await response.WriteAsJsonAsync(items);
});

app.MapGet("/api/test", async (DataBase db, HttpContext context) =>
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
	//.Concat(db.Resistors
	//	.Select(r => new
	//	{
	//		r.Manufacturer.ManufacturerName,
	//		r.Kind.RuComponentKind,
	//		r.Kind.EnComponentKind,
	//		r.Type.RuComponentType,
	//		r.Type.EnComponentType,
	//		r.ComponentName
	//	}))
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
		}))
	.ToList();

	response.ContentType = "application/json";
	await response.WriteAsJsonAsync(items);
});


app.MapGet("/api/options", async (DataBase db, HttpContext context) =>
{
	var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = 
	db.Microchips
	.Select(m => new { m.Manufacturer.ManufacturerName, m.Kind.RuComponentKind, m.Kind.EnComponentKind, m.Type.RuComponentType, m.Type.EnComponentType, m.ComponentName })
	.GroupBy(m => m.ManufacturerName)
	//.Union(db.Transistors.Select(m => new { m.Manufacturer.ManufacturerName, m.Kind.RuComponentKind, m.Kind.EnComponentKind, m.Type.RuComponentType, m.Type.EnComponentType, m.ComponentName }))
	//.Union(db.Resistors.Select(m => new { m.Manufacturer.ManufacturerName, m.Kind.RuComponentKind, m.Kind.EnComponentKind, m.Type.RuComponentType, m.Type.EnComponentType, m.ComponentName }))
	//.Union(db.Capacitors.Select(m => new { m.Manufacturer.ManufacturerName, m.Kind.RuComponentKind, m.Kind.EnComponentKind, m.Type.RuComponentType, m.Type.EnComponentType, m.ComponentName }))
	//.Union(db.Diods.Select(m => new { m.Manufacturer.ManufacturerName, m.Kind.RuComponentKind, m.Kind.EnComponentKind, m.Type.RuComponentType, m.Type.EnComponentType, m.ComponentName }))
	.ToList();
	response.ContentType = "application/json";
	await response.WriteAsJsonAsync(items);
});

app.MapGet("/api/componentNames", async (DataBase db, HttpContext context) =>
{
	var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = db.Microchips
	.Select(m => new {m.ComponentName })
	.Union(db.Capacitors.Select(m => new { m.ComponentName, }))
	.Union(db.Resistors.Select(m => new { m.ComponentName }))
	.Union(db.Transistors.Select(m => new { m.ComponentName }))
	.Union(db.Diods.Select(m => new { m.ComponentName }))
	.ToList();
	response.ContentType = "application/json";
	await response.WriteAsJsonAsync(items);
});

app.MapGet("/api/manufacturers", async (DataBase db, HttpContext context) =>
{
	var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = db.Manufacturers.Select(m => new { m.ManufacturerName }).ToList();

	response.ContentType = "application/json";
	await response.WriteAsJsonAsync(items);
});


app.MapGet("/api/resistors", async (DataBase db, HttpContext context) =>
{
    var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = db.Resistors
	.Select(r => new {
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
	})
	.ToList();
    response.ContentType = "application/json";
    await response.WriteAsJsonAsync(items);
});

app.MapGet("/api/transistors", async (DataBase db, HttpContext context) =>
{
    var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = db.Transistors
	.Select(t => new {
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
	})
	.ToList();
    response.ContentType = "application/json";
    await response.WriteAsJsonAsync(items);
});

app.MapGet("/api/diods", async (DataBase db, HttpContext context) =>
{
    var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	object items = db.Diods
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
	})
	.ToList();
    response.ContentType = "application/json";
    await response.WriteAsJsonAsync(items);
});

app.MapGet("/api/microchips", async (DataBase db, HttpContext context) =>
{
    var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = db.Microchips
	.Select(m => new {
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
	})
	.ToList();
    response.ContentType = "application/json";
    await response.WriteAsJsonAsync(items);
});

app.MapGet("/api/capacitors", async (DataBase db, HttpContext context) =>
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
	})
	.ToList();
    response.ContentType = "application/json";
    await response.WriteAsJsonAsync(items);
});

app.MapGet("/api/microchips/bitdepthvalue", async (DataBase db, HttpContext context, string? ManufacturerName, string? componentKind, string? componentName, string? bitdepthvalue) =>
{
	var response = context.Response;
	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
	var items = db.Microchips.Select(m => new { m.Manufacturer.ManufacturerName, m.BitDepthValue, m.ComponentName, m.Kind.RuComponentKind });
	if(!ManufacturerName.IsNullOrEmpty())
	{
		items = items.Where(m => m.ManufacturerName == ManufacturerName);
	}
	if (!componentKind.IsNullOrEmpty())
	{
		items = items.Where(m => m.RuComponentKind == componentKind);
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


//app.MapGet("/api/microchips/bitdepthvalue", async (DataBase db, HttpContext context, string? manufacturername, string? componentkind, string? componentname, string? bitdepthvalue) =>
//{
//	var response = context.Response;
//	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
//	var items = db.Microchips.Select(m => new { m.Manufacturer.ManufacturerName, m.BitDepthValue, m.ComponentName, m.Kind.RuComponentKind, m. });
//	if (!manufacturername.IsNullOrEmpty())
//	{
//		items = items.Where(m => m.ManufacturerName == manufacturername);
//	}
//	if (!componentkind.IsNullOrEmpty())
//	{
//		items = items.Where(m => m. == manufacturername);
//	}
//	if (!bitdepthvalue.IsNullOrEmpty())
//	{
//		items = items.Where(m => m.BitDepthValue == bitdepthvalue);
//	}
//	response.ContentType = "application/json";
//	await response.WriteAsJsonAsync(items.ToList());
//});


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

