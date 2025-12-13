using app.Db.Context;
using app.Db.ef;
using app.Db.utils;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace app.Db.Rep
{
	public class ResistorRepository : IRepositoryBase<Resistors>
	{
		DataBase db;
		public ResistorRepository(DataBase context)
		{
			db = context;
		}
		public int GetCount()
		{
			return db.Resistors.Count();
		}
		public Task<List<Resistors>> SelectAll()
		{
			var query = db.Resistors
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
