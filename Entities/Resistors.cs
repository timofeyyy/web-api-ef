using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.Json.Serialization;

namespace app.Entities
{
    public class Resistors
    {
		static public string Name { get { return "resistors"; } }

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
		public double? PowerRating { get; set; }
        public double? MinVoltage { get; set; }
        public double? MaxVoltage { get; set; }
        public double? MinRatedResistance { get; set; }
        public double? MaxRatedResistance { get; set; }
        public double? ResistanceTolerance { get; set; }
        public double? MinOperatingTemperature { get; set; }
        public double? MaxOperatingTemperature { get; set; }
        public double? CurrentLimit { get; set; }
        public string? QualicationSG { get; set; }
        public string? QualicationЕС { get; set; }
        public string? Package { get; set; }
        public string? Remark1 { get; set; }
        public string? Remark2 { get; set; }
    }
}
