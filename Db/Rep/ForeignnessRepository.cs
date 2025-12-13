using app.Db.Context;
using app.Db.ef;
using app.Db.utils;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Db.Rep
{
	public class ForeignnessRepository: IRepositoryBase<Foreignness>
	{
		DataBase db;
		public ForeignnessRepository(DataBase context)
		{
			db = context;
		}

		public int GetCount()
		{
			return db.Manufacturers.Count();
		}
		public Task<List<Foreignness>> SelectAll()
		{
			var query = db.Foreignnesses.AsQueryable();
			var items = query
							.Select(c => c);
			return items.ToListAsync();
		}
	}
}
