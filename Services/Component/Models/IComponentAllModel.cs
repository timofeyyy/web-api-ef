using app.Db.ef;

namespace app.Services.Component.Models
{
	public interface IComponentAllModel
	{
		public List<Microchips>? microchip { get; set; }
		public List<Capacitors>? capacitor { get; set; }
		public List<Resistors>? resistor { get; set; }
		public List<Transistors>? transistor { get; set; }
		public List<Diods>? diod { get; set; }
	}
}
