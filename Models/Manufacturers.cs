using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace app.Entities
{
    public class Manufacturers
    {
        [Key]
        public int ID { get; set; }

        [StringLength(450)]
        public string ManufacturerName { get; set; }
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
	}
}
