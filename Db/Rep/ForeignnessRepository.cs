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
		public Task<List<Foreignness>> SelectAsObj((Dictionary<string, object> pairs, List<int> ids) dict = default)
		{
			var query = db.Foreignnesses.AsQueryable();
			var items = query
							.Select(c => c);
			return items.ToListAsync();
		}

		public async Task<List<Dictionary<string, object>>> SelectAsDict((Dictionary<string, object> pairs, List<int> ids) dict = default)
		{
			var query = db.Foreignnesses.AsQueryable();
			var result = new List<Dictionary<string, object>>();
			var props = typeof(Foreignness).GetProperties()
				.Where(p => !Attribute.IsDefined(p, typeof(NotMappedAttribute)))
				.ToArray();

			await foreach (var c in query.AsAsyncEnumerable())
			{

				var dictItem = new Dictionary<string, object>();
				foreach (var prop in props)
				{
					dictItem[prop.Name] = prop.GetValue(c);
				}

				result.Add(dictItem);
			}

			return result;
		}
		public async Task<Foreignness> SelectOne(Dictionary<string, string> dict = null)
		{
			if (dict != null && !dict.ContainsKey("foreign_type"))
			{
				return null;
			}
			string type = dict["foreign_type"];
			var foreignnesses = db.Foreignnesses.AsQueryable();
			var native = foreignnesses.Where(f => f.ForeignName.ToLower() == type).FirstAsync();

			return await native;
		}
		public async Task<List<Foreignness>> Select(Dictionary<string, string> dict = null)
		{
			if(dict != null && !dict.ContainsKey("foreign_type"))
			{
				return null;
			}
			string type = dict["foreign_type"];
			var foreignnesses = db.Foreignnesses.AsQueryable();
			var native = foreignnesses.Where(f => f.ForeignName.ToLower() == type);

			return await native.ToListAsync();
		}
	}
}
