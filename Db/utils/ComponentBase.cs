namespace app.Db.utils
{
	public class ComponentBase<T> where T : IComponentModel
	{
		//public abstract Task<List<T>> Select(Dictionary<string, string> dict);
	
		public IQueryable<T> FilterByParamValue(Dictionary<string, object> dict, IQueryable<T> query)
		{
			if (dict != null && dict.ContainsKey("RuComponentType") && dict["RuComponentType"] != null)
			{
				query = query.Where(c => c.Type.RuComponentType.ToLower() == dict["RuComponentType"].ToString().ToLower());
			}
			if (dict != null && dict.ContainsKey("RuComponentKind") && dict["RuComponentKind"] != null)
			{
				query = query.Where(c => c.Kind.RuComponentKind.ToLower() == dict["RuComponentKind"].ToString().ToLower());
			}
			if (dict != null && dict.ContainsKey("EnComponentType") && dict["EnComponentType"] != null)
			{
				query = query.Where(c => c.Type.EnComponentType.ToLower() == dict["EnComponentType"].ToString().ToLower());
			}
			if (dict != null && dict.ContainsKey("EnComponentKind") && dict["EnComponentKind"] != null)
			{
				query = query.Where(c => c.Kind.EnComponentKind.ToLower() == dict["EnComponentKind"].ToString().ToLower());
			}
			if (dict != null && dict.ContainsKey("ManufacturerName") && dict["ManufacturerName"] != null)
			{
				query = query.Where(c => c.Manufacturer.ManufacturerName.ToLower() == dict["ManufacturerName"].ToString().ToLower());
			}
			if (dict != null && dict.ContainsKey("ComponentName") && dict["ComponentName"] != null)
			{
				query = query.Where(c => c.ComponentName.ToLower() == dict["ComponentName"].ToString().ToLower());
			}
			if (dict != null && dict.ContainsKey("Id") && dict["Id"] != null)
			{
				query = query.Where(c => c.ID == (int)dict["Id"]);
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
	}
}
