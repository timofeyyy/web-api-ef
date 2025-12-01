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
        public string ManufacturerName { get; set; }
		[JsonIgnore]
		public int? CountryID { get; set; }
		[JsonIgnore]
		public Country Country { get; set; }
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
		public string CountryName { get; set; }
		[NotMapped]
		public string ForeignnessType{ get; set; }
		public Manufacturers() { }
		public Manufacturers(Manufacturers m)
		{
			ManufacturerName = m.ManufacturerName;
			ID = m.ID;
			CountryID = m.CountryID;
		}
	}
}
