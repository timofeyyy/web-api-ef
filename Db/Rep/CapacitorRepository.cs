using app.Context;
using app.db;
using app.Entities;
using app.Models.Ef;
using app.src1.interfaces;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Pdf.Filters;
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

		public Task<List<Capacitors>> Select(Dictionary<string, string> dict = null)
		{
			var query = db.Capacitors.AsQueryable();
			query = FilterByParamValue(dict, query);
			var items = query
							.Select(c => new Capacitors(c)
							{
								RuComponentKind = c.Kind.RuComponentKind,
								EnComponentKind = c.Kind.RuComponentKind,
								RuComponentType = c.Type.RuComponentType,
								EnComponentType = c.Type.EnComponentType,
								ManufacturerName = c.Manufacturer.ManufacturerName,
							});
			return items.ToListAsync();
		}
		int _count;
		public int GetCount()
		{
			return db.Capacitors.Count();
		}
		public override Task<List<IComponentModel>> SelectPreviewByParamValue(Dictionary<string, string> dict)
		{
			var query = db.Capacitors.AsQueryable();
			query = FilterByParamValue(dict, query);
			var items = SelectComponentPreview(query);
			return items.ToListAsync();
		}
		public override Task<List<IComponentModel>> SelectPreviewByListIds(List<int> ids)
		{
			var query = db.Capacitors.AsQueryable();
			query = FilterByIds(ids, query);
			var items = SelectComponentPreview(query);
			return items.ToListAsync();
		}

		public List<Dictionary<string, object>> ToDictionary(List<Capacitors> components)
		{
			List<Dictionary<string, object>> keyValuePairs = new List<Dictionary<string, object>>();
			var dictionary = new Dictionary<string, object>();

			foreach (var component in components)
			{
				keyValuePairs.Add(ObjToDictionary(component));
			}
			return keyValuePairs;
		}

		public Dictionary<string, object> ObjToDictionary(Capacitors obj)
		{

			var dictionary = new Dictionary<string, object>();
			PropertyInfo[] properties = obj.GetType().GetProperties(); 
			
			foreach (PropertyInfo property in properties)
			{
				dictionary.Add(property.Name, property.GetValue(obj));
			}

			return dictionary;
		}
		public override Task<List<Capacitors>> SelectByListIds(List<int> ids)
		{
			var query = db.Capacitors.AsQueryable();
			query = FilterByIds(ids, query);
			var items = query.Select(c => new Capacitors(c)
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
