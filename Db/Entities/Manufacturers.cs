using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace app.Db.ef
{
	public class Manufacturers
	{
		[Key]
		public int ID { get; set; }

		[StringLength(450)]
		public string? ManufacturerName { get; set; }
		[JsonIgnore]
		public int? CountryID { get; set; }
		[JsonIgnore]
		public Country? Country { get; set; }
		[JsonIgnore]
		public List<Capacitors> Capacitors { get; set; }
		[JsonIgnore]
		public List<Resistors> Resistors { get; set; }
		[JsonIgnore]
		public List<Diods> Diods { get; set; }
		[JsonIgnore]
		public List<Microchips> Microchips { get; set; }
		[JsonIgnore]
		public List<Transistors> Transistors { get; set; }
		[NotMapped]
		public string? CountryName { get { return Country == null ? null : Country.CountryName; } }
		[NotMapped]
		public string? ForeignnessType { get { return Country == null ? null : Country.Foreignness == null ? null : Country.Foreignness.ForeignName; } }
	}

	//public static class ManufacturersExtension {
	//	public static List<KeyValueObject ToOneDictionary(this List<Manufacturers> manufacturers)
	//	{
	//		KeyValueObject res = new();
	//		foreach (var manufacturer in manufacturers)
	//		{
	//			res[manufacturer.ManufacturerName] = new();
	//			dict[manufacturerName]["country"] = (string)manufacturer["CountryName"];
	//			dict[manufacturerName]["foreignness"] = (string)manufacturer["ForeignnessType"];
	//		}
	//	} 
	//}
}
