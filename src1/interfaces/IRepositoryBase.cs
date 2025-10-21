namespace app.src1.interfaces
{
	public interface IRepositoryBase<T> where T : class
	{
		public Task<List<T>> Select(Dictionary<string, string> dict);
		public int GetCount();
	}
}
