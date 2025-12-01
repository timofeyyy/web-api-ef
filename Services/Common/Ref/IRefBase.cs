using System.Reflection;

namespace app.Services.Common.Ref
{
	public interface IRefBase
	{
		public PropertyInfo[] PropsByAttrs(Type t, List<(Type t, bool shoudHave)> exceptionsAttr = null);
	}
}
