using app.Context;
using app.Entities;
using app.Models.Ef;
using Microsoft.EntityFrameworkCore;

namespace app.Db.Rep
{
	public class ComponentTypeRepository
	{
		DataBase db;
		public ComponentTypeRepository(DataBase context)
		{
			db = context;
		}
		public async Task<List<ComponentTypes>> Select()
		{
			var items = db.ComponentTypes.Select(t => t);
			return await items.ToListAsync();
		}
	}
}
