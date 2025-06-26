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
builder.Services.AddControllers(); // èñïîëüçóåì êîíòğîëëåğû áåç ïğåäñòàâëåíèé
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
app.UseStaticFiles(new StaticFileOptions()
{
	OnPrepareResponse = ctx => {
		ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
		ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers",
		  "Origin, X-Requested-With, Content-Type, Accept");
	},

});


app.MapGet("/", (HttpContext context) =>
{
    context.Response.Redirect("/swagger/index.html");
});
//app.MapGet("/api/components", async (DataBase db, HttpContext context, string? ruComponentType, string? ruComponentKind, string? manufacturerName) =>
//{
//	var response = context.Response;
//	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
//	var items = db.ComponentTypes
//	.SelectMany(c => db.Microchips
//		.Where(m => m.Type.RuComponentType == c.RuComponentType)
//		.Select(m => new
//		{
//			m.Manufacturer.ManufacturerName,
//			m.Kind.RuComponentKind,
//			m.Kind.EnComponentKind,
//			m.Type.RuComponentType,
//			m.Type.EnComponentType,
//			m.ComponentName
//		}))
//	.Concat(db.Transistors
//		.Select(t => new
//		{
//			t.Manufacturer.ManufacturerName,
//			t.Kind.RuComponentKind,
//			t.Kind.EnComponentKind,
//			t.Type.RuComponentType,
//			t.Type.EnComponentType,
//			t.ComponentName
//		}))
//	.Concat(db.Resistors
//		.Select(r => new
//		{
//			r.Manufacturer.ManufacturerName,
//			r.Kind.RuComponentKind,
//			r.Kind.EnComponentKind,
//			r.Type.RuComponentType,
//			r.Type.EnComponentType,
//			r.ComponentName
//		}))
//	.Concat(db.Capacitors
//		.Select(ca => new
//		{
//			ca.Manufacturer.ManufacturerName,
//			ca.Kind.RuComponentKind,
//			ca.Kind.EnComponentKind,
//			ca.Type.RuComponentType,
//			ca.Type.EnComponentType,
//			ca.ComponentName
//		}))
//	.Concat(db.Diods
//		.Select(d => new
//		{
//			d.Manufacturer.ManufacturerName,
//			d.Kind.RuComponentKind,
//			d.Kind.EnComponentKind,
//			d.Type.RuComponentType,
//			d.Type.EnComponentType,
//			d.ComponentName
//		}));

//	if (ruComponentType != null)
//	{
//		items = items.Where(t => t.RuComponentType == ruComponentType);
//	}
//	if (ruComponentKind != null)
//	{
//		items = items.Where(t => t.RuComponentKind == ruComponentKind);
//	}
//	if (manufacturerName != null)
//	{
//		items = items.Where(t => t.ManufacturerName == manufacturerName);
//	}
	
//	items.ToList();

//	response.ContentType = "application/json";
//	await response.WriteAsJsonAsync(items);
//});

//app.MapGet("/api/resistors", async (
//	DataBase db, 
//	HttpContext context, 
//	string? componentName,
//	int? docID,
//	string? ruComponentType,
//	string? ruComponentKind,
//	string? manufacturerName,
//	string? enComponentKind,
//	string? enComponentType,

//	double? powerRating,
//	double? minVoltage,
//	double? maxVoltage,
//	double? resistanceTolerance,
//	double? minOperatingTemperature,
//	double? maxOperatingTemperature,
//	double? minRatedResistance,
//	double? maxRatedResistance,


//	string? qualicationSG,
//	string? qualicationÅÑ,
//	string? package,
//	string? remark1,
//	string? remark2
//	) =>
//{
//    var response = context.Response;
//	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
//	var items = db.Resistors
//	.Select(r => new
//	{
//		r.ID,
//		r.DocID,
//		r.Kind.RuComponentKind,
//		r.Kind.EnComponentKind,
//		r.Type.RuComponentType,
//		r.Type.EnComponentType,
//		r.Manufacturer.ManufacturerName,
//		r.PowerRating,
//		r.ComponentName,
//		r.MinVoltage,
//		r.MaxVoltage,
//		r.MinRatedResistance,
//		r.MaxRatedResistance,
//		r.ResistanceTolerance,
//		r.MinOperatingTemperature,
//		r.MaxOperatingTemperature,
//		r.CurrentLimit,
//		r.QualicationSG,
//		r.QualicationÅÑ,
//		r.Package,
//		r.Remark1,
//		r.Remark2
//	});
//	if (!componentName.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.ComponentName == componentName);
//	}

