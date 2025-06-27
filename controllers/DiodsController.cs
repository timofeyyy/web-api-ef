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
    public class DiodsController : ControllerBase
    {
        DataBase db;
		public DiodsController(DataBase context)
        {
            db = context;
        }
 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Diods>>> Get(
			[FromQuery] string? componentName
			)
        {
			var items = db.Diods
			.Select(d => new Diods(d)
			{
				RuComponentKind = d.Kind.RuComponentKind,
				EnComponentKind = d.Kind.RuComponentKind,
				RuComponentType = d.Type.RuComponentType,
				EnComponentType = d.Type.EnComponentType,
				ManufacturerName = d.Manufacturer.ManufacturerName,
			});

			if (!componentName.IsNullOrEmpty())
			{
				items = items.Where(r => r.ComponentName == componentName);
			}

			return await items.ToListAsync();
        }
    }
}