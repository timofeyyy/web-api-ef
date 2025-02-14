using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace app.Entities
{
    public class Resistors
    {
        [Key]
        public int ID { get; set; }
        public long? DocID { get; set; }
 
        [StringLength(450)]
        public string ComponentName { get; set; }
        public ComponentTypes? Type_ { get; set; }
        public ComponentKinds? Kind_ { get; set; }
        public Manufacturers? ManufacturerName_ { get; set; }
        public double? PowerRating { get; set; }
        public double? MinVoltage { get; set; }
        public double? MaxVoltage { get; set; }
        public double? MinRatedResistance { get; set; }
        public double? MaxRatedResistance { get; set; }
        public double? ResistanceTolerance { get; set; }
        public double? MinOperatingTemperature { get; set; }
        public double? MaxOperatingTemperature { get; set; }
        public double? CurrentLimit { get; set; }
        public string? QualicationSG { get; set; }
        public string? QualicationЕС { get; set; }
        public string? Package { get; set; }
        public string? Remark1 { get; set; }
        public string? Remark2 { get; set; }
    }
}
