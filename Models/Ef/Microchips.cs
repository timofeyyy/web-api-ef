using app.Entities;
using app.src1.attrs;
using app.src1.interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text.Json.Serialization;

namespace app.Models.Ef
{
    public class Microchips : IComponentModel
	{
		[NotMapped]
		public string? RuComponentKind { get; set; }
		[NotMapped]
		public string? ManufacturerName { get; set; }
		[NotMapped]
		public string? EnComponentKind { get; set; }
		[NotMapped]
		public string? RuTechnologyName { get; set; }
		[NotMapped]
		public string? EnTechnologyName { get; set; }
		[Key]
		//[JsonIgnore]
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
		[ChartUsage]
        public double MinVoltage { get; set; }
		[ChartUsage]
        public double MaxVoltage { get; set; }
		[ChartUsage]
        public double? Frequency { get; set; }
		[ChartUsage]
        public string? BitDepthValue { get; set; }
		[ChartUsage]
        public double? ConsumptionCurrent { get; set; }
		[NotMapped]
		[JsonIgnore]
		public int? TechnologyName_ID { get; set; }
		[JsonIgnore]
		public Technologies Technology { get; set; }
		[ChartUsage]
        public double MinOperatingTemperature { get; set; }
		[ChartUsage]
        public double MaxOperatingTemperature { get; set; }
		[ChartUsage]
        public double? RadiationResistance { get; set; }
        public string? RadiationResistanceI { get; set; }
		[ChartUsage]
        public string? MemoryFormat { get; set; }
		[ChartUsage]
        public double? SamplingTime { get; set; }
        public string? Qualication { get; set; }
        public string? Remark1 { get; set; }
		public DateTime? Date { get; set; }
		[NotMapped]
		//[JsonIgnore]
		public string? RuComponentType { get; set; }
		[NotMapped]
		//[JsonIgnore]
		public string? EnComponentType { get; set; }
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
			Date = m.Date;
		}
	}
}
