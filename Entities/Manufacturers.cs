using System.ComponentModel.DataAnnotations;

namespace app.Entities
{
    public class Manufacturers
    {
        [Key]
        public int ID { get; set; }

        [StringLength(450)]
        public string ManufacturerName { get; set; }
        public List<Capacitors> Capacitors { get; set; }
        public List<Resistors> Resistors { get; set; }
        public List<Diods> Diods { get; set; }
        public List<Microchips> Microchips { get; set; }
        public List<Transistors> Transistors { get; set; }
        
    }
}
