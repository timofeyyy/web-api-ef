using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace app.Db.ef
{
	public class ComponentTypes
	{
		[JsonIgnore]
		[Key]
		public int ID { get; set; }
		[StringLength(450)]
		public string RuComponentType { get; set; }
		[StringLength(450)]
		public string EnComponentType { get; set; }
		[JsonIgnore]
		public List<Capacitors> Capacitors { get; set; }
		[JsonIgnore]
		public List<Resistors> Resistors { get; set; }
		[JsonIgnore]
		public List<Diods> Diods { get; set; }
		[JsonIgnore]
		public List<Microchips> Microchips { get; set; }
		[JsonIgnore]
		public List<Transistors> Transistors { get; set; }
	}

	public static class ComponentTypeExstension {
		public static bool ExistEnType(this List<ComponentTypes> componentTypes, string entype)
		{
			return componentTypes.Exists(el => el.EnComponentType.ToLower() == entype.ToLower());
		}

		public static ComponentList ToDictionary(this List<ComponentTypes> componentTypes)
		{
			ComponentList res = new();
			foreach (var componentType in componentTypes) {
				KeyValueObject keyValuePairs = new();
				keyValuePairs["RuComponentType"] = componentType.RuComponentType;
				keyValuePairs["EnComponentType"] = componentType.EnComponentType;
				res.Add(keyValuePairs);
			}
			return res;
		}
	}
}
