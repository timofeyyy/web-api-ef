using app.Db.Context;
using app.Db.ef;
using app.Db.utils;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace app.Db.Rep
{
	public class TransistorRepository : IRepositoryBase<Transistors>
	{
		DataBase db;
		public TransistorRepository(DataBase context)
		{
			db = context;
		}
		public int GetCount()
		{
			return db.Transistors.Count();
		}

		public Task<List<Transistors>> SelectAll()
		{
			var query = db.Transistors
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