//	if (!docID.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.DocID == docID);
//	}
//	if (!ruComponentType.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.RuComponentType == ruComponentType);
//	}
//	if (!enComponentType.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.EnComponentType == enComponentType);
//	}
//	if (!ruComponentKind.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.RuComponentKind == ruComponentKind);
//	}
//	if (!enComponentKind.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.EnComponentKind == enComponentKind);
//	}
//	if (!manufacturerName.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.ManufacturerName == manufacturerName);
//	}

//	if (!minOperatingTemperature.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MinOperatingTemperature >= minOperatingTemperature);
//	}
//	if (!maxOperatingTemperature.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MaxOperatingTemperature <= maxOperatingTemperature);
//	}
//	if (!minVoltage.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MinVoltage >= minVoltage);
//	}
//	if (!maxVoltage.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MaxVoltage <= maxVoltage);
//	}
//	if (!powerRating.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.PowerRating == powerRating);
//	}

//	if (!resistanceTolerance.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.ResistanceTolerance == resistanceTolerance);
//	}
//	if (!minRatedResistance.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MaxRatedResistance >= minRatedResistance);
//	}
//	if (!maxRatedResistance.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MaxRatedResistance <= maxRatedResistance);
//	}
//	if (!package.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.Package == package);
//	}
//	if (!qualicationSG.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.QualicationSG == qualicationSG);
//	}
//	if (!qualicationÅÑ.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.QualicationÅÑ == qualicationÅÑ);
//	}
//	if (!remark1.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.Remark1 == remark1);
//	}
//	if (!remark2.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.Remark2 == remark2);
//	}

//	response.ContentType = "application/json";
//    await response.WriteAsJsonAsync(items.ToList());
//});

//app.MapGet("/api/transistors", async (
//	DataBase db, 
//	HttpContext context, 
//	string? componentName,
//	int? docID,
//	string? ruComponentType,
//	string? ruComponentKind,
//	string? manufacturerName,
//	string? enComponentKind,
//	string? enComponentType,


//	double? maxPermissibleDCVoltage,
//	double? minOperatingTemperature,
//	double? maxOperatingTemperature,
//	double? maxPermissibleDCCollectorCurrent,

//	double? radiationResistance,
//	string? radiationResistanceI,
//	string? qualicationSG,
//	string? qualicationÅÑ,
//	string? package,
//	string? remark1,
//	string? remark2
//	) =>
//{
//    var response = context.Response;
//	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
//	var items = db.Transistors
//	.Select(t => new
//	{
//		t.ID,
//		t.DocID,
//		t.Kind.RuComponentKind,
//		t.Kind.EnComponentKind,
//		t.Type.RuComponentType,
//		t.Type.EnComponentType,
//		t.Manufacturer.ManufacturerName,
//		t.MaxPermissibleDCVoltage,
//		t.ComponentName,
//		t.MinOperatingTemperature,
//		t.MaxOperatingTemperature,
//		t.MaxPermissibleDCCollectorCurrent,
//		t.RadiationResistance,
//		t.RadiationResistanceI,
//		t.QualicationSG,
//		t.QualicationÅÑ,
//		t.Package,
//		t.Remark1,
//		t.Remark2
//	});
//	if (!componentName.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.ComponentName == componentName);
//	}

//	if (!docID.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.DocID == docID);
//	}
//	if (!ruComponentType.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.RuComponentType == ruComponentType);
//	}
//	if (!enComponentType.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.EnComponentType == enComponentType);
//	}
//	if (!ruComponentKind.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.RuComponentKind == ruComponentKind);
//	}
//	if (!enComponentKind.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.EnComponentKind == enComponentKind);
//	}
//	if (!manufacturerName.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.ManufacturerName == manufacturerName);
//	}

