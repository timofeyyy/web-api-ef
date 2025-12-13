using app.Db.Context;
using app.Db.ef;
using app.Db.utils;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Pdf.Filters;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Security.Principal;
using System.Text.Json.Serialization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace app.Db.Rep
{
	public class CapacitorRepository : ComponentBase<Capacitors>, IRepositoryBase<Capacitors>
	{
		DataBase db;
		public CapacitorRepository(DataBase context) {
			db = context;
		}

		public Task<List<Capacitors>> SelectAll()
		{
			var query = db.Capacitors
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
		public int GetCount()
		{
			return db.Capacitors.Count();
		}
	}
}
