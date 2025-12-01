using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace app.Db.ef
{
	public class Country
	{
		[Key]
		[JsonIgnore]
		public int ID { get; set; }
		[StringLength(100)]
		public string CountryName { get; set; }
		[JsonIgnore]
		public int? ForeignNative { get; set; }
		[JsonIgnore]
		public Foreignness Foreignness { get; set; }
		[JsonIgnore]
		public List<Manufacturers> Manufacturers { get; set; }
	}
}
