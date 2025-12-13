using app.Db.Context;
using app.Db.ef;
using app.Db.utils;
using Microsoft.EntityFrameworkCore;

namespace app.Db.Rep
{
	public class ComponentTypeRepository : IRepositoryBase<ComponentTypes>
	{
		DataBase db;
		public ComponentTypeRepository(DataBase context)
		{
			db = context;
		}

		public int GetCount()
		{
			return db.ComponentTypes.Count();
		}

		public async Task<List<ComponentTypes>> SelectAll()
		{
			var items = db.ComponentTypes.Select(t => t);
			return await items.ToListAsync();
		}
	}
}
