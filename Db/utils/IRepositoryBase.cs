namespace app.Db.utils
{
	public interface IRepositoryBase<T> where T : class 
	{
		public Task<List<T>> SelectAll();
		public int GetCount();
	}
}
