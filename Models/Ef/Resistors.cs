using app.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text.Json.Serialization;

namespace app.Models.Ef
{
    public class Resistors
    {
		[NotMapped]
		public string? RuComponentKind { get; set; }
		[NotMapped]
		public string? ManufacturerName { get; set; }
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
		[NotMapped]
		public string? RuComponentType { get; set; }
		[NotMapped]
		public string? EnComponentType { get; set; }
		public Resistors() { }
        public Resistors(Resistors r) {
			ID = r.ID;
			DocID = r.DocID;
			PowerRating = r.PowerRating;
			ComponentName = r.ComponentName;
			MinVoltage = r.MinVoltage;
			MaxVoltage = r.MaxVoltage;
			MinRatedResistance = r.MinRatedResistance;
			MaxRatedResistance = r.MaxRatedResistance;
			ResistanceTolerance = r.ResistanceTolerance;
			MinOperatingTemperature = r.MinOperatingTemperature;
			MaxOperatingTemperature = r.MaxOperatingTemperature;
			CurrentLimit = r.CurrentLimit;
			QualicationSG = r.QualicationSG;
			QualicationЕС = r.QualicationЕС;
			Package = r.Package;
			Remark1 = r.Remark1;
			Remark2 = r.Remark2;
		}
    }
}
