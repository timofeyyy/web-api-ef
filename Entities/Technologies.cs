using System.ComponentModel.DataAnnotations;

namespace app.Entities
{
    public class Technologies
    {
        [Key]
        public int ID { get; set; }
        public string RuTechnologyName { get; set; }
        public string EnTechnologyName { get; set; }
    }
}
