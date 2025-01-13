using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace XMLProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet("GetCustomerDetails")]
        public IActionResult GetCustomerDetails()
        {
            // Path to your XML file (adjust this based on your file location)
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "GetCustomerByCifResponse.xml");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("The XML file was not found.");
            }

            // Load the XML document
            XDocument xmlDoc;
            try
            {
                xmlDoc = XDocument.Load(filePath);
            }
            catch
            {
                return BadRequest("Failed to load the XML file.");
            }

            // Extract the required data
            var customerDetails = xmlDoc.Descendants("Body").Select(body => new
            {
                Name = body.Element("Full_Name_EN")?.Value,
                Gndr = body.Element("Gndr")?.Value,
                Age = body.Element("Age")?.Value,
                Email = body.Element("Email")?.Value
            }).FirstOrDefault();

            if (customerDetails == null)
            {
                return NotFound("Customer details not found in the XML file");
            }

            // Return the extracted data as JSON
            return Ok(customerDetails);
        }
    }
}
