namespace app.Db.utils
{
	public interface IRepositoryBase<T> where T : class 
	{
		public Task<List<T>> SelectAsObj((Dictionary<string, object> pairs, List<int> ids) parameters = default);
		public Task<List<Dictionary<string, object>>> SelectAsDict((Dictionary<string, object> pairs, List<int> ids) parameters = default);
		public int GetCount();
	}
}
