using app.db;
using app.Db.Rep;
using app.Models.Ef;
using app.Models.other.alias;
using app.Models.other.component;
using app.Models.other.production;
using app.src1.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;



namespace WebAPIApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComponentsController : ControllerBase
    {
		readonly UnitOfWork _uow;

		public ComponentsController(UnitOfWork uow)
		{
			_uow = uow;
		}

		[HttpGet("statistic")]
		public async Task<ActionResult<Dictionary<string, List<ManufacturerProductionModel>>>> GetManufacturerNameStatistic([FromQuery] string? ruComponentType)
		{
			
			Dictionary<string, string> parameters = new Dictionary<string, string>();
			parameters["RuComponentType"] = ruComponentType;

			//Dictionary<string, int> totals = new Dictionary<string, int>();
			//totals.Add("microchip", _uow.GetCount();
			//totals.Add("resistor", _uow.GetResistorCount());
			//totals.Add("transistor", _uow.GetTransistorCount());
			//totals.Add("diod", _uow.GetDiodCount());
			//totals.Add("capacitor", _uow.GetCapacitorCount());


			List<IComponentModel> components = await _uow.GetComponentPreviewByParamValue(parameters);

			var dict = _uow.GetManufacturerStatistic(components);
			return dict;
		}

		[HttpGet("names")]
		public async Task<ActionResult<IEnumerable<ComponentTypes>>> GetNames()
		{
			return await _uow.GetComponentTypes();
		}

		[HttpGet("all")]
		public async Task<ActionResult<ComponentAllModel>> GetComponents(
		[FromQuery] string? ruComponentType,
			[FromQuery] string? ruComponentKind,
			[FromQuery] string? manufacturerName
			)
		{
			Dictionary<string, string> dict = new Dictionary<string, string>();
			dict["RuComponentType"] = ruComponentType;
			dict["RuComponentKind"] = ruComponentKind;
			dict["ManufacturerName"] = manufacturerName;
			return await _uow.SelectAll(dict);
		}
		[HttpGet("{entype}/columns/chart")]
		public async Task<ActionResult<IEnumerable<AliasModel>>> GetChartColumns(
			string entype
			)
		{
			var items = await _uow.GetChartColumns(entype);
			if(items == null)
			{
				return BadRequest();
			}
			return items;
		}
		[HttpGet("{entype}/columns/all")]
		public async Task<ActionResult<IEnumerable<AliasModel>>> GetAllMapedColumns(
			string entype
			)
		{
			var items = await _uow.GetAllMapedColumfGetParamStatisticns(entype);
			if (items == null)
			{
				return BadRequest();
			}
			return items;
		}

		[HttpGet("alias")]
		public async Task<ActionResult<string>> GetAlias()
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "alias.json");
			if (!System.IO.File.Exists(path))
				return NotFound();

			string json = await System.IO.File.ReadAllTextAsync(path);
			var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
			return Content(json, "application/json");
		}
		[HttpGet("{entype}/{parameter}/statistic")]
		public async Task<ActionResult<Dictionary<string, List<ParamProductionModel>>>> GetParamStatistic(
				string entype,
				string parameter
			)
		{
			var cts = await _uow.GetComponentTypes();
			var isEntypeExists = _uow.IsEnComponentTypeExists(cts, entype);
			if(!isEntypeExists)
			{
				return BadRequest();
			}
			var alias = await _uow.GetAllMapedColumfGetParamStatisticns(entype);
			var IsParameterExists = _uow.IsParameterExists(alias, parameter);
			if(!IsParameterExists)
			{
				return BadRequest();
			}

			List<IComponentModel> components = await _uow.GetAllComponentsByEnType(entype);
			if (components == null || components.Count() == 0)
			{
				return BadRequest();
			}
			var statistic =  _uow.GetParamStatistic(components, parameter, entype);
			return statistic;
		}
	}
}