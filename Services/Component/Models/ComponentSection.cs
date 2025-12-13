namespace app.Services.Component.Models
{
	public class ComponentSection
	{
		//public delegate bool Compare(object instance, object comparedVal);
		public ComponentList Components { get; set; } = new();
		public ComponentMetadata Metadata { get; set; } = new();
	}

	public class ComponentMetadata
	{
		public delegate bool Compare(object instance, object comparedVal);
		public List<string> JsonIgnoreAttr { get; set; } = new();
		public List<string> ChartUsageAttr { get; set; } = new();
		public List<string> NotMapperAttr { get; set; } = new();
		public Dictionary<string, Compare> CompareAttr { get; set; } = new();
	}
}
