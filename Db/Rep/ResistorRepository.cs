using app.Context;
using app.db;
using app.Entities;
using app.Models.Ef;
using app.src1.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Text.Json.Serialization;

namespace app.Db.Rep
{
	public class ResistorRepository : ComponentBase<Resistors>, IRepositoryBase<Resistors>
	{
		DataBase db;
		public ResistorRepository(DataBase context)
		{
			db = context;
		}
		public Task<List<Resistors>> Select(Dictionary<string, string> dict = null)
		{
			var query = db.Resistors.AsQueryable();
			query = FilterByParamValue(dict, query);
			var items = query
							.Select(c => new Resistors(c)
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
			return db.Resistors.Count();
		}
		public override Task<List<IComponentModel>> SelectPreviewByParamValue(Dictionary<string, string> dict)
		{
			var query = db.Resistors.AsQueryable();
			query = FilterByParamValue(dict, query);
			var items = SelectComponentPreview(query);
			return items.ToListAsync();
		}
		public override Task<List<IComponentModel>> SelectPreviewByListIds(List<int> ids)
		{
			var query = db.Resistors.AsQueryable();
			query = FilterByIds(ids, query);
			var items = SelectComponentPreview(query);
			return items.ToListAsync();
		}
		public override Task<List<Resistors>> SelectByListIds(List<int> ids)
		{
			var query = db.Resistors.AsQueryable();
			query = FilterByIds(ids, query);
			var items = query.Select(c => new Resistors(c)
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