//	if (!minOperatingTemperature.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MinOperatingTemperature >= minOperatingTemperature);
//	}
//	if (!maxOperatingTemperature.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MaxOperatingTemperature <= maxOperatingTemperature);
//	}
//	if (!maxPermissibleDCVoltage.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MaxPermissibleDCVoltage <= maxPermissibleDCVoltage);
//	}
//	if (!maxPermissibleDCCollectorCurrent.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MaxPermissibleDCCollectorCurrent <= maxPermissibleDCCollectorCurrent);
//	}
	
//	if (!radiationResistance.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.RadiationResistance == radiationResistance);
//	}
//	if (!radiationResistanceI.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.RadiationResistanceI == radiationResistanceI);
//	}
//	if (!qualicationSG.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.QualicationSG == qualicationSG);
//	}
//	if (!qualicationÅÑ.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.QualicationÅÑ == qualicationÅÑ);
//	}
//	if (!package.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.Package == package);
//	}
//	if (!remark1.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.Remark1 == remark1);
//	}
//	if (!remark2.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.Remark2 == remark2);
//	}
//	response.ContentType = "application/json";
//    await response.WriteAsJsonAsync(items.ToList());
//});

//app.MapGet("/api/diods", async (
//	DataBase db,
//	HttpContext context,
//	string? componentName,
//	int? docID,
//	string? ruComponentType,
//	string? ruComponentKind,
//	string? manufacturerName,
//	string? enComponentKind,
//	string? enComponentType,
//	string? outputType,


//	double? maxPermissibleDCVoltage,
//	double? minOperatingTemperature,
//	double? maxOperatingTemperature,
//	double? maxPermissibleAverageDirectCurrent,
//	double? maxiPermissibleDirectCurrent,

//	double? radiationResistance,
//	string? radiationResistanceI,
//	string? qualicationSG,
//	string? qualicationÅÑ,
//	string? package,
//	string? remark1,
//	string? remark2
//	) =>
//{
//    var response = context.Response;
//	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
//	var items = db.Diods
//	.Select(d => new {
//		d.ID,
//		d.DocID,
//		d.Kind.RuComponentKind,
//		d.Kind.EnComponentKind,
//		d.Type.RuComponentType,
//		d.Type.EnComponentType,
//		d.Manufacturer.ManufacturerName,
//		d.MaxPermissibleDCVoltage,
//		d.ComponentName,
//		d.MinOperatingTemperature,
//		d.MaxOperatingTemperature,
//		d.MaxPermissibleAverageDirectCurrent,
//		d.MaxiPermissibleDirectCurrent,
//		d.RadiationResistance,
//		d.RadiationResistanceI,
//		d.QualicationSG,
//		d.QualicationÅÑ,
//		d.Package,
//		d.Remark1,
//		d.Remark2
//	});
//	if (!componentName.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.ComponentName == componentName);
//	}

//	if (!docID.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.DocID == docID);
//	}
//	if (!ruComponentType.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.RuComponentType == ruComponentType);
//	}
//	if (!enComponentType.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.EnComponentType == enComponentType);
//	}
//	if (!ruComponentKind.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.RuComponentKind == ruComponentKind);
//	}
//	if (!enComponentKind.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.EnComponentKind == enComponentKind);
//	}
//	if (!manufacturerName.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.ManufacturerName == manufacturerName);
//	}

//	if (!minOperatingTemperature.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MinOperatingTemperature >= minOperatingTemperature);
//	}
//	if (!maxOperatingTemperature.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MaxOperatingTemperature <= maxOperatingTemperature);
//	}
//	if (!maxPermissibleDCVoltage.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MaxPermissibleDCVoltage <= maxPermissibleDCVoltage);
//	}
//	if (!maxPermissibleAverageDirectCurrent.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MaxPermissibleAverageDirectCurrent <= maxPermissibleAverageDirectCurrent);
//	}
//	if (!maxiPermissibleDirectCurrent.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MaxiPermissibleDirectCurrent <= maxiPermissibleDirectCurrent);
//	}

//	if (!radiationResistance.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.RadiationResistance == radiationResistance);
//	}
//	if (!radiationResistanceI.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.RadiationResistanceI == radiationResistanceI);
//	}
//	if (!qualicationSG.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.QualicationSG == qualicationSG);
//	}
//	if (!qualicationÅÑ.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.QualicationÅÑ == qualicationÅÑ);
//	}
//	if (!remark1.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.Remark1 == remark1);
//	}
//	if (!remark2.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.Remark2 == remark2);
//	}
//	response.ContentType = "application/json";
//	await response.WriteAsJsonAsync(items.ToList());
//});

