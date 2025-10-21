using app.Context;
using app.db;
using app.Entities;
using app.Models.Ef;
using app.src1.interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace app.Db.Rep
{
	public class DiodRepository : ComponentBase<Diods>, IRepositoryBase<Diods>
	{
		DataBase db;
		public DiodRepository(DataBase context)
		{
			db = context;
		}
		public Task<List<Diods>> Select(Dictionary<string, string> dict = null)
		{
			var query = db.Diods.AsQueryable();
			query = FilterByParamValue(dict, query);
			var items = query
							.Select(c => new Diods(c)
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
			return db.Diods.Count();
		}
		public override Task<List<IComponentModel>> SelectPreviewByParamValue(Dictionary<string, string> dict)
		{
			var query = db.Diods.AsQueryable();
			query = FilterByParamValue(dict, query);
			var items = SelectComponentPreview(query);
			return items.ToListAsync();
		}
		public override Task<List<IComponentModel>> SelectPreviewByListIds(List<int> ids)
		{
			var query = db.Diods.AsQueryable();
			query = FilterByIds(ids, query);
			var items = SelectComponentPreview(query);
			return items.ToListAsync();
		}
		public override Task<List<Diods>> SelectByListIds(List<int> ids)
		{
			var query = db.Diods.AsQueryable();
			query = FilterByIds(ids, query);
			var items = query.Select(c => new Diods(c)
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
