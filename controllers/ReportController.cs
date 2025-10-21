using app.Context;
using app.db;
using app.Entities;
using app.Logger;
using app.Models.Ef;
using app.Models.other.repository;
using app.PdfReport;
using app.src1.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using NuGet.Protocol.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPIApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
		readonly UnitOfWork _uow;

		public ReportController(UnitOfWork uow)
		{
			_uow = uow;
		}


		[HttpPost("{entype}/pdf")]
		public async Task<ActionResult<ReportSelectionModel>> MakeReport(
			string entype,
			[FromBody] ReportSelectionModel report
		)
		{
			if(report.Components == null)
			{
				return BadRequest();
			}
			List<IComponentModel> components = await _uow.ComponentListByEnType(entype, report.Components);
			if (components == null || components.Count() == 0)
			{
				return BadRequest();
			}
			var columns = await _uow.GetAllMapedColumfGetParamStatisticns(entype);
			if (columns == null)
			{
				return BadRequest();
			}
		
			PdfReport pdfRep = new PdfReport();
			Document doc = pdfRep.CreateDocReport(report, components, columns);

			PdfDocumentRenderer renderer = new PdfDocumentRenderer();
			renderer.Document = doc;
			renderer.RenderDocument();

			using (var stream = new MemoryStream())
			{
				renderer.PdfDocument.Save(stream, false);
				stream.Position = 0;
				return File(stream.ToArray(), "application/pdf", "report.pdf");
			}
		}
	}
}