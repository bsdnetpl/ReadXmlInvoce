using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadXmlInvoce.Service;

namespace ReadXmlInvoce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoceXmlController : ControllerBase
    {
        private readonly IInvoceFromXml _invoceFromXml;

        public InvoceXmlController(IInvoceFromXml invoceFromXml)
        {
            _invoceFromXml = invoceFromXml;
        }
        [HttpPost("AddNewInvoce")]
        public ActionResult<bool> AddNewInvoce(string fileName)
        {
            _invoceFromXml.ReadXml(fileName);
            return true;
        }
    }
}
