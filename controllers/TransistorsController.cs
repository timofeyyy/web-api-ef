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
			var query = db.Transistors.AsQueryable();

			if (!string.IsNullOrEmpty(componentName))
			{
				query = query.Where(d => d.ComponentName == componentName);
			}

			var items = await query
				.Select(t=>t)
				.ToListAsync();

			return items;
		}
    }
}