using app.Entities;
using app.Models.Ef;
using app.src1.interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text.Json.Serialization;

namespace app.Models.other.component
{
	public class ComponentPreviewModel : IComponentModel
	{
		//public string? RuComponentType { get; set; }
		//public string? RuComponentKind { get; set; }
		//public string? ManufacturerName { get; set; }
		//public string? EnComponentType { get; set; }
		//public string? EnComponentKind { get; set; }
		public string? ComponentName { get; set; }
		public ComponentTypes Type { get; set; }
		public ComponentKinds Kind { get; set; }
		public Manufacturers Manufacturer { get; set; }
		public int ID { get; set; }
	}
}
