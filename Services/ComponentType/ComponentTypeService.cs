using app.Db.ef;
using app.Db.Uow;
using System.Runtime.Intrinsics.Arm;

namespace app.Services.ComponentType
{
	public class ComponentTypeService : IComponentTypeService
	{
		readonly UnitOfWork1 _uow;
		public ComponentTypeService(UnitOfWork1 uow)
		{
			_uow = uow;
		}
		public Task<List<ComponentTypes>> GetNamesAsObj()
		{
			return _uow.ComponentTypeRepository.SelectAsObj();
		}
		public async Task<bool> IsEnComponentTypeExists(string entype)
		{
			var ctList = await GetNamesAsObj();
			return ctList.Exists(el => el.EnComponentType.ToLower() == entype.ToLower());
		}
		public async Task<ComponentTypes> GetByEn(string entype)
		{
			var ctList = await GetNamesAsObj();
			return ctList.Find(el => el.EnComponentType.ToLower() == entype.ToLower());
		}
		public async Task<ComponentTypes> GetByRu(string rutype)
		{
			var ctList = await GetNamesAsObj();
			return ctList.Find(el => el.RuComponentType.ToLower() == rutype.ToLower());
		}
	}
}
