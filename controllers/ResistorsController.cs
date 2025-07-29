using app.Context;
using app.Logger;
using app.Models.Ef;
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
    public class ResistorsController : ControllerBase
    {
        DataBase db;
		public ResistorsController(DataBase context)
        {
            db = context;
        }
 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resistors>>> Get(
			[FromQuery] string? componentName
			)
        {
			var query = db.Resistors.AsQueryable();

			if (!string.IsNullOrEmpty(componentName))
			{
				query = query.Where(d => d.ComponentName == componentName);
			}

			var items = await query
				.Select(r => new Resistors(r)
				{
					RuComponentKind = r.Kind.RuComponentKind,
					EnComponentKind = r.Kind.RuComponentKind,
					RuComponentType = r.Type.RuComponentType,
					EnComponentType = r.Type.EnComponentType,
					ManufacturerName = r.Manufacturer.ManufacturerName,
				})
				.ToListAsync();

			return items;
		}
    }
}