using app.Models.Ef;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.Json.Serialization;

namespace app.Entities
{
	public class ComponentAll
	{
		public List<Microchips>? microchips { get; set; }
		public List<Capacitors>? capacitors { get; set; }
		public List<Resistors>? resistors { get; set; }
		public List<Transistors>? transistors { get; set; }
		public List<Diods>? diods { get; set; }
	}
}
