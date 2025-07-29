using app.Context;
using app.Logger;
using app.Models.Ef;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
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
			var query = db.Diods.AsQueryable();

			if (!string.IsNullOrEmpty(componentName))
			{
				query = query.Where(d => d.ComponentName == componentName);
			}

			var items = await query
				.Select(d => new Diods(d)
				{
					RuComponentKind = d.Kind.RuComponentKind,
					EnComponentKind = d.Kind.RuComponentKind,
					RuComponentType = d.Type.RuComponentType,
					EnComponentType = d.Type.EnComponentType,
					ManufacturerName = d.Manufacturer.ManufacturerName,
				})
				.ToListAsync();

			return items;
        }
    }
}