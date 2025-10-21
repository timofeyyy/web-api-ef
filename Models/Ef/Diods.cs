using app.Entities;
using app.src1.attrs;
using app.src1.interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text.Json.Serialization;

namespace app.Models.Ef
{
    public class Diods : IComponentModel
	{
		[NotMapped]
		public string? RuComponentKind { get; set; }
		[NotMapped]
		[ChartUsage]
		public string? ManufacturerName { get; set; }
		[NotMapped]
		public string? EnComponentKind { get; set; }

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
		[ChartUsage]
		public double? MaxPermissibleDCVoltage { get; set; }
		[ChartUsage]
		public double? MinOperatingTemperature { get; set; }
		[ChartUsage]
		public double? MaxOperatingTemperature { get; set; }
		[ChartUsage]
		public double? MaxPermissibleAverageDirectCurrent { get; set; }
		[ChartUsage]
		public double? MaxiPermissibleDirectCurrent { get; set; }
		[ChartUsage]
		public double? RadiationResistance { get; set; }
        public string? RadiationResistanceI { get; set; }
        public string? QualicationSG { get; set; }
        public string? QualicationЕС { get; set; }
        public string? Package { get; set; }
        public string? Remark1 { get; set; } 
        public string? Remark2 { get; set; }
		public DateTime? Date { get; set; }
		//[JsonIgnore]
		[NotMapped]
		public string? RuComponentType { get; set; }
		//[JsonIgnore]
		[NotMapped]
		public string? EnComponentType { get; set; }

		public Diods() { }
        public Diods(Diods d) {
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
			Date = d.Date;
		}
    }
}
