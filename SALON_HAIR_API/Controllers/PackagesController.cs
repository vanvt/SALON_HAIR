
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
        private readonly IPackageSalonBranch _packageSalonBranch;
        private readonly IUser _user;
        private readonly IServicePackage _servicePackage;
        private readonly ICustomerPackage _customerPackage;
        public PackagesController(ICustomerPackage customerPackage, IPackageSalonBranch packageSalonBranch,IServicePackage servicePackage,IPackage package, IUser user)
        {
            _customerPackage = customerPackage;
            _packageSalonBranch = packageSalonBranch;
            _servicePackage = servicePackage;
            _package = package;
            _user = user;
        }

        // GET: api/Packages
        [HttpGet]
        public IActionResult GetPackage(long salonBranchId = 0,int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _package.SearchAllFileds(keyword);
            data = GetByCurrentSalon(data);
            data = GetByCurrentSpaBranch(data);          
            var dataReturn = data.Include(e=>e.ServicePackage).ThenInclude(x=>x.Service);          
            return OkList(dataReturn);
        }
        //[HttpGet("by-customer/{customerId}")]
        //public IActionResult GetPackageByCustomer(long customerId, int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var customerPackage = _customerPackage.FindBy(e => e.CustomerId == customerId);
        //    customerPackage = customerPackage.Include(e => e.Package);
        //    if (customerPackage == null)
        //    {
        //        return NotFound();
        //    }
        //    return OkList(customerPackage);
        //}

        [HttpGet("setting")]
        public IActionResult GetPackageSetting(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var currentSalonBranchId = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;
            var dataKeyword = _package.SearchAllFileds(keyword).Select(e => e.Id);          
            var data = _packageSalonBranch.GetAll()
                .Where(e => e.SalonBranchId == currentSalonBranchId)
                .Where(e => dataKeyword.Contains(e.PackageId));
            data = data.Include(e => e.Package);
            return OkList(data);
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
                  package.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
             

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
        [HttpPut("setting/{id}")]
        public async Task<IActionResult> PutPackageSetting([FromRoute] long id, [FromBody] PackageSalonBranch package)
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
                package.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                await _packageSalonBranch.EditAsync(package);            
                return Ok(package);

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

                throw new UnexpectedException(package, e);
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
                package.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                package.SalonId = JwtHelper.GetCurrentInformationLong(User, e => e.Type.Equals("salonId"));
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
        private IQueryable<Package> GetByCurrentSpaBranch(IQueryable<Package> data)
        {
            var currentSalonBranch = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;

            if (currentSalonBranch != default || currentSalonBranch != 0)
            {
                var listPackageAvailable = _packageSalonBranch
               .FindBy(e => e.SalonBranchId == currentSalonBranch)
               .Where(e => e.Status.Equals("ENABLE"))
               .Select(e => e.PackageId);
                data = data.Where(e => listPackageAvailable.Contains(e.Id));
            }
            return data;
        }
        private IQueryable<Package> GetByCurrentSalon(IQueryable<Package> data)
        {
            data = data.Where(e => e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals(CLAIMUSER.SALONID)));
            return data;
        }
    }
}

