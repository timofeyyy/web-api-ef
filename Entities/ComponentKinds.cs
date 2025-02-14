using System.ComponentModel.DataAnnotations;

namespace app.Entities
{
    public class ComponentKinds
    {
        [Key]
        public int ID { get; set; }
        [StringLength(450)]
        public string RuComponentKind { get; set; }
        [StringLength(450)]
        public string EnComponentKind { get; set; }
        public List<Capacitors> Capacitors { get; set; }
    }
}
