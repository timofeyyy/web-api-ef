using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.Json.Serialization;

namespace app.Entities
{
    public class Transistors
    {
		static public string Name { get { return "transistors"; } }

		[Key]
        public int ID { get; set; }
        public long? DocID { get; set; }
 
        [StringLength(450)]
        public string ComponentName { get; set; }
		public int? Type_ID { get; set; }
		[JsonIgnore]
		public ComponentTypes Type { get; set; }
		public int? Kind_ID { get; set; }
		[JsonIgnore]
		public ComponentKinds Kind { get; set; }
        public int? ManufacturerName_ID { get; set; }
		[JsonIgnore]
        public Manufacturers Manufacturer { get; set; }
        public double? MaxPermissibleDCVoltage { get; set; }
        public double? MinOperatingTemperature { get; set; }
        public double? MaxOperatingTemperature { get; set; }
        public double? MaxPermissibleDCCollectorCurrent { get; set; }
        public double? RadiationResistance { get; set; }
        public string? RadiationResistanceI { get; set; }
        public string? QualicationSG { get; set; }
        public string? QualicationЕС { get; set; }
        public string? Package { get; set; }
        public string? Remark1 { get; set; }
        public string? Remark2 { get; set; }
    }
}
