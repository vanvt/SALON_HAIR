
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
            var start = DateTime.Now;
            Console.WriteLine("GetPackage");
            Console.WriteLine("-----------------------------------------------------------------------------");
            var data = _package.SearchAllFileds(keyword).Where
                (e => e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals("salonId"))); ;
            var dataReturn = data.Include(e=>e.ServicePackage).ThenInclude(x=>x.Service);
            //var dataReturn = data.Include(e => e.ServicePackage);
            var end = DateTime.Now;
            Console.WriteLine($"Finished in {(end - start).TotalMilliseconds} miliseconds");
            Console.WriteLine("-----------------------------------------------------------------------------");
            return OkList(dataReturn);
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

                  throw new UnexpectedException(id, e);
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
                  package.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
             

                if (package.ServicePackage.Select(e => e.ServiceId).Count() != package.ServicePackage.Select(e => e.ServiceId).Distinct().Count())
                {
                    throw new BadRequestException("Không thể tạo gói dịch vụ có hai dịch vụ giống nhau được babe");
                }
     
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

                  throw new UnexpectedException(package,e);
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
                package.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _package.AddAsync(package);
                var servicePackge = _servicePackage.FindBy(e => e.PackageId == package.Id).Include(e=>e.Service);
                package.ServicePackage = servicePackge.ToList();
                return CreatedAtAction("GetPackage", new { id = package.Id }, package);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(package,e);
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

                throw new UnexpectedException(id,e);
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

