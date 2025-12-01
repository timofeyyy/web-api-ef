using app.Db.Context;
using app.Db.ef;
using app.Db.utils;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace app.Db.Rep
{
	public class TransistorRepository : ComponentBase<Transistors>, IRepositoryBase<Transistors>
	{
		DataBase db;
		public TransistorRepository(DataBase context)
		{
			db = context;
		}
		public Task<List<Transistors>> SelectAsObj((Dictionary<string, object> pairs, List<int> ids) parameters = default)
		{
			var query = db.Transistors
				.Include(c => c.Kind)
				.Include(c => c.Type)
				.Include(c => c.Manufacturer)
				.AsQueryable();
			if (parameters.pairs != null)
			{
				query = FilterByParamValue(parameters.pairs, query);
			}
			if(parameters.ids != null)
			{
				query = FilterByIds(parameters.ids, query);
			}
			var items = query
							.Select(c => new Transistors(c)
							{								
								RuComponentKind = c.Kind.RuComponentKind,
								EnComponentKind = c.Kind.EnComponentKind,
								RuComponentType = c.Type.RuComponentType,
								EnComponentType = c.Type.EnComponentType,
								ManufacturerName = c.Manufacturer.ManufacturerName
							});
			return items.ToListAsync();
		}

		public async Task<List<Dictionary<string, object>>> SelectAsDict((Dictionary<string, object> pairs, List<int> ids) parameters = default)
		{
			var query = db.Transistors.AsQueryable();
			if (parameters.pairs != null)
			{
				query = FilterByParamValue(parameters.pairs, query);
			}
			if (parameters.ids != null)
			{
				query = FilterByIds(parameters.ids, query);
			}
			var result = new List<Dictionary<string, object>>();

			var props = typeof(Transistors).GetProperties()
				.Where(p => !Attribute.IsDefined(p, typeof(NotMappedAttribute)))
				.ToArray();

			await foreach (var c in query.AsAsyncEnumerable())
			{
				var cap = new Transistors(c)
				{
					RuComponentKind = c.Kind.RuComponentKind,
					EnComponentKind = c.Kind.EnComponentKind,
					RuComponentType = c.Type.RuComponentType,
					EnComponentType = c.Type.EnComponentType,
					ManufacturerName = c.Manufacturer.ManufacturerName
				};

				var dictItem = new Dictionary<string, object>();
				foreach (var prop in props)
				{
					dictItem[prop.Name] = prop.GetValue(cap);
				}

				result.Add(dictItem);
			}

			return result;
		}
		public int GetCount()
		{
			return db.Transistors.Count();
		}
	}
}
