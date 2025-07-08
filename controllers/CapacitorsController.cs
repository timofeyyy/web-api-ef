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
    public class CapacitorsController : ControllerBase
    {
        DataBase db;
		public CapacitorsController(DataBase context)
        {
            db = context;
        }
 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Capacitors>>> Get(
			[FromQuery] string? componentName
			)
        {
			var query = db.Capacitors.AsQueryable();

			if (!string.IsNullOrEmpty(componentName))
			{
				query = query.Where(d => d.ComponentName == componentName);
			}

			var items = await query
				.Select(c => new Capacitors(c)
				{
					RuComponentKind = c.Kind.RuComponentKind,
					EnComponentKind = c.Kind.RuComponentKind,
					RuComponentType = c.Type.RuComponentType,
					EnComponentType = c.Type.EnComponentType,
					ManufacturerName = c.Manufacturer.ManufacturerName,
				})
				.ToListAsync();

			return items;
		}
    }
}