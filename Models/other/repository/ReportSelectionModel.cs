using app.Models.other.priority;

namespace app.Models.other.repository
{
	public class ReportSelectionModel
	{
		public PriorityWrapperModel Parameters { get; set; }
		public List<int> Components { get; set; }
	}
}
