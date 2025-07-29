using app.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text.Json.Serialization;

namespace app.Models.Ef
{
    public class Transistors
    {
		[NotMapped]
		public string? RuComponentType { get; set; }
		[NotMapped]
		public string? RuComponentKind { get; set; }
		[NotMapped]
		public string? ManufacturerName { get; set; }
		[NotMapped]
		public string? EnComponentType { get; set; }
		[NotMapped]
		public string? EnComponentKind { get; set; }

		[Key]
		[JsonIgnore]
		public int ID { get; set; }
		[JsonIgnore]
		public long? DocID { get; set; }
 
        [StringLength(450)]
        public string ComponentName { get; set; }
		[JsonIgnore]
		public int? Type_ID { get; set; }
		[JsonIgnore]
		public ComponentTypes Type { get; set; }
		[JsonIgnore]
		public int? Kind_ID { get; set; }
		[JsonIgnore]
		public ComponentKinds Kind { get; set; }
		[JsonIgnore]
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

        public Transistors() { }
        public Transistors(Transistors t) {
			ID = t.ID;
			DocID = t.DocID;
			MaxPermissibleDCVoltage = t.MaxPermissibleDCVoltage;
			ComponentName = t.ComponentName;
			MinOperatingTemperature = t.MinOperatingTemperature;
			MaxOperatingTemperature = t.MaxOperatingTemperature;
			MaxPermissibleDCCollectorCurrent = t.MaxPermissibleDCCollectorCurrent;
			RadiationResistance = t.RadiationResistance;
			RadiationResistanceI = t.RadiationResistanceI;
			QualicationSG = t.QualicationSG;
			QualicationЕС = t.QualicationЕС;
			Package = t.Package;
			Remark1 = t.Remark1;
			Remark2 = t.Remark2;
		}
    }
}
