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
			var items = db.Capacitors
			.Select(m => m);

			if (!componentName.IsNullOrEmpty())
			{
				items = items.Where(r => r.ComponentName == componentName);
			}

			return await items.ToListAsync();
        }
    }
}