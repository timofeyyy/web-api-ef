using app.Entities;
using app.Models.Ef;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.src1.interfaces
{
	public interface IComponentModel
	{
		public int ID { get; set; }
		public string ComponentName { get; set; }
		public ComponentTypes Type { get; set; }
		public ComponentKinds Kind { get; set; }
		public Manufacturers Manufacturer { get; set; }
	}
}
