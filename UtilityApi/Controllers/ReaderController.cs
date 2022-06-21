using Domains;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.IO;
using System.Threading.Tasks;

namespace UtilityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        public ReaderController(ApplicationDbContext context)
        {
            _context = context;
        }
        private ApplicationDbContext _context;

        [HttpPost]
        public async Task<IActionResult> Read(IFormFile file)
        {
           
            string a = string.Empty;
            using(var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using(var package  = new ExcelPackage(stream))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    for(int row = 2; row < rowCount; row++)
                    {
                        Region region = new Region();
                        region.RegionIndex = int.Parse(worksheet.Cells[row, 1].Value.ToString().Trim());
                        region.UzName = worksheet.Cells[row, 2].Value.ToString().Trim();
                        region.RuName = worksheet.Cells[row, 3].Value.ToString().Trim();
                        region.EngName = worksheet.Cells[row, 4].Value.ToString().Trim();
                        _context.Regions.Add(region);
                    }
                }
            }
            _context.SaveChanges();
            return Ok();
        }
    }
}
