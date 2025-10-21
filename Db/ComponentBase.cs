using app.Models.other;
using app.Models.other.component;
using app.src1.interfaces;

namespace app.db
{
	public abstract class ComponentBase<T> where T : IComponentModel
	{
		//public abstract Task<List<T>> Select(Dictionary<string, string> dict);
	
		public IQueryable<T> FilterByParamValue(Dictionary<string, string> dict, IQueryable<T> query)
		{
			if (dict != null && dict.ContainsKey("RuComponentType") && dict["RuComponentType"] != null)
			{
				query = query.Where(c => c.Type.RuComponentType.ToLower() == dict["RuComponentType"].ToLower());
			}
			if (dict != null && dict.ContainsKey("RuComponentKind") && dict["RuComponentKind"] != null)
			{
				query = query.Where(c => c.Kind.RuComponentKind.ToLower() == dict["RuComponentKind"].ToLower());
			}
			if (dict != null && dict.ContainsKey("ManufacturerName") && dict["ManufacturerName"] != null)
			{
				query = query.Where(c => c.Manufacturer.ManufacturerName.ToLower() == dict["ManufacturerName"].ToLower());
			}
			if (dict != null && dict.ContainsKey("ComponentName") && dict["ComponentName"] != null)
			{
				query = query.Where(c => c.ComponentName.ToLower() == dict["ComponentName"].ToLower());
			}
			return query;
		}
		public IQueryable<T> FilterByIds(List<int> ids, IQueryable<T> query)
		{
			if (ids != null) { 
			
				query = query.Where(c => ids.Contains(c.ID));
			}
			return query;
		}

		//public abstract int GetCount();

		public abstract Task<List<IComponentModel>> SelectPreviewByParamValue(Dictionary<string, string> dict);
		public abstract Task<List<IComponentModel>> SelectPreviewByListIds(List<int> ids);
		public abstract Task<List<T>> SelectByListIds(List<int> ids);
		public IQueryable<IComponentModel> SelectComponentPreview(IQueryable<T> query) {
			var items = query
								.Select(m => new ComponentPreviewModel()
								{
									Manufacturer = m.Manufacturer,
									Kind = m.Kind,
									Type = m.Type,
									ComponentName = m.ComponentName
								});
			return items;
		}
	}
}
