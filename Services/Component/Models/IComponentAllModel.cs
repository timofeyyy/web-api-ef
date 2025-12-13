using app.Db.ef;
using app.Db.utils;

namespace app.Services.Component.Models
{
	public interface IComponentAllModel
	{
		public List<Microchips>? Microchip { get; set; }
		public List<Capacitors>? Capacitor { get; set; }
		public List<Resistors>? Resistor { get; set; }
		public List<Transistors>? Transistor { get; set; }
		public List<Diods>? Diod { get; set; }
	}
}
