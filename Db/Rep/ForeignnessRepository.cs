using app.Context;
using app.Models.ef;
using app.Models.Ef;
using app.src1.interfaces;
using Microsoft.EntityFrameworkCore;

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
