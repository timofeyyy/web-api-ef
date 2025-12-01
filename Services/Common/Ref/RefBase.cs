using System.Reflection;

namespace app.Services.Common.Ref
{
	public class RefBase : IRefBase
	{
		public PropertyInfo[] PropsByAttrs(Type t, List<(Type t, bool shoudHave)> exceptionsAttr = null)
		{
			return t.GetProperties().Where(prop =>
			{
				bool flag = true;
				foreach (var exception in exceptionsAttr)
				{
					if (Attribute.IsDefined(prop, exception.t) != exception.shoudHave)
					{
						flag = false;
						break;
					}
				}
				return flag;
			}).ToArray();
		}
	}
}
