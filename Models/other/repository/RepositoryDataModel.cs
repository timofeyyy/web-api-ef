using app.db;
using app.Db.Rep;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MigraDoc.DocumentObjectModel.Tables;
using NuGet.Packaging.Signing;
using System.Text.Json;

namespace app.Models.other.repository
{
	public class RepositoryDataModel
	{
		//Dictionary<string, Func<Task<List<IComponent>>>> _selectPreviewTasks = new();
		//Dictionary<string, Func<List<string>>> _mapedColumns = new();
		//Dictionary<string, Func<List<string>>> _chartColumns = new();
		//public Dictionary<string, Func<Task<List<IComponent>>>> SelectPreviewTasks { get => _selectPreviewTasks; }
		//public Dictionary<string, Func<List<string>>> MapedColumns { get => _mapedColumns; }
		//public Dictionary<string, Func<List<string>>> ChartColumns { get => _chartColumns; }

		//Dictionary<string, ComponentBase<IComponent>> _mapedColumns = new();


		UnitOfWork _uow;
		public RepositoryDataModel(UnitOfWork uow)
		{
			_uow = uow;
		}

		public async void InitModel()
		{
			//_mapedColumns["microchip"] = _uow.Mr;

			//_mapedColumns["microchip"] = () => _uow.Mr.GetAllMapedColumns();
			//_mapedColumns["capacitor"] = () => _uow.Cr.GetAllMapedColumns();
			//_mapedColumns["diod"] = () => _uow.Dr.GetAllMapedColumns();
			//_mapedColumns["transistor"] = () => _uow.Tr.GetAllMapedColumns();
			//_mapedColumns["resistor"] = () => _uow.Rr.GetAllMapedColumns();

			//_chartColumns["microchip"] = () => _uow.Mr.GetChartColumns();
			//_chartColumns["capacitor"] = () => _uow.Cr.GetChartColumns();
			//_chartColumns["diod"] = () => _uow.Dr.GetChartColumns();
			//_chartColumns["transistor"] = () => _uow.Tr.GetChartColumns();
			//_chartColumns["resistor"] = () => _uow.Rr.GetChartColumns();

			//_selectPreviewTasks["microchip"] = async () => await _uow.Mr.SelectPreviewByListIds(ids);
			//_selectPreviewTasks["capacitor"] = async () => await _uow.Cr.SelectPreviewByListIds(ids);
			//_selectPreviewTasks["diod"] = async () => await _uow.Dr.SelectPreviewByListIds(ids);
			//_selectPreviewTasks["transistor"] = async () => await _tr.SelectPreviewByListIds(ids);
			//_selectPreviewTasks["resistor"] = async () => await _rr.SelectPreviewByListIds(ids);
		}

		public async Task<Dictionary<string, string>> GetAlias() {
			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "alias.json");
			if (!File.Exists(path))
				return null;

			string json = await File.ReadAllTextAsync(path);
			var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
			return dict;
		}
	}
}
