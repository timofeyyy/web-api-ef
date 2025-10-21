using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace app.Models.ef
{
	public class Foreignness
	{
		[Key]
		[JsonIgnore]
		public int ID { get; set; }
		[StringLength(100)]
		public string ForeignName { get; set; }
		[JsonIgnore]
		public List<Country> Countries { get; set; }
	}
}