//app.MapGet("/api/microchips", async (
//	DataBase db, 
//	HttpContext context, 
//	string? componentName, 
//	int? docID, 
//	string? ruComponentType, 
//	string? ruComponentKind, 
//	string? manufacturerName, 
//	string? enComponentKind, 
//	string? enComponentType,
//	string? enTechnologyName,
//	string? ruTechnologyName,
//	string? interfaces, 
//	double? minVoltage,
//	double? maxVoltage,
//	double? frequency,
//	string? bitDepthValue,
//	double? consumptionCurrent,
//	double? minOperatingTemperature,
//	double? maxOperatingTemperature,
//	double? radiationResistance,
//	string? radiationResistanceI,
//	string? memoryFormat,
//	double? samplingTime,
//	string? remark1
//	) =>
//{
//    var response = context.Response;
//	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");

//	var items = db.Microchips
//	.Select(m => new
//	{
//		m.ID,
//		m.DocID,
//		m.Kind.RuComponentKind,
//		m.Kind.EnComponentKind,
//		m.Type.RuComponentType,
//		m.Type.EnComponentType,
//		m.Manufacturer.ManufacturerName,
//		m.BitDepthValue,
//		m.ComponentName,
//		m.ConsumptionCurrent,
//		m.Interfaces,
//		m.MinVoltage,
//		m.MaxVoltage,
//		m.Frequency,
//		m.Technology.EnTechnologyName,
//		m.Technology.RuTechnologyName,
//		m.MinOperatingTemperature,
//		m.MaxOperatingTemperature,
//		m.RadiationResistance,
//		m.RadiationResistanceI,
//		m.MemoryFormat,
//		m.SamplingTime,
//		m.Qualication,
//		m.Remark1
//	});
//	if (!componentName.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.ComponentName == componentName);
//	}
//	if (!docID.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.DocID == docID);
//	}
//	if (!ruComponentType.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.RuComponentType == ruComponentType);
//	}
//	if (!enComponentType.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.EnComponentType == enComponentType);
//	}
//	if (!ruComponentKind.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.RuComponentKind == ruComponentKind);
//	}
//	if (!enComponentKind.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.EnComponentKind == enComponentKind);
//	}
//	if (!manufacturerName.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.ManufacturerName == manufacturerName);
//	}
//	if (!bitDepthValue.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.BitDepthValue == bitDepthValue);
//	}
//	if (!interfaces.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.Interfaces == interfaces);
//	}
//	if (!frequency.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.Frequency == frequency);
//	}
//	if (!minVoltage.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MinVoltage >= minVoltage);
//	}
//	if (!maxVoltage.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MaxVoltage <= maxVoltage);
//	}
//	if (!consumptionCurrent.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.ConsumptionCurrent == consumptionCurrent);
//	}
//	if (!minOperatingTemperature.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MinOperatingTemperature >= minOperatingTemperature);
//	}
//	if (!maxOperatingTemperature.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MaxOperatingTemperature <= maxOperatingTemperature);
//	}
//	if (!radiationResistance.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.RadiationResistance == radiationResistance);
//	}
//	if (!radiationResistanceI.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.RadiationResistanceI == radiationResistanceI);
//	}
//	if (!memoryFormat.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MemoryFormat == memoryFormat);
//	}
//	if (!samplingTime.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.SamplingTime == samplingTime);
//	}
//	if (!remark1.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.Remark1 == remark1);
//	}
//	response.ContentType = "application/json";
//    await response.WriteAsJsonAsync(items.ToList());
//});

//app.MapGet("/api/capacitors", async (
//	DataBase db, 
//	HttpContext context,
//	string? componentName,
//	int? docID,
//	string? ruComponentType,
//	string? ruComponentKind,
//	string? manufacturerName,
//	string? enComponentKind,
//	string? enComponentType,
//	string? outputType,

//	double? minVoltage,
//	double? maxVoltage,
//	double? minCapacity,
//	double? maxCapacity,

