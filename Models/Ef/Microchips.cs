using app.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text.Json.Serialization;

namespace app.Models.Ef
{
    public class Microchips
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
		[NotMapped]
		public string? RuTechnologyName { get; set; }
		[NotMapped]
		public string? EnTechnologyName { get; set; }
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
		public string? Interfaces { get; set; }
        public double MinVoltage { get; set; }
        public double MaxVoltage { get; set; }
        public double? Frequency { get; set; }
        public string? BitDepthValue { get; set; }
        public double? ConsumptionCurrent { get; set; }
		[NotMapped]
		public int? TechnologyName_ID { get; set; }
		[JsonIgnore]
		public Technologies Technology { get; set; }
        public double MinOperatingTemperature { get; set; }
        public double MaxOperatingTemperature { get; set; }
        public double? RadiationResistance { get; set; }
        public string? RadiationResistanceI { get; set; }
        public string? MemoryFormat { get; set; }
        public double? SamplingTime { get; set; }
        public string? Qualication { get; set; }
        public string? Remark1 { get; set; }

		public Microchips() { }

		public Microchips(Microchips m) {
			ID = m.ID;
			DocID = m.DocID;
	
		
			BitDepthValue = m.BitDepthValue;
			ComponentName = m.ComponentName;
			ConsumptionCurrent = m.ConsumptionCurrent;
			Interfaces = m.Interfaces;
			MinVoltage = m.MinVoltage;
			MaxVoltage = m.MaxVoltage;
			Frequency = m.Frequency;
			MinOperatingTemperature = m.MinOperatingTemperature;
			MaxOperatingTemperature = m.MaxOperatingTemperature;
			RadiationResistance = m.RadiationResistance;
			RadiationResistanceI = m.RadiationResistanceI;
			MemoryFormat = m.MemoryFormat;
			SamplingTime = m.SamplingTime;
			Qualication = m.Qualication;
			Remark1 = m.Remark1;
		}
	}
}
