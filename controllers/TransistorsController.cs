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
    public class TransistorsController : ControllerBase
    {
        DataBase db;
		public TransistorsController(DataBase context)
        {
            db = context;
        }
 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transistors>>> Get(
			[FromQuery] string? componentName
			)
        {
			var items = db.Transistors
			.Select(t => new Transistors(t)
			{
				RuComponentKind = t.Kind.RuComponentKind,
				EnComponentKind = t.Kind.RuComponentKind,
				RuComponentType = t.Type.RuComponentType,
				EnComponentType = t.Type.EnComponentType,
				ManufacturerName = t.Manufacturer.ManufacturerName
			});

			if (!componentName.IsNullOrEmpty())
			{
				items = items.Where(r => r.ComponentName == componentName);
			}

			return await items.ToListAsync();
        }
    }
}