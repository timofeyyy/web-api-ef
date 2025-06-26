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
    public class MicrochipsController : ControllerBase
    {
        DataBase db;
		public MicrochipsController(DataBase context)
        {
            db = context;
        }
 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Microchips>>> Get(
			[FromQuery] string? componentName
			)
        {
			var items = db.Microchips
	        .Select(m => m);

			if (!componentName.IsNullOrEmpty())
			{
				items = items.Where(r => r.ComponentName == componentName);
			}

			return await items.ToListAsync();
        }

		[HttpGet("bitdepthvalue")]
		public async Task<ActionResult<object>> Get(
			[FromQuery] string? componentName,
			[FromQuery] string? manufacturerName,
			[FromQuery] string? ruComponentKind,
			[FromQuery] string? bitdepthvalue
			)
		{
			var items = db.Microchips.Select(m => new { m.Manufacturer.ManufacturerName, m.BitDepthValue, m.ComponentName, m.Kind.RuComponentKind });

			if (!manufacturerName.IsNullOrEmpty())
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
			return await items.ToListAsync();
		}
	}
}