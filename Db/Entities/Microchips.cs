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
		[IsEqualFilter()]
		public string? RuComponentKind { get { return Kind == null ? null : Kind.RuComponentKind; } }
		[NotMapped]
		[IsEqualFilter()]
		[ChartUsage]
		public string? ManufacturerName { get { return Manufacturer == null ? null : Manufacturer.ManufacturerName; } }
		[NotMapped]
		[IsEqualFilter()]
		public string? EnComponentKind { get { return Kind == null ? null : Kind.EnComponentKind; } }
		[NotMapped]
		[IsEqualFilter()]
		public string? RuComponentType { get { return Type == null ? null : Type.RuComponentType; } }
		[NotMapped]
		[IsEqualFilter()]
		public string? EnComponentType { get { return Type == null ? null : Type.EnComponentType; } }


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
		[IsEqualFilter()]
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
		[IsEqualFilter()]
		public string ComponentName { get; set; }
		[ChartUsage]
		[IsMoreFilter()]
		public double MinVoltage { get; set; }
		[ChartUsage]
		[IsLessFilter()]
		public double MaxVoltage { get; set; }
		[StringLength(450)]
		[IsEqualFilter()]
		public string? Interfaces { get; set; }

		[ChartUsage]
		[IsEqualFilter()]
		public double? Frequency { get; set; }
		[StringLength(450)]
		[ChartUsage]
		[IsEqualFilter()]
		public string? BitDepthValue { get; set; }
		[ChartUsage]
		[IsEqualFilter()]
		public double? ConsumptionCurrent { get; set; }
		[ChartUsage]
		[IsMoreFilter()]
		public double MinOperatingTemperature { get; set; }
		[ChartUsage]
		[IsLessFilter()]
		public double MaxOperatingTemperature { get; set; }
		[ChartUsage]
		[IsEqualFilter()]
		public double? RadiationResistance { get; set; }
		[StringLength(450)]
		[IsEqualFilter()]
		public string? RadiationResistanceI { get; set; }
		[StringLength(450)]
		[ChartUsage]
		[IsEqualFilter()]
		public string? MemoryFormat { get; set; }
		[ChartUsage]
		[IsEqualFilter()]
		public double? SamplingTime { get; set; }
		[StringLength(450)]
		[IsEqualFilter()]
		public string? Qualication { get; set; }
		[StringLength(450)]
		[IsEqualFilter()]
		public string? Package { get; set; }
		[StringLength(450)]
		[IsEqualFilter()]
		public string? Remark1 { get; set; }
		[JsonIgnore]
		public DateTime? Insertion { get; set; }
		public string? InsertionDate { get { return Insertion == null ? null : Insertion.Value.Date.ToString("yyyy-MM-dd"); } }
		[StringLength(450)]
		[IsEqualFilter()]
		public string? SpecificationDoc { get; set; }
		[NotMapped]
		[IsEqualFilter()]
		public string? RuTechnologyName { get { return Technology == null ? null : Technology.RuTechnologyName; } }
		[NotMapped]
		[IsEqualFilter()]
		public string? EnTechnologyName {  get { return Technology == null ? null : Technology.EnTechnologyName; }
		}
	}
}
