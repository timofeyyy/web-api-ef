using app.Context;
using app.Entities;
using app.Models.other;
using Microsoft.AspNetCore.Mvc;


namespace WebAPIApp.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class ManufacturersController : ControllerBase
    {
        DataBase db;
		public ManufacturersController(DataBase context)
        {
            db = context;
        }

		//[HttpGet("names")]
		//public async Task<ActionResult<IEnumerable<string>>> Get(
		//	[FromQuery] string? manufacturerName
		//	)
		//{
		//	var items = db.Manufacturers.Select(m => m.ManufacturerName);
		//	if (manufacturerName != null)
		//	{
		//		items = items.Where(m => m == manufacturerName);
		//	}
		//	return await items.ToListAsync();
		//}

		[HttpGet("production")]
		public async Task<ActionResult<Dictionary<string, Dictionary<string, int>>>> Get(
			[FromQuery] string? ruComponentType,
			[FromQuery] string? ruComponentKind,
			[FromQuery] string? manufacturerName
			)
		{
			var items = db.ComponentTypes
			.SelectMany(c => db.Microchips
				.Where(m => m.Type.RuComponentType == c.RuComponentType)
				.Select(m => new ComponentPreview()
				{
					ManufacturerName = m.Manufacturer.ManufacturerName,
					RuComponentKind = m.Kind.RuComponentKind,
					EnComponentKind = m.Kind.EnComponentKind,
					RuComponentType = m.Type.RuComponentType,
					EnComponentType = m.Type.EnComponentType,
					ComponentName = m.ComponentName
				}))
			.Concat(db.Transistors
				.Select(t => new ComponentPreview()
				{
					ManufacturerName = t.Manufacturer.ManufacturerName,
					RuComponentKind = t.Kind.RuComponentKind,
					EnComponentKind = t.Kind.EnComponentKind,
					RuComponentType = t.Type.RuComponentType,
					EnComponentType = t.Type.EnComponentType,
					ComponentName = t.ComponentName
				}))
			.Concat(db.Resistors
				.Select(r => new ComponentPreview()
				{
					ManufacturerName = r.Manufacturer.ManufacturerName,
					RuComponentKind = r.Kind.RuComponentKind,
					EnComponentKind = r.Kind.EnComponentKind,
					RuComponentType = r.Type.RuComponentType,
					EnComponentType = r.Type.EnComponentType,
					ComponentName = r.ComponentName
				}))
			.Concat(db.Capacitors
				.Select(c => new ComponentPreview()
				{
					ManufacturerName = c.Manufacturer.ManufacturerName,
					RuComponentKind = c.Kind.RuComponentKind,
					EnComponentKind = c.Kind.EnComponentKind,
					RuComponentType = c.Type.RuComponentType,
					EnComponentType = c.Type.EnComponentType,
					ComponentName = c.ComponentName
				}))
			.Concat(db.Diods
				.Select(d => new ComponentPreview()
				{
					ManufacturerName = d.Manufacturer.ManufacturerName,
					RuComponentKind = d.Kind.RuComponentKind,
					EnComponentKind = d.Kind.EnComponentKind,
					RuComponentType = d.Type.RuComponentType,
					EnComponentType = d.Type.EnComponentType,
					ComponentName = d.ComponentName
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

			Dictionary<string, Dictionary<string, int>> dict = new Dictionary<string, Dictionary<string, int>>();

			foreach (var item in items)
			{
				if (!dict.ContainsKey(item.ManufacturerName))
				{
					dict[item.ManufacturerName] = new Dictionary<string, int>();
				}
				if (!dict[item.ManufacturerName].ContainsKey(item.EnComponentType))
				{
					dict[item.ManufacturerName][item.EnComponentType] = 0;
				}
				dict[item.ManufacturerName][item.EnComponentType] += 1;
			}

			return dict;
		}
	}
}