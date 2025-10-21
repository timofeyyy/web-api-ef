using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.Json.Serialization;

namespace app.Models.other.production
{
	public class ManufacturerProductionModel
	{
		public string ManufacturerName { get; set; }
		public double Weight { get; set; }
		public int Amount { get; set; }
	}
}
