using app.Context;
using app.Entities;
using app.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> Get(
			[FromQuery] string? ruComponentType,
			[FromQuery] string? ruComponentKind,
			[FromQuery] string? manufacturerName
			)
        {
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

			return await items.ToListAsync();
        }
    }
}