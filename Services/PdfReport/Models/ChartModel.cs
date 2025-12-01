using app.Services.Common.alias;

namespace app.Services.PdfReport.Models
{
	public class ChartModel
	{
		public List<AliasModel> Names { get; set; } = new List<AliasModel>() { 
			new() { RuVal = "Гистограмма", EnVal = "bar" },
			new() { RuVal = "Диаграмма Парето", EnVal = "mixed" },
			new() { RuVal = "Кольцевая", EnVal = "donut" },
			new() { RuVal = "Кругавая", EnVal = "circle" },
		};
	}
}
