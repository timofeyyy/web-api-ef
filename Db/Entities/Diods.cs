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



		[JsonIgnore]
		public ComponentTypes Type { get; set; }
		[JsonIgnore]
		public ComponentKinds Kind { get; set; }
		[JsonIgnore]
		public Manufacturers Manufacturer { get; set; }



		[IsEqualFilter(nameof(ComponentName))]
		[StringLength(450)]
		public string ComponentName { get; set; }
		[IsLessFilter(nameof(MaxPermissibleDCVoltage))]
		[ChartUsage]
		public double? MaxPermissibleDCVoltage { get; set; }
		[IsMoreFilter(nameof(MinOperatingTemperature))]
		[ChartUsage]
		public double? MinOperatingTemperature { get; set; }
		[IsLessFilter(nameof(MaxOperatingTemperature))]
		[ChartUsage]
		public double? MaxOperatingTemperature { get; set; }
		[IsLessFilter(nameof(MaxPermissibleAverageDirectCurrent))]
		[ChartUsage]
		public double? MaxPermissibleAverageDirectCurrent { get; set; }
		[IsLessFilter(nameof(MaxiPermissibleDirectCurrent))]
		[ChartUsage]
		public double? MaxiPermissibleDirectCurrent { get; set; }
		[IsEqualFilter(nameof(RadiationResistance))]
		[ChartUsage]
		public double? RadiationResistance { get; set; }
		[StringLength(450)]
		[IsEqualFilter(nameof(RadiationResistanceI))]
		public string? RadiationResistanceI { get; set; }
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
		public Diods() { }
		public Diods(Diods d)
		{
			ID = d.ID;
			DocID = d.DocID;


			MaxPermissibleDCVoltage = d.MaxPermissibleDCVoltage;
			ComponentName = d.ComponentName;
			MinOperatingTemperature = d.MinOperatingTemperature;
			MaxOperatingTemperature = d.MaxOperatingTemperature;
			MaxPermissibleAverageDirectCurrent = d.MaxPermissibleAverageDirectCurrent;
			MaxiPermissibleDirectCurrent = d.MaxiPermissibleDirectCurrent;
			RadiationResistance = d.RadiationResistance;
			RadiationResistanceI = d.RadiationResistanceI;
			QualicationSG = d.QualicationSG;
			QualicationЕС = d.QualicationЕС;
			Package = d.Package;
			Remark1 = d.Remark1;
			Remark2 = d.Remark2;
			Insertion = d.Insertion;
			SpecificationDoc = d.SpecificationDoc;
		}
	}
}
