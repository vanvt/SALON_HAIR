
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using ULTIL_HELPER;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace SALON_HAIR_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PhotosController : CustomControllerBase
    {
        private readonly IPhoto _photo;
        private readonly IUser _user;
        public IConfiguration _configuration { get; }
        public PhotosController(IPhoto photo, IUser user, IConfiguration configuration)
        {
            _configuration = configuration;
            _photo = photo;
            _user = user;
        }

        // GET: api/Photos
        [HttpGet]
        public IActionResult GetAuthority(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            return OkList(_photo.Paging(_photo.SearchAllFileds(keyword, orderBy, orderType), page, rowPerPage));
        }
        // GET: api/Photos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhoto([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var photo = await _photo.FindAsync(id);

                if (photo == null)
                {
                    return NotFound();
                }
                return Ok(photo);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        // PUT: api/Photos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhoto([FromRoute] long id, [FromBody] Photo photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != photo.Id)
            {
                return BadRequest();
            }
            try
            {
          
                await _photo.EditAsync(photo);
                return CreatedAtAction("GetPhoto", new { id = photo.Id }, photo);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }           
            catch (Exception e)
            {

                throw;
            }
        }

        // POST: api/Photos
        [HttpPost]
        public async Task<IActionResult> PostPhoto(IFormCollection fileImageCollect)
        {

            try
            {
                var fileImage = fileImageCollect.Files[0];
                if (fileImage == null || fileImage.Length == 0)
                    return Content("file not selected");

               
               
                var uniqueFile = DateTime.Now.ToString("yyyyMMddhhmmss");
               
                var fileStore = uniqueFile + "-" + fileImage.FileName;
                var path = Path.Combine("wwwroot", "upload", "frontend",
                            fileStore);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await fileImage.CopyToAsync(stream);
                }

                Photo photo = new Photo()
                {
                    DataContentType = fileImage.ContentType,
                    Name = fileImage.Name,
                    OriginalName = fileImage.Name,
                    Path = Path.Combine( _configuration["URLAPI:value"],path),
                    Url = Path.Combine(_configuration["URLAPI:value"], "upload", "frontend", fileStore),

                };
                await _photo.AddAsync(photo);
                return CreatedAtAction("GetPhoto", new { id = photo.Id }, photo);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        // DELETE: api/Photos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var photo = await _photo.FindAsync(id);
                if (photo == null)
                {
                    return NotFound();
                }

                await _photo.DeleteAsync(photo);

                return Ok(photo);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        private bool PhotoExists(long id)
        {
            return _photo.Any<Photo>(e => e.Id == id);
        }
    }
}

