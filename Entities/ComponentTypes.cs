using System.ComponentModel.DataAnnotations;

namespace app.Entities
{
    public class ComponentTypes
    {
        [Key]
        public int ID { get; set; }
        [StringLength(450)]
        public string RuComponentType { get; set; }
        [StringLength(450)]
        public string EnComponentType { get; set; }
        public List<Capacitors> Capacitors { get; set; }
    }
}