//	double? minOperatingTemperature,
//	double? maxOperatingTemperature,
//	double? acceptableCapacityIncrease,
//	double? acceptableÑapacityReduction,
//	string? qualicationSG,
//	string? qualicationÅÑ,
//	string? remark1,
//	string? remark2

//	) =>
//{
//    var response = context.Response;
//	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
//	var items = db.Capacitors
//	.Select(c => new {
//		c.ID,
//		c.DocID,
//		c.Kind.RuComponentKind,
//		c.Kind.EnComponentKind,
//		c.Type.RuComponentType,
//		c.Type.EnComponentType,
//		c.Manufacturer.ManufacturerName,
//		c.OutputType,
//		c.ComponentName,
//		c.MinVoltage,
//		c.MaxVoltage,
//		c.MaxCapacity,
//		c.MinCapacity,
//		c.MinOperatingTemperature,
//		c.MaxOperatingTemperature,
//		c.AcceptableCapacityIncrease,
//		c.AcceptableÑapacityReduction,
//		c.QualicationSG,
//		c.QualicationÅÑ,
//		c.Remark1,
//		c.Remark2
//	});
//	if (!componentName.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.ComponentName == componentName);
//	}
//	if (!docID.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.DocID == docID);
//	}
//	if (!ruComponentType.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.RuComponentType == ruComponentType);
//	}
//	if (!enComponentType.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.EnComponentType == enComponentType);
//	}
//	if (!ruComponentKind.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.RuComponentKind == ruComponentKind);
//	}
//	if (!enComponentKind.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.EnComponentKind == enComponentKind);
//	}
//	if (!manufacturerName.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.ManufacturerName == manufacturerName);
//	}	
//	if (!outputType.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.OutputType == outputType);
//	}
//	if (!minVoltage.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MinVoltage >= minVoltage);
//	}
//	if (!maxVoltage.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MaxVoltage <= maxVoltage);
//	}
//	if (!minCapacity.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MinCapacity >= minCapacity);
//	}
//	if (!maxCapacity.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MaxCapacity <= maxCapacity);
//	}
//	if (!minOperatingTemperature.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MinOperatingTemperature >= minOperatingTemperature);
//	}
//	if (!maxOperatingTemperature.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.MaxOperatingTemperature <= maxOperatingTemperature);
//	}
//	if (!acceptableCapacityIncrease.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.AcceptableCapacityIncrease == acceptableCapacityIncrease);
//	}
//	if (!acceptableÑapacityReduction.ToString().IsNullOrEmpty())
//	{
//		items = items.Where(r => r.AcceptableÑapacityReduction == acceptableÑapacityReduction);
//	}
//	if (!qualicationSG.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.QualicationSG == qualicationSG);
//	}
//	if (!qualicationÅÑ.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.QualicationÅÑ == qualicationÅÑ);
//	}
//	if (!remark1.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.Remark1 == remark1);
//	}
//	if (!remark2.IsNullOrEmpty())
//	{
//		items = items.Where(r => r.Remark2 == remark2);
//	}
//	response.ContentType = "application/json";
//    await response.WriteAsJsonAsync(items.ToList());
//});

//app.MapGet("/api/microchips/bitdepthvalue", async (DataBase db, HttpContext context, string? manufacturerName, string? ruComponentKind, string? componentName, string? bitdepthvalue) =>
//{
//	var response = context.Response;
//	logger.LogInformation($"Request: {context.Request.Path} {DateTime.Now}");
//	var items = db.Microchips.Select(m => new { m.Manufacturer.ManufacturerName, m.BitDepthValue, m.ComponentName, m.Kind.RuComponentKind });
//	if(!manufacturerName.IsNullOrEmpty())
//	{
//		items = items.Where(m => m.ManufacturerName == manufacturerName);
//	}
//	if (!ruComponentKind.IsNullOrEmpty())
//	{
//		items = items.Where(m => m.RuComponentKind == ruComponentKind);
//	}
//	if (!componentName.IsNullOrEmpty())
//	{
//		items = items.Where(m => m.ComponentName == componentName);
//	}
//	if (!bitdepthvalue.IsNullOrEmpty())
//	{
//		items = items.Where(m => m.BitDepthValue == bitdepthvalue);
//	}
//	response.ContentType = "application/json";
//	await response.WriteAsJsonAsync(items.ToList());
//});

app.Run();

