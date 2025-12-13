using app.Db.Context;
using app.Db.ef;
using app.Db.utils;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace app.Db.Rep
{
	public class MicrochipRepository : IRepositoryBase<Microchips>
	{
		DataBase db;
		public MicrochipRepository(DataBase context)
		{
			db = context;
		}
		public int GetCount()
		{
			return db.Microchips.Count();
		}
		public Task<List<Microchips>> SelectAll()
		{
			var query = db.Microchips
			.Include(c => c.Kind)
			.Include(c => c.Type)
			.Include(c => c.Manufacturer)
			.ThenInclude(c => c.Country)
			.ThenInclude(c => c.Foreignness)
			.AsQueryable();
			var items = query
							.Select(c => c);
			return items.ToListAsync();
		}
	}
}
