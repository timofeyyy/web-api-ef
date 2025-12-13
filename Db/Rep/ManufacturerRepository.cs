using app.Db.Context;
using app.Db.ef;
using app.Db.utils;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace app.Db.Rep
{
	public class ManufacturerRepository: IRepositoryBase<Manufacturers>
	{
		DataBase db;
		public ManufacturerRepository(DataBase context) {
			db = context;
		}

		public int GetCount()
		{
			return db.Manufacturers.Count();
		}

		public Task<List<Manufacturers>> SelectAll()
		{
			var query = db.Manufacturers
			.Include(m => m.Country)
			.ThenInclude(c => c.Foreignness)
			.AsQueryable();
			return query.ToListAsync();
		}
	}
}
