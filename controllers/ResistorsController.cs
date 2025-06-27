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
			var items = db.Resistors
			.Select(r => new Resistors(r)
			{
				RuComponentKind = r.Kind.RuComponentKind,
				EnComponentKind = r.Kind.RuComponentKind,
				RuComponentType = r.Type.RuComponentType,
				EnComponentType = r.Type.EnComponentType,
				ManufacturerName = r.Manufacturer.ManufacturerName,
			});

			if (!componentName.IsNullOrEmpty())
			{
				items = items.Where(r => r.ComponentName == componentName);
			}

			return await items.ToListAsync();
        }
    }
}