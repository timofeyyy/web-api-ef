using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text.Json.Serialization;

namespace app.Models.other
{
	public class ComponentPreview
	{
		public string? RuComponentType { get; set; }
		public string? RuComponentKind { get; set; }
		public string? ManufacturerName { get; set; }
		public string? EnComponentType { get; set; }
		public string? EnComponentKind { get; set; }
		public string? ComponentName { get; set; }

	}
}
