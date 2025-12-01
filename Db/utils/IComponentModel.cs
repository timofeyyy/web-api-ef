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
		public string? RuComponentKind { get; set; }
		public string? ManufacturerName { get; set; }
		public string? EnComponentKind { get; set; }
		public string? RuComponentType { get; set; }
		public string? EnComponentType { get; set; }
	}
}
