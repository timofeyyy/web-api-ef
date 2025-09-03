using app.Models.Ef;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text.Json.Serialization;

namespace app.Entities
{
    public class Capacitors
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
		[StringLength(450)]
        public string? OutputType { get; set; }
        public double? MinVoltage { get; set; }
        public double? MaxVoltage { get; set; }
        public double? MaxCapacity { get; set; }
        public double? MinCapacity { get; set; }
        public double? MinOperatingTemperature { get; set; }
        public double? MaxOperatingTemperature { get; set; }
        public double? AcceptableCapacityIncrease { get; set; }
        public double? AcceptableСapacityReduction { get; set; }
        public string? QualicationSG { get; set; }
        public string? QualicationЕС { get; set; }
        public string? Remark1 { get; set; }
        public string? Remark2 { get; set; }
		public DateTime? Date { get; set; }
		[NotMapped]
		public string? RuComponentType { get; set; }
		[NotMapped]
		public string? EnComponentType { get; set; }

		public Capacitors() { }
		public Capacitors(Capacitors c) {
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
			Date = c.Date;
		}
	}
}
