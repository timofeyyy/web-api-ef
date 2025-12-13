using app.Db.ef;

namespace app.Db.utils
{
	public interface IComponentModel
	{
		public int ID { get; set; }
		public string ComponentName { get; set; }
		public ComponentTypes Type { get; set; }
		public ComponentKinds Kind { get; set; }
		public Manufacturers Manufacturer { get; set; }
		public string? RuComponentKind { get; }
		public string? ManufacturerName { get; }
		public string? EnComponentKind { get; }
		public string? RuComponentType { get; }
		public string? EnComponentType { get; }
	}
}
