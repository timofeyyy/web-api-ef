using app.Db.utils;
using app.Services.Common.attrs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace app.Db.ef
{
	public class Diods : IComponentModel
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



		[JsonIgnore]
		public ComponentTypes Type { get; set; }
		[JsonIgnore]
		public ComponentKinds Kind { get; set; }
		[JsonIgnore]
		public Manufacturers Manufacturer { get; set; }



		[IsEqualFilter()]
		[StringLength(450)]
		public string ComponentName { get; set; }
		[IsLessFilter()]
		[ChartUsage]
		public double? MaxPermissibleDCVoltage { get; set; }
		[IsMoreFilter()]
		[ChartUsage]
		public double? MinOperatingTemperature { get; set; }
		[IsLessFilter()]
		[ChartUsage]
		public double? MaxOperatingTemperature { get; set; }
		[IsLessFilter()]
		[ChartUsage]
		public double? MaxPermissibleAverageDirectCurrent { get; set; }
		[IsLessFilter()]
		[ChartUsage]
		public double? MaxiPermissibleDirectCurrent { get; set; }
		[IsEqualFilter()]
		[ChartUsage]
		public double? RadiationResistance { get; set; }
		[StringLength(450)]
		[IsEqualFilter()]
		public string? RadiationResistanceI { get; set; }
		[StringLength(450)]
		[IsEqualFilter()]
		public string? QualicationSG { get; set; }
		[StringLength(450)]
		[IsEqualFilter()]
		public string? QualicationЕС { get; set; }
		[StringLength(450)]
		[IsEqualFilter()]
		[ChartUsage]
		public string? Package { get; set; }
		[StringLength(450)]
		[IsEqualFilter()]
		public string? Remark1 { get; set; }
		[StringLength(450)]
		[IsEqualFilter()]
		public string? Remark2 { get; set; }
		[JsonIgnore]
		public DateTime? Insertion { get; set; }
		public string? InsertionDate { get { return Insertion == null ? null : Insertion.Value.Date.ToString("yyyy-MM-dd"); } }
		[StringLength(450)]
		[IsEqualFilter()]
		public string? SpecificationDoc { get; set; }
	}
}
