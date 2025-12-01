using app.Db.ef;
using app.Db.Uow;
using app.Db.utils;
using app.Services.Component;
using app.Services.Manufacturer;
using app.Services.Manufacturer.production;
using Microsoft.AspNetCore.Mvc;


namespace WebAPIApp.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class ManufacturersController : ControllerBase
    {
		readonly IManufacturerService _manufacturerService;
		readonly IComponentService _componentService;

		public ManufacturersController(IManufacturerService manufacturerService, IComponentService componentService)
        {
			_manufacturerService = manufacturerService;
			_componentService = componentService;
		}

		[HttpGet("production")]
		public async Task<ActionResult<Dictionary<string, Dictionary<string, int>>>> Get(
			[FromQuery] string? ruComponentType,
			[FromQuery] string? ruComponentKind,
			[FromQuery] string? manufacturerName
			)
		{
			Dictionary<string, object> parameters = new();
			parameters["RuComponentType"] = ruComponentType;
			parameters["RuComponentKind"] = ruComponentKind;
			parameters["ManufacturerName"] = manufacturerName;
			
			List<IComponentModel> components = await _componentService.GetComponentsAsObjIEnum((pairs: parameters, ids: null));
			return _manufacturerService.GetProdAsDict(components);
		}

		[HttpGet("production/classification")]
		public async Task<ActionResult<ClassifiedManufacturersModel>> GetClassifiedManufacturer()
		{
			List<IComponentModel> components = await _componentService.GetComponentsAsObjIEnum();
			var classification = await _manufacturerService.GetForeignList(components);
			return classification;
		}

		[HttpGet("all")]
		public async Task<ActionResult<List<Manufacturers>>> GetAll()
		{
			return await _manufacturerService.GetAsObj();
		}
	}
}