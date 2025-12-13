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
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Caching.Memory;
using Mono.TextTemplating;
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
		readonly IMemoryCache _cache;

		public ComponentsController(IComponentService componentService, IRefHelper refHelperService, IComponentTypeService componentTypeService, IManufacturerService manufacturerService, IMemoryCache cache)
		{
			_componentService = componentService;
			_refHelperService = refHelperService;
			_componentTypeService = componentTypeService;
			_manufacturerService = manufacturerService;
			_cache = cache;
		}
		[HttpGet("statistic")]
		public async Task<ActionResult<Dictionary<string, List<ManufacturerProductionModel>>>> GetManufacturerNameStatistic(
			[FromQuery] string? RuComponentType,
			[FromQuery] string? EnComponentType
			)
		{
			KeyValueObject parameters = new();
			parameters["RuComponentType"] = RuComponentType;
			parameters["EnComponentType"] = EnComponentType;
			_cache.TryGetValue("components/all", out DataSystemView all);
			if (all == null)
			{
				all = await _componentService.SelectAll();
				_cache.Set("components/all", all, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));
			}
			all = all.FilterByParamValue(parameters);
			return _manufacturerService.GetStatistic(all);
		}

		[HttpGet("names")]
		public async Task<ActionResult<ComponentList>> GetNames()
		{
			_cache.TryGetValue("components/names", out List<ComponentTypes> names);
			if (names == null)
			{
				names = await _componentTypeService.SelectAll();
				_cache.Set("components/names", names, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));
			}
			return names.ToDictionary();
		}


		[HttpGet("all")]
		public async Task<ActionResult<DataClientView>> GetComponents(
			[FromQuery] string? RuComponentType,
			[FromQuery] string? RuComponentKind,
			[FromQuery] string? EnComponentType,
			[FromQuery] string? EnComponentKind,
			[FromQuery] string? ManufacturerName
			)
		{
			KeyValueObject dict = new();
			dict["RuComponentType"] = RuComponentType;
			dict["RuComponentKind"] = RuComponentKind;
			dict["EnComponentType"] = EnComponentType;
			dict["EnComponentKind"] = EnComponentKind;
			dict["ManufacturerName"] = ManufacturerName;
			_cache.TryGetValue("components/all", out DataSystemView all);
			if (all == null)
			{
				all = await _componentService.SelectAll();
				_cache.Set("components/all", all, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));
			}
			return all.FilterByParamValue(dict).RemoveMetadata();
		}

		[HttpGet("dates")]
		public async Task<ActionResult<SelectionView>> GetLatestComponents()
		{
			_cache.TryGetValue("components/all", out DataSystemView all);
			if (all == null)
			{
				all = await _componentService.SelectAll();
				_cache.Set("components/all", all, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));
			}
			return all.SelectAsDates();
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
		public async Task<ActionResult<KeyValueObject>> GetComponentById(
				string entype,
				int id
			)
		{
			entype = entype.ToLower();
			_cache.TryGetValue("components/names", out List<ComponentTypes> names);
			if (names == null)
			{
				names = await _componentTypeService.SelectAll();
				_cache.Set("components/names", names, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));
			}
			var isEntypeExists = names.ExistEnType(entype);
			if (!isEntypeExists)
			{
				return BadRequest();
			}
			KeyValueObject parameters = new();
			parameters["ID"] = id;

			_cache.TryGetValue("components/all", out DataSystemView all);
			if (all == null)
			{
				all = await _componentService.SelectAll();
				_cache.Set("components/all", all, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));
			}
			var components = all.FilterByParamValue(parameters).GetComponentsByEnType(entype);
			if (components.Components.Count == 0)
			{
				return BadRequest();
			}
			return components.Components[0];
		}
		[HttpGet("{entype}/{parameter}/statistic")]
		public async Task<ActionResult<Dictionary<string, List<ParamProductionModel>>>> GetParamStatistic(
				string entype,
				string parameter
			)
		{
			entype = entype.ToLower();
			_cache.TryGetValue("components/names", out List<ComponentTypes> names);
			if (names == null)
			{
				names = await _componentTypeService.SelectAll();
				_cache.Set("components/names", names, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));
			}
			var isEntypeExists = names.ExistEnType(entype);
			if (!isEntypeExists)
			{
				return BadRequest();
			}

			var IsParameterExists = await _refHelperService.IsParameterExists(entype, parameter);
			if (!IsParameterExists)
			{
				return BadRequest();
			}


			_cache.TryGetValue("components/all", out DataSystemView all);
			if (all == null)
			{
				all = await _componentService.SelectAll();
				_cache.Set("components/all", all, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));
			}


			ComponentSection components = all.GetComponentsByEnType(entype);
			if (components == null || components.Components.Count() == 0)
			{
				return BadRequest();
			}
			var statistic = _componentService.GetParamStat(components.Components, parameter);
			return statistic;
		}

		[HttpGet("{entype}/priorities")]
		public async Task<ActionResult<DataClientView>> GetComponentsByProperties(
				string entype,
				[FromQuery] Dictionary<string, string> parameters
			)
		{
			entype = entype.ToLower();
			_cache.TryGetValue("components/names", out List<ComponentTypes> names);
			if (names == null)
			{
				names = await _componentTypeService.SelectAll();
				_cache.Set("components/names", names, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));
			}

			var isEntypeExists = names.ExistEnType(entype);
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
			_cache.TryGetValue("components/all", out DataSystemView all);
			if (all == null)
			{
				all = await _componentService.SelectAll();
				_cache.Set("components/all", all, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));
			}

			return all.GetComponentsByEnType(entype).GetStepSelection(parameters);
		}
	}
}