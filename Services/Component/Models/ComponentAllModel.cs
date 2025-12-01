using app.Db.ef;
using app.Services.Component.Models;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.Json.Serialization;

namespace app.Services.Component.component
{
	public class ComponentAllModel : IComponentAllModel
	{
		public List<Microchips>? microchip { get; set; }
		public List<Capacitors>? capacitor { get; set; }
		public List<Resistors>? resistor { get; set; }
		public List<Transistors>? transistor { get; set; }
		public List<Diods>? diod { get; set; }
	}
}
