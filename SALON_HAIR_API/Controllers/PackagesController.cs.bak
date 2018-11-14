
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using ULTIL_HELPER;
using Microsoft.AspNetCore.Authorization;
using SALON_HAIR_API.Exceptions;

namespace SALON_HAIR_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PackagesController : CustomControllerBase
    {
        private readonly IPackage _package;
        private readonly IUser _user;
        private readonly IServicePackage _servicePackage;
        public PackagesController(IServicePackage servicePackage,IPackage package, IUser user)
        {
            _servicePackage = servicePackage;
            _package = package;
            _user = user;
        }

        // GET: api/Packages
        [HttpGet]
        public IActionResult GetPackage(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            return OkList(_package.Paging( _package.SearchAllFileds(keyword),page,rowPerPage).Include(e=>e.ServicePackage).ThenInclude(x=>x.Service));
        }
        // GET: api/Packages/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPackage([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var package = await _package.FindAsync(id);

                if (package == null)
                {
                    return NotFound();
                }
                return Ok(package);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        // PUT: api/Packages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackage([FromRoute] long id, [FromBody] Package package)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != package.Id)
            {
                return BadRequest();
            }
            try
            {
                //package.UpdatedBy = _user.FindBy(e => e.Id == JwtHelper.GetIdFromToken(User.Claims)).FirstOrDefault().Email;
                //await _package.EditAsync(package);
                //return CreatedAtAction("GetPackage", new { id = package.Id }, package);

                if (package.ServicePackage.Select(e => e.ServiceId).Count() != package.ServicePackage.Select(e => e.ServiceId).Distinct().Count())
                {
                    throw new BadRequestException("Không thể tạo gói dịch vụ có hai dịch vụ giống nhau được babe");
                }
                //service.ServiceProduct.ToList().ForEach(e =>
                //{
                //    e.Product = null;
                //});
                package.UpdatedBy = ""+JwtHelper.GetIdFromToken(User.Claims);
                await _package.EditAsync(package);

                var servicePackage = _servicePackage.GetAll().Where(e => e.PackageId == package.Id).Include(e => e.Service);

                package.ServicePackage = servicePackage.ToList();
                return CreatedAtAction("GetPackage", new { id = package.Id }, package);

            }

            catch (DbUpdateConcurrencyException)
            {
                if (!PackageExists(id))
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

        // POST: api/Packages
        [HttpPost]
        public async Task<IActionResult> PostPackage([FromBody] Package package)
        {

            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (package.ServicePackage.Select(e => e.ServiceId).Count() != package.ServicePackage.Select(e => e.ServiceId).Distinct().Count())
                {
                    throw new BadRequestException("Không thể tạo gói dịch vụ có hai dịch vụ giống nhau được babe");
                }
                package.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));
                await _package.AddAsync(package);
                var servicePackge = _servicePackage.FindBy(e => e.PackageId == package.Id).Include(e=>e.Service);
                package.ServicePackage = servicePackge.ToList();
                return CreatedAtAction("GetPackage", new { id = package.Id }, package);
            }
            catch (Exception e)
            {
                throw;
            }
          
        }

        // DELETE: api/Packages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var package = await _package.FindAsync(id);
                if (package == null)
                {
                    return NotFound();
                }

                await _package.DeleteAsync(package);

                return Ok(package);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        private bool PackageExists(long id)
        {
            return _package.Any<Package>(e => e.Id == id);
        }
        [HttpPut("change-status/{id}")]
        public async Task<IActionResult> ChangeStatusAsync([FromBody] Package package, [FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var oldPackage = await _package.FindAsync(id);
                if (oldPackage == null)
                {
                    return NotFound();
                }

                oldPackage.Status = package.Status.ToUpper();
                await _package.EditAsync(oldPackage);
                return Ok(oldPackage);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}

