using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace app.Entities
{
    public class Microchips
    {
        [Key]
        public int ID { get; set; }
        public long? DocID { get; set; }
        [StringLength(450)]
        public string ComponentName { get; set; }
        public ComponentTypes Type_ { get; set; }
        public ComponentKinds Kind_ { get; set; }
        public Manufacturers ManufacturerName_ { get; set; }
        public string? Interfaces { get; set; }
        public double MinVoltage { get; set; }
        public double MaxVoltage { get; set; }
        public double? Frequency { get; set; }
        public string? BitDepthValue { get; set; }
        public double? ConsumptionCurrent { get; set; }
        public Technologies TechnologyName_ { get; set; }
        public double MinOperatingTemperature { get; set; }
        public double MaxOperatingTemperature { get; set; }
        public double? RadiationResistance { get; set; }
        public string? RadiationResistanceI { get; set; }
        public string? MemoryFormat { get; set; }
        public double? SamplingTime { get; set; }
        public string? Qualication { get; set; }
        public string? Remark1 { get; set; }
    }
}
