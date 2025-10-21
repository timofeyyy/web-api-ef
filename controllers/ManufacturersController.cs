using app.Context;
using app.db;
using app.Entities;
using app.Models.Ef;
using app.Models.other.production;
using app.src1.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace WebAPIApp.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class ManufacturersController : ControllerBase
    {
		readonly UnitOfWork _uow;

		public ManufacturersController(UnitOfWork uow)
        {
            _uow = uow;
        }

		[HttpGet("production")]
		public async Task<ActionResult<Dictionary<string, Dictionary<string, int>>>> Get(
			[FromQuery] string? ruComponentType,
			[FromQuery] string? ruComponentKind,
			[FromQuery] string? manufacturerName
			)
		{
			Dictionary<string, string> parameters = new Dictionary<string, string>();
			parameters["RuComponentType"] = ruComponentType;
			parameters["RuComponentKind"] = ruComponentKind;
			parameters["ManufacturerName"] = manufacturerName;
			List<IComponentModel> components = await _uow.GetComponentPreviewByParamValue(parameters);
			return _uow.GetManufacturersProductionAsDictionarty(components);
		}

		[HttpGet("production/classification")]
		public async Task<ActionResult<ClassifiedManufacturersModel>> GetClassifiedManufacturer()
		{
			List<IComponentModel> components = await _uow.GetComponentPreviewByParamValue();
			List<Manufacturers> manufacturers = await _uow.GetManufacturers();
			var classification = _uow.GetForeignManufacturerList(components, manufacturers);
			return classification;
		}

		[HttpGet("all")]
		public async Task<ActionResult<List<Manufacturers>>> GatAll()
		{
			List<Manufacturers> manufacturers = await _uow.GetManufacturers();
			return manufacturers;
		}
	}
}