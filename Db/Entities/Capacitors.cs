using app.Db.utils;
using app.Services.Common.attrs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace app.Db.ef
{
	public class Capacitors : IComponentModel
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


		[IsEqualFilter(nameof(ComponentName))]
		[StringLength(450)]
		public string ComponentName { get; set; }
		[IsEqualFilter(nameof(OutputType))]
		[StringLength(450)]
		public string? OutputType { get; set; }
		[IsMoreFilter(nameof(MinVoltage))]
		[ChartUsage]
		public double? MinVoltage { get; set; }
		[IsLessFilter(nameof(MaxVoltage))]
		[ChartUsage]
		public double? MaxVoltage { get; set; }
		[IsLessFilter(nameof(MaxCapacity))]
		[ChartUsage]
		public double? MaxCapacity { get; set; }
		[IsMoreFilter(nameof(MinCapacity))]
		[ChartUsage]
		public double? MinCapacity { get; set; }
		[IsMoreFilter(nameof(MinOperatingTemperature))]
		[ChartUsage]
		public double? MinOperatingTemperature { get; set; }
		[IsLessFilter(nameof(MaxOperatingTemperature))]
		[ChartUsage]
		public double? MaxOperatingTemperature { get; set; }
		[IsEqualFilter(nameof(AcceptableCapacityIncrease))]
		[ChartUsage]
		public double? AcceptableCapacityIncrease { get; set; }
		[IsEqualFilter(nameof(AcceptableСapacityReduction))]
		[ChartUsage]
		public double? AcceptableСapacityReduction { get; set; }
		[StringLength(450)]
		[IsEqualFilter(nameof(QualicationSG))]
		public string? QualicationSG { get; set; }
		[StringLength(450)]
		[IsEqualFilter(nameof(QualicationЕС))]
		public string? QualicationЕС { get; set; }
		[StringLength(450)]
		[IsEqualFilter(nameof(Remark1))]
		public string? Remark1 { get; set; }
		[StringLength(450)]
		[IsEqualFilter(nameof(Remark2))]
		public string? Remark2 { get; set; }
		public DateTime? Insertion { get; set; }
		[StringLength(450)]
		[IsEqualFilter(nameof(SpecificationDoc))]
		public string? SpecificationDoc { get; set; }

		public Capacitors() { }
		public Capacitors(Capacitors c)
		{
			ID = c.ID;
			DocID = c.DocID;

			OutputType = c.OutputType;
			ComponentName = c.ComponentName;
			MinVoltage = c.MinVoltage;
			MaxVoltage = c.MaxVoltage;
			MaxCapacity = c.MaxCapacity;
			MinCapacity = c.MinCapacity;
			MinOperatingTemperature = c.MinOperatingTemperature;
			MaxOperatingTemperature = c.MaxOperatingTemperature;
			AcceptableCapacityIncrease = c.AcceptableCapacityIncrease;
			AcceptableСapacityReduction = c.AcceptableСapacityReduction;
			QualicationSG = c.QualicationSG;
			QualicationЕС = c.QualicationЕС;
			Remark1 = c.Remark1;
			Remark2 = c.Remark2;
			Insertion = c.Insertion;
			SpecificationDoc = c.SpecificationDoc;
		}
	}
}
