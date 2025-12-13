using app.Db.ef;
using app.Db.Uow;
using app.Db.utils;
using app.Services.Component;
using app.Services.Component.component;
using app.Services.Manufacturer;
using app.Services.Manufacturer.production;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;


namespace WebAPIApp.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ManufacturersController : ControllerBase
	{
		readonly IManufacturerService _manufacturerService;
		readonly IComponentService _componentService;
		readonly IMemoryCache _cache;


		public ManufacturersController(IManufacturerService manufacturerService, IComponentService componentService, IMemoryCache cache)
		{
			_manufacturerService = manufacturerService;
			_componentService = componentService;
			_cache = cache;
		}

		[HttpGet("production")]
		public async Task<ActionResult<Dictionary<string, Dictionary<string, int>>>> Get(
				[FromQuery] string? RuComponentType,
				[FromQuery] string? RuComponentKind,
				[FromQuery] string? ManufacturerName
				)
		{
			KeyValueObject dict = new();
			dict["RuComponentType"] = RuComponentType;
			dict["RuComponentKind"] = RuComponentKind;
			dict["ManufacturerName"] = ManufacturerName;

			_cache.TryGetValue("components/all", out DataSystemView all);
			if (all == null)
			{
				all = await _componentService.SelectAll();
				_cache.Set("components/all", all, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));

			}
			all = all.FilterByParamValue(dict);
			return _manufacturerService.GetProdAsDict(all);
		}

		[HttpGet("production/classification")]
		public async Task<ActionResult<ClassifiedManufacturersModel>> GetClassifiedManufacturer()
		{
			_cache.TryGetValue("components/all", out DataSystemView all);
			if (all == null)
			{
				all = await _componentService.SelectAll();
				_cache.Set("components/all", all, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));

			}
			_cache.TryGetValue("manufacturers/all", out List<Manufacturers> manufacturers);
			if (manufacturers == null)
			{
				manufacturers = await _manufacturerService.SelectAll();
				_cache.Set("manufacturers/all", all, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));
			}
			return _manufacturerService.GetForeignList(all, manufacturers);
		}

		[HttpGet("all")]
		public async Task<ActionResult<List<Manufacturers>>> GetAll()
		{
			_cache.TryGetValue("manufacturers/all", out List<Manufacturers> manufacturers);
			if (manufacturers == null)
			{
				manufacturers = await _manufacturerService.SelectAll();
				_cache.Set("manufacturers/all", manufacturers, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));
			}
			return manufacturers;
		}
	}
}
