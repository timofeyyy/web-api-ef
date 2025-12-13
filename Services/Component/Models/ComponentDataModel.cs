using app.Db.Uow;
using app.Db.utils;
using System.Collections;

namespace app.Services.Component.Models
{
	public class ComponentDataModel
	{
		UnitOfWork1 _uow;

		public ComponentDataModel(UnitOfWork1 uow)
		{
			_uow = uow;
		}

		//Dictionary<string, Func<Task<List<Dictionary<string, object>>>>> _componentFullDictionary;
		Dictionary<string, Func<Task<List<KeyValueObject>>>> _componentDictionary;
		//public Dictionary<string, Func<Task<List<IComponentModel>>>> GetComponentDictionary()
		//{
		//	if (_componentDictionary == null)
		//	{
		//		Dictionary<string, Func<Task<List<IComponentModel>>>> tasks = new();
		//		tasks["capacitor"] = async () =>
		//		{
		//			object boxing = await _uow.CapacitorRepository.SelectAsObj();
		//			var unboxing = ((IEnumerable)boxing).Cast<IComponentModel>().ToList();
		//			return unboxing;
		//		};
		//		tasks["microchip"] = async () =>
		//		{
		//			object boxing = await _uow.MicrochipRepository.SelectAsObj();
		//			var unboxing = ((IEnumerable)boxing).Cast<IComponentModel>().ToList();
		//			return unboxing;
		//		};
		//		tasks["diod"] = async () =>
		//		{
		//			object boxing = await _uow.DiodRepository.SelectAsObj();
		//			var unboxing = ((IEnumerable)boxing).Cast<IComponentModel>().ToList();
		//			return unboxing;
		//		}; ;
		//		tasks["transistor"] = async () =>
		//		{
		//			object boxing = await _uow.TransistorRepository.SelectAsObj();
		//			var unboxing = ((IEnumerable)boxing).Cast<IComponentModel>().ToList();
		//			return unboxing;
		//		}; ;
		//		tasks["resistor"] = async () =>
		//		{
		//			object boxing = await _uow.ResistorRepository.SelectAsObj();
		//			var unboxing = ((IEnumerable)boxing).Cast<IComponentModel>().ToList();
		//			return unboxing;
		//		};
		//		_componentDictionary = tasks;
		//	}
		//	return _componentDictionary;
		//}

		//public Dictionary<string, Func<Task<List<KeyValueObject>>>> GetComponentDictionary()
		//{
		//	if (_componentDictionary == null)
		//	{
		//		Dictionary<string, Func<Task<List<KeyValueObject>>>> tasks = new();
		//		//tasks["capacitor"] = async () => await _uow.CapacitorRepository.SelectAsDict();
		//		//tasks["microchip"] = async () => await _uow.MicrochipRepository.SelectAsDict();
		//		//tasks["diod"] = async () => await _uow.DiodRepository.SelectAsDict();
		//		//tasks["transistor"] = async () => await _uow.TransistorRepository.SelectAsDict();
		//		//tasks["resistor"] = async () => await _uow.ResistorRepository.SelectAsDict();
		//		_componentDictionary = tasks;
		//	}
		//	return _componentDictionary;
		//}

		//public Dictionary<string, Func<Task<List<Dictionary<string, object>>>>> GetComponentFullDictionary()
		//{
		//	if (_componentFullDictionary == null)
		//	{
		//		Dictionary<string, Func<Task<List<Dictionary<string, object>>>>> tasks = new();
		//		tasks["capacitor"] = async () => await _uow.CapacitorRepository.SelectAsDict();
		//		tasks["microchip"] = async () => await _uow.MicrochipRepository.SelectAsDict();
		//		tasks["diod"] = async () => await _uow.DiodRepository.SelectAsDict();
		//		tasks["transistor"] = async () => await _uow.TransistorRepository.SelectAsDict();
		//		tasks["resistor"] = async () => await _uow.ResistorRepository.SelectAsDict();
		//		_componentFullDictionary = tasks;
		//	}
		//	return _componentFullDictionary;
		//}

		//public Dictionary<string, Func<int>> GetTotalsDictionary()
		//{
		//	var totalsDictionary = new Dictionary<string, Func<int>>();
		//	totalsDictionary.Add("microchip", () => _uow.MicrochipRepository.GetCount());
		//	totalsDictionary.Add("resistor", () => _uow.ResistorRepository.GetCount());
		//	totalsDictionary.Add("transistor", () => _uow.TransistorRepository.GetCount());
		//	totalsDictionary.Add("diod", () => _uow.DiodRepository.GetCount());
		//	totalsDictionary.Add("capacitor", () => _uow.CapacitorRepository.GetCount());
		//	return totalsDictionary;
		//}
	}
}
