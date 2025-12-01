namespace app.Services.Manufacturer.production
{
	public class ClassifiedManufacturersModel
	{
		public Dictionary<string, Dictionary<string, int>> CIS { get; set; }
		public Dictionary<string, Dictionary<string, int>> OTHER { get; set; }
	}
}
