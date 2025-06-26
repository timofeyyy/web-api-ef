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
	        .Select(m => m);

			if (!componentName.IsNullOrEmpty())
			{
				items = items.Where(r => r.ComponentName == componentName);
			}

			return await items.ToListAsync();
        }
    }
}