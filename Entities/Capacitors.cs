using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace app.Entities
{
    public class Capacitors
    {
        [Key]
        public int ID { get; set; }
        public long? DocID { get; set; }
 
        [StringLength(450)]
        public string ComponentName { get; set; }
        public ComponentTypes? Type_ { get; set; }
        public ComponentKinds? Kind_ { get; set; }
        public Manufacturers? ManufacturerName_ { get; set; }
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
    }
}
