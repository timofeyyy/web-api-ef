using app.Db.utils;
using app.Services.Common.attrs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace app.Db.ef
{
	public class Resistors : IComponentModel
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
		//[JsonIgnore]
		[IsEqualFilter(nameof(RuComponentType))]
		public string? RuComponentType { get; set; }
		[NotMapped]
		//[JsonIgnore]
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
		[IsEqualFilter(nameof(PowerRating))]
		[ChartUsage]
		public double? PowerRating { get; set; }
		[IsMoreFilter(nameof(MinVoltage))]
		public double? MinVoltage { get; set; }
		[IsLessFilter(nameof(MaxVoltage))]
		public double? MaxVoltage { get; set; }
		[IsMoreFilter(nameof(MinRatedResistance))]
		[ChartUsage]
		public double? MinRatedResistance { get; set; }
		[IsLessFilter(nameof(MaxRatedResistance))]
		public double? MaxRatedResistance { get; set; }
		[IsEqualFilter(nameof(ResistanceTolerance))]
		public double? ResistanceTolerance { get; set; }
		[IsMoreFilter(nameof(MinOperatingTemperature))]
		[ChartUsage]
		public double? MinOperatingTemperature { get; set; }
		[IsLessFilter(nameof(MaxOperatingTemperature))]
		[ChartUsage]
		public double? MaxOperatingTemperature { get; set; }
		[IsEqualFilter(nameof(CurrentLimit))]
		public double? CurrentLimit { get; set; }
		[StringLength(450)]
		[IsEqualFilter(nameof(QualicationSG))]
		public string? QualicationSG { get; set; }
		[StringLength(450)]
		[IsEqualFilter(nameof(QualicationЕС))]
		public string? QualicationЕС { get; set; }
		[StringLength(450)]
		[IsEqualFilter(nameof(Package))]
		[ChartUsage]
		public string? Package { get; set; }
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
		public Resistors() { }
		public Resistors(Resistors r)
		{
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
			Insertion = r.Insertion;
			SpecificationDoc = r.SpecificationDoc;
		}
	}
}
