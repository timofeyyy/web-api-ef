using app.Db.utils;
using app.Services.Common.attrs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace app.Db.ef
{
	public class Microchips : IComponentModel
	{
		[NotMapped]
		[IsEqualFilter(nameof(RuComponentKind))]
		public string? RuComponentKind { get; set; }
		[NotMapped]
		[IsEqualFilter(nameof(ManufacturerName))]
		[ChartUsage]
		public string? ManufacturerName { get; set; }
		[NotMapped]
		[IsEqualFilter(nameof(EnComponentKind))]
		public string? EnComponentKind { get; set; }
		[NotMapped]
		[IsEqualFilter(nameof(RuTechnologyName))]
		public string? RuTechnologyName { get; set; }
		[NotMapped]
		[IsEqualFilter(nameof(EnTechnologyName))]
		public string? EnTechnologyName { get; set; }
		[NotMapped]
		[JsonIgnore]
		[IsEqualFilter(nameof(RuComponentType))]
		public string? RuComponentType { get; set; }
		[NotMapped]
		[JsonIgnore]
		[IsEqualFilter(nameof(EnComponentType))]
		public string? EnComponentType { get; set; }


		[JsonIgnore]
		public ComponentTypes Type { get; set; }
		[JsonIgnore]
		public ComponentKinds Kind { get; set; }
		[JsonIgnore]
		public Manufacturers Manufacturer { get; set; }
		[JsonIgnore]
		public Technologies Technology { get; set; }







		[JsonIgnore]
		public int? TechnologyName_ID { get; set; }
		[IsEqualFilter(nameof(ID))]
		[Key]
		public int ID { get; set; }
		[JsonIgnore]
		public long? DocID { get; set; }
		[JsonIgnore]
		public int? Type_ID { get; set; }
		[JsonIgnore]
		public int? Kind_ID { get; set; }
		[JsonIgnore]
		public int? ManufacturerName_ID { get; set; }





		[StringLength(450)]
		[IsEqualFilter(nameof(ComponentName))]
		public string ComponentName { get; set; }
		[StringLength(450)]
		[IsEqualFilter(nameof(Interfaces))]
		public string? Interfaces { get; set; }
		[ChartUsage]
		[IsMoreFilter(nameof(MinVoltage))]
		public double MinVoltage { get; set; }
		[ChartUsage]
		[IsLessFilter(nameof(MaxVoltage))]
		public double MaxVoltage { get; set; }
		[ChartUsage]
		[IsEqualFilter(nameof(Frequency))]
		public double? Frequency { get; set; }
		[StringLength(450)]
		[ChartUsage]
		[IsEqualFilter(nameof(BitDepthValue))]
		public string? BitDepthValue { get; set; }
		[ChartUsage]
		[IsEqualFilter(nameof(ConsumptionCurrent))]
		public double? ConsumptionCurrent { get; set; }
		[ChartUsage]
		[IsMoreFilter(nameof(MinOperatingTemperature))]
		public double MinOperatingTemperature { get; set; }
		[ChartUsage]
		[IsLessFilter(nameof(MaxOperatingTemperature))]
		public double MaxOperatingTemperature { get; set; }
		[ChartUsage]
		[IsEqualFilter(nameof(RadiationResistance))]
		public double? RadiationResistance { get; set; }
		[StringLength(450)]
		[IsEqualFilter(nameof(RadiationResistanceI))]
		public string? RadiationResistanceI { get; set; }
		[StringLength(450)]
		[ChartUsage]
		[IsEqualFilter(nameof(MemoryFormat))]
		public string? MemoryFormat { get; set; }
		[ChartUsage]
		[IsEqualFilter(nameof(SamplingTime))]
		public double? SamplingTime { get; set; }
		[StringLength(450)]
		[IsEqualFilter(nameof(Qualication))]
		public string? Qualication { get; set; }
		[StringLength(450)]
		[IsEqualFilter(nameof(Package))]
		public string? Package { get; set; }
		[StringLength(450)]
		[IsEqualFilter(nameof(Remark1))]
		public string? Remark1 { get; set; }
		public DateTime? Insertion { get; set; }
		[StringLength(450)]
		[IsEqualFilter(nameof(SpecificationDoc))]
		public string? SpecificationDoc { get; set; }


		public Microchips() { }

		public Microchips(Microchips m)
		{
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
			Insertion = m.Insertion;
			Package = m.Package;
			SpecificationDoc = m.SpecificationDoc;
		}
	}
}
