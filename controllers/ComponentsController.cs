using app.Context;
using app.Entities;
using app.Logger;
using app.Models.Ef;
using app.Models.other;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComponentsController : ControllerBase
    {
        DataBase db;
		public ComponentsController(DataBase context)
        {
            db = context;
        }
		
      
		[HttpGet("short")]
		public async Task<ActionResult<IEnumerable<ComponentPreview>>> Get(
		[FromQuery] string? ruComponentType,
			[FromQuery] string? ruComponentKind,
			[FromQuery] string? manufacturerName,
			[FromQuery] string? componentName
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
					EnComponentType =d.Type.EnComponentType,
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
			if (componentName != null)
			{
				items = items.Where(t => t.ComponentName == componentName);
			}

			return await items.ToListAsync();
		}

		[HttpGet("statistic")]
		public async Task<ActionResult<Dictionary<string, List<ManufacturerProduction>>>> Get(
			[FromQuery] string? ruComponentType
			)
		{

			Dictionary<string, int> totals = new Dictionary<string, int>();
			totals.Add("microchip", db.Microchips.Count());
			totals.Add("resistor", db.Resistors.Count());
			totals.Add("transistor", db.Transistors.Count());
			totals.Add("diod", db.Diods.Count());
			totals.Add("capacitor", db.Capacitors.Count());


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

			Dictionary<string, List<ManufacturerProduction>> dict = new Dictionary<string, List<ManufacturerProduction>>();

			foreach (var item in items)
			{
				if(!dict.ContainsKey(item.EnComponentType))
				{
					dict[item.EnComponentType] = new List<ManufacturerProduction>();
				}

				var list = dict[item.EnComponentType].Where(mp => mp.ManufacturerName == item.ManufacturerName).ToList();
				int componentTypeTotal = totals[item.EnComponentType.ToLower()];

				if (list.Count == 0)
				{
					ManufacturerProduction mp = new ManufacturerProduction()
					{
						ManufacturerName = item.ManufacturerName,
						Amount = 1,
						Weight = 100 / double.Parse(componentTypeTotal.ToString())
					};
					dict[item.EnComponentType].Add(mp);
				}
				else
				{
					foreach (var existedMP in dict[item.EnComponentType].Where(mp => mp.ManufacturerName == item.ManufacturerName))
					{
						existedMP.Amount += 1;
						existedMP.Weight = (100 * existedMP.Amount) / double.Parse(componentTypeTotal.ToString());
						break;
					}
				}
			}
			return dict;
		}

		[HttpGet("names")]
		public async Task<ActionResult<IEnumerable<ComponentTypes>>> Get()
		{
			var items = db.ComponentTypes.Select(t => t);
			return await items.ToListAsync();
		}

		[HttpGet("all")]
		public async Task<ActionResult<ComponentAll>> Get(
		[FromQuery] string? ruComponentType,
			[FromQuery] string? ruComponentKind,
			[FromQuery] string? manufacturerName
			)
		{

			List<Microchips> m = new List<Microchips>();
			List<Capacitors> c = new List<Capacitors>();
			List<Diods> d = new List<Diods>();
			List<Transistors> t = new List<Transistors>();
			List<Resistors> r = new List<Resistors>();

			if (ruComponentType.IsNullOrEmpty() || ruComponentType.ToLower() == "микросхема") {
				m = db.Microchips
				.Select(m => new Microchips(m)
				{
					RuComponentKind = m.Kind.RuComponentKind,
					EnComponentKind = m.Kind.RuComponentKind,
					RuComponentType = m.Type.RuComponentType,
					EnComponentType = m.Type.EnComponentType,
					ManufacturerName = m.Manufacturer.ManufacturerName,
					EnTechnologyName = m.Technology.EnTechnologyName,
					RuTechnologyName = m.Technology.RuTechnologyName
				}).ToList();

				if (!ruComponentKind.IsNullOrEmpty()) {
					m = m.Where(m => m.RuComponentKind == ruComponentKind).ToList();
				}

				if (!manufacturerName.IsNullOrEmpty())
				{
					m = m.Where(m => m.ManufacturerName == manufacturerName).ToList();
				}
			}

			if (ruComponentType.IsNullOrEmpty() || ruComponentType.ToLower() == "конденсатор")
			{
				 c = db.Capacitors
				.Select(c => new Capacitors(c)
				{
					RuComponentKind = c.Kind.RuComponentKind,
					EnComponentKind = c.Kind.RuComponentKind,
					RuComponentType = c.Type.RuComponentType,
					EnComponentType = c.Type.EnComponentType,
					ManufacturerName = c.Manufacturer.ManufacturerName
				}).ToList();

				if (!ruComponentKind.IsNullOrEmpty())
				{
					c = c.Where(c => c.RuComponentKind == ruComponentKind).ToList();
				}

				if (!manufacturerName.IsNullOrEmpty())
				{
					c = c.Where(c => c.ManufacturerName == manufacturerName).ToList();
				}
			}


			if (ruComponentType.IsNullOrEmpty() || ruComponentType.ToLower() == "транзистор")
			{
				t = db.Transistors
				.Select(t => new Transistors(t)
				{
					RuComponentKind = t.Kind.RuComponentKind,
					EnComponentKind = t.Kind.RuComponentKind,
					RuComponentType = t.Type.RuComponentType,
					EnComponentType = t.Type.EnComponentType,
					ManufacturerName = t.Manufacturer.ManufacturerName
				}).ToList();

				if (!ruComponentKind.IsNullOrEmpty())
				{
					t = t.Where(t => t.RuComponentKind == ruComponentKind).ToList();
				}

				if (!manufacturerName.IsNullOrEmpty())
				{
					t = t.Where(t => t.ManufacturerName == manufacturerName).ToList();
				}
			}

			if (ruComponentType.IsNullOrEmpty() || ruComponentType.ToLower() == "диод")
			{
				d = db.Diods
				.Select(d => new Diods(d)
				{
					RuComponentKind = d.Kind.RuComponentKind,
					EnComponentKind = d.Kind.RuComponentKind,
					RuComponentType = d.Type.RuComponentType,
					EnComponentType = d.Type.EnComponentType,
					ManufacturerName = d.Manufacturer.ManufacturerName,
				}).ToList();

				if (!ruComponentKind.IsNullOrEmpty())
				{
					d = d.Where(d => d.RuComponentKind == ruComponentKind).ToList();
				}

				if (!manufacturerName.IsNullOrEmpty())
				{
					d = d.Where(d => d.ManufacturerName == manufacturerName).ToList();
				}
			}



			if (ruComponentType.IsNullOrEmpty() || ruComponentType.ToLower() == "резистор")
			{
				r = db.Resistors
				.Select(r => new Resistors(r)
				{
					RuComponentKind = r.Kind.RuComponentKind,
					EnComponentKind = r.Kind.RuComponentKind,
					RuComponentType = r.Type.RuComponentType,
					EnComponentType = r.Type.EnComponentType,
					ManufacturerName = r.Manufacturer.ManufacturerName,
				}).ToList();

				if (!ruComponentKind.IsNullOrEmpty())
				{
					r = r.Where(r => r.RuComponentKind == ruComponentKind).ToList();
				}

				if (!manufacturerName.IsNullOrEmpty())
				{
					r = r.Where(r => r.ManufacturerName == manufacturerName).ToList();
				}
			}

	
			return new ComponentAll() {
				microchip = m,
				capacitor = c,
				diod = d,
				resistor = r,
				transistor = t
			};
		}
	}
}