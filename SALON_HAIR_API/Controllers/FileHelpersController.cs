using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SALON_HAIR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileHelpersController : CustomControllerBase
    {
      
        // POST: api/FileHelpers
        [HttpPost]
        public async Task<IActionResult> Uploadfile(IFormFile file)
        {
            var uniqueFile = DateTime.Now.ToString("yyyyMMddhhmmss");
           
            if (file == null || file.Length == 0)
                return Content("file not selected");
            var fileStore = uniqueFile + "-" + file.FileName;
            var path = Path.Combine("wwwroot", "upload", "frontend",
                        fileStore);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok(new { Name = file.FileName, Url = Path.Combine("upload", "frontend", fileStore) });
        }
    }
}
