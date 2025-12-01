using System.ComponentModel.DataAnnotations;

namespace app.Db.ef
{
    public class Technologies
    {
        [Key]
        public int ID { get; set; }
        public string RuTechnologyName { get; set; }
        public string EnTechnologyName { get; set; }
		public List<Microchips> Microchips { get; set; }
	}
}
