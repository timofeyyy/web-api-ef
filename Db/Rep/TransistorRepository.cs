using app.Context;
using app.db;
using app.Models.Ef;
using app.src1.interfaces;
using Microsoft.EntityFrameworkCore;
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
		public Task<List<Transistors>> Select(Dictionary<string, string> dict = null)
		{
			var query = db.Transistors.AsQueryable();
			query = FilterByParamValue(dict, query);
			var items = query
							.Select(c => new Transistors(c)
							{
								RuComponentKind = c.Kind.RuComponentKind,
								EnComponentKind = c.Kind.RuComponentKind,
								RuComponentType = c.Type.RuComponentType,
								EnComponentType = c.Type.EnComponentType,
								ManufacturerName = c.Manufacturer.ManufacturerName,
							});
			return items.ToListAsync();
		}
		public int GetCount()
		{
			return db.Transistors.Count();
		}
		public override Task<List<IComponentModel>> SelectPreviewByParamValue(Dictionary<string, string> dict)
		{
			var query = db.Transistors.AsQueryable();
			query = FilterByParamValue(dict, query);
			var items = SelectComponentPreview(query);
			return items.ToListAsync();
		}
		public override Task<List<IComponentModel>> SelectPreviewByListIds(List<int> ids)
		{
			var query = db.Transistors.AsQueryable();
			query = FilterByIds(ids, query);
			var items = SelectComponentPreview(query);
			return items.ToListAsync();
		}
		public override Task<List<Transistors>> SelectByListIds(List<int> ids)
		{
			var query = db.Transistors.AsQueryable();
			query = FilterByIds(ids, query);
			var items = query.Select(c => new Transistors(c)
			{
				RuComponentKind = c.Kind.RuComponentKind,
				EnComponentKind = c.Kind.RuComponentKind,
				RuComponentType = c.Type.RuComponentType,
				EnComponentType = c.Type.EnComponentType,
				ManufacturerName = c.Manufacturer.ManufacturerName,
			});

			return items.ToListAsync();
		}
	}
}
