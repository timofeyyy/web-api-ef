using app.Context;
using app.db;
using app.Models.Ef;
using app.src1.interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace app.Db.Rep
{
	public class MicrochipsRepository : ComponentBase<Microchips>, IRepositoryBase<Microchips>
	{
		DataBase db;
		public MicrochipsRepository(DataBase context)
		{
			db = context;
		}

		public int GetCount()
		{
			return db.Microchips.Count();
		}

		public Task<List<Microchips>> Select(Dictionary<string, string> dict = null)
		{
			var query = db.Microchips.AsQueryable();
			query = FilterByParamValue(dict, query);
			var items = query
							.Select(c => new Microchips(c)
							{
								RuComponentKind = c.Kind.RuComponentKind,
								EnComponentKind = c.Kind.RuComponentKind,
								RuComponentType = c.Type.RuComponentType,
								EnComponentType = c.Type.EnComponentType,
								ManufacturerName = c.Manufacturer.ManufacturerName,
								EnTechnologyName = c.Technology.EnTechnologyName,
								RuTechnologyName = c.Technology.RuTechnologyName
							});
			return items.ToListAsync();
		}
		public override Task<List<IComponentModel>> SelectPreviewByParamValue(Dictionary<string, string> dict)
		{
			var query = db.Microchips.AsQueryable();
			query = FilterByParamValue(dict, query);
			var items = SelectComponentPreview(query);
			return items.ToListAsync();
		}
		public override Task<List<IComponentModel>> SelectPreviewByListIds(List<int> ids)
		{
			var query = db.Microchips.AsQueryable();
			query = FilterByIds(ids, query);
			var items = SelectComponentPreview(query);
			return items.ToListAsync();

		}
		public override Task<List<Microchips>> SelectByListIds(List<int> ids)
		{
			var query = db.Microchips.AsQueryable();
			query = FilterByIds(ids, query);
			var items = query.Select(c => new Microchips(c)
			{
				RuComponentKind = c.Kind.RuComponentKind,
				EnComponentKind = c.Kind.RuComponentKind,
				RuComponentType = c.Type.RuComponentType,
				EnComponentType = c.Type.EnComponentType,
				ManufacturerName = c.Manufacturer.ManufacturerName,
				EnTechnologyName = c.Technology.EnTechnologyName,
				RuTechnologyName = c.Technology.RuTechnologyName
			});

			return items.ToListAsync();
		}
	}
}
