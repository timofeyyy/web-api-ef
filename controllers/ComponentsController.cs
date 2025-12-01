using app.Db.ef;
using app.Db.utils;
using app.Services.Common.alias;
using app.Services.Common.Ref;
using app.Services.Component;
using app.Services.Component.component;
using app.Services.Component.Models;
using app.Services.ComponentType;
using app.Services.Manufacturer;
using app.Services.Manufacturer.production;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;



namespace WebAPIApp.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ComponentsController : ControllerBase
	{
		readonly IComponentService _componentService;
		readonly IRefHelper _refHelperService;
		readonly IComponentTypeService _componentTypeService;
		readonly IManufacturerService _manufacturerService;
		public ComponentsController(IComponentService componentService, IRefHelper refHelperService, IComponentTypeService componentTypeService, IManufacturerService manufacturerService)
		{
			_componentService = componentService;
			_refHelperService = refHelperService;
			_componentTypeService = componentTypeService;
			_manufacturerService = manufacturerService;
		}
		[HttpGet("statistic")]
		public async Task<ActionResult<Dictionary<string, List<ManufacturerProductionModel>>>> GetManufacturerNameStatistic([FromQuery] string? ruComponentType)
		{

			Dictionary<string, object> parameters = new();
			parameters["RuComponentType"] = ruComponentType;
			List<IComponentModel> components = await _componentService.GetComponentsAsObjIEnum((pairs: parameters, ids: null));
			var dict = _manufacturerService.GetStatistic(components, _componentService.ComponentDataModel.GetTotalsDictionary());
			return dict;
		}

		[HttpGet("names")]
		public async Task<ActionResult<IEnumerable<ComponentTypes>>> GetNames()
		{
			return await _componentTypeService.GetNamesAsObj();
		}

		[HttpGet("all")]
		public async Task<ActionResult<ComponentAllModel>> GetComponents(
			[FromQuery] string? ruComponentType,
			[FromQuery] string? ruComponentKind,
			[FromQuery] string? enComponentType,
			[FromQuery] string? enComponentKind,
			[FromQuery] string? manufacturerName
			)
		{
			Dictionary<string, object> dict = new();
			dict["RuComponentType"] = ruComponentType;
			dict["RuComponentKind"] = ruComponentKind;
			dict["EnComponentType"] = enComponentType;
			dict["EnComponentKind"] = enComponentKind;
			dict["ManufacturerName"] = manufacturerName;
			return await _componentService.GetComponentsAsObj((pairs: dict, ids: null));
		}
		[HttpGet("{entype}/columns/chart")]
		public async Task<ActionResult<IEnumerable<AliasModel>>> GetChartColumns(
			string entype
			)
		{
			entype = entype.ToLower();
			var items = await _refHelperService.GetChartColumns(entype);
			if (items == null)
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
			entype = entype.ToLower();
			var items = await _refHelperService.GetAllMapedColumns(entype);
			if (items == null)
			{
				return BadRequest();
			}
			return items;
		}
		[HttpGet("columns/all")]
		public async Task<ActionResult<Dictionary<string, List<AliasModel>>>> GetAllMapedColumns()
		{
			var items = await _refHelperService.GetAllMapedColumns();
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
		[HttpGet("{entype}/{id}")]
		public async Task<ActionResult<Dictionary<string, object>>> GetParamStatistic(
				string entype,
				int id
			)
		{
			entype = entype.ToLower();
			var isEntypeExists = await _componentTypeService.IsEnComponentTypeExists(entype);
			if (!isEntypeExists)
			{
				return BadRequest();
			}
			Dictionary<string, string> parameters = new();
			parameters["Id"] = id.ToString();
			List<IComponentModel> components = await _componentService.GetComponentsByEnType(entype);
			var prComponents = await _componentService.GetPriorityComponents(components, entype, parameters);
			var dict = prComponents[entype.ToLower()];
			if(dict.Count == 0)
			{
				return BadRequest();
			}
			var items = dict["Id"];
			if (items.Count == 0)
			{
				return BadRequest();
			}
			return items[0];
		}
		[HttpGet("{entype}/{parameter}/statistic")]
		public async Task<ActionResult<Dictionary<string, List<ParamProductionModel>>>> GetParamStatistic(
				string entype,
				string parameter
			)
		{
			entype = entype.ToLower();
			var isEntypeExists = await _componentTypeService.IsEnComponentTypeExists(entype);
			if (!isEntypeExists)
			{
				return BadRequest();
			}
			var IsParameterExists = await _refHelperService.IsParameterExists(entype, parameter);
			if (!IsParameterExists)
			{
				return BadRequest();
			}

			List<IComponentModel> components = await _componentService.GetComponentsByEnType(entype);
			if (components == null || components.Count() == 0)
			{
				return BadRequest();
			}
			var statistic = _componentService.GetParamStat(components, parameter, entype);
			return statistic;
		}

		[HttpGet("{entype}/priorities")]
		public async Task<ActionResult<Dictionary<string, Dictionary<string, List<Dictionary<string, object>>>>>> GetComponentsByProperties(
				string entype,
				[FromQuery] Dictionary<string, string> parameters
			)
		{
			entype = entype.ToLower();
			var isEntypeExists = await _componentTypeService.IsEnComponentTypeExists(entype);
			if (!isEntypeExists)
			{
				return BadRequest();
			}
			foreach (var key in parameters.Keys)
			{
				var IsParameterExists = await _refHelperService.IsParameterExists(entype, key);
				if (!IsParameterExists)
				{
					return BadRequest();
				}
			}
			List<IComponentModel> components = await _componentService.GetComponentsByEnType(entype);

			var prComponents = await _componentService.GetPriorityComponents(components, entype, parameters);

			return prComponents;
		}
	}
}