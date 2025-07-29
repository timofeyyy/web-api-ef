using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.Json.Serialization;

namespace app.Entities
{
	public class ManufacturerProduction
	{
		public string ManufacturerName { get; set; }
		public int Count { get; set; }
	}
}
