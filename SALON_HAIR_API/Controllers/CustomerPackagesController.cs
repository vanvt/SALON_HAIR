
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
    public class CustomerPackagesController : CustomControllerBase
    {
        private readonly ICustomerPackage _customerPackage;
        private readonly IUser _user;

        public CustomerPackagesController(ICustomerPackage customerPackage, IUser user)
        {
            _customerPackage = customerPackage;
            _user = user;
        }

        // GET: api/CustomerPackages
        [HttpGet]
        public IActionResult GetCustomerPackage(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _customerPackage.SearchAllFileds(keyword);
            var dataReturn =   _customerPackage.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/CustomerPackages/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerPackage([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var customerPackage =  _customerPackage.FindBy(e => e.CustomerId == id);
                customerPackage = customerPackage.Include(e => e.Package);
                if (customerPackage == null)
                {
                    return NotFound();
                }
                return Ok(customerPackage);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/CustomerPackages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerPackage([FromRoute] long id, [FromBody] CustomerPackage customerPackage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != customerPackage.Id)
            {
                return BadRequest();
            }
            try
            {
                customerPackage.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                await _customerPackage.EditAsync(customerPackage);
                return CreatedAtAction("GetCustomerPackage", new { id = customerPackage.Id }, customerPackage);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerPackageExists(id))
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

                  throw new UnexpectedException(customerPackage,e);
            }
        }

        // POST: api/CustomerPackages
        [HttpPost]
        public async Task<IActionResult> PostCustomerPackage([FromBody] CustomerPackage customerPackage)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                customerPackage.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                await _customerPackage.AddAsync(customerPackage);
                return CreatedAtAction("GetCustomerPackage", new { id = customerPackage.Id }, customerPackage);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(customerPackage,e);
            }
          
        }

        // DELETE: api/CustomerPackages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerPackage([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var customerPackage = await _customerPackage.FindAsync(id);
                if (customerPackage == null)
                {
                    return NotFound();
                }

                await _customerPackage.DeleteAsync(customerPackage);

                return Ok(customerPackage);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }
        [HttpGet("by-customer/{customerId}")]
        public IActionResult GetPackageByCustomer(long customerId, int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customerPackage = _customerPackage.FindBy(e => e.CustomerId == customerId);
            customerPackage = customerPackage.Include(e => e.Package);
            if (customerPackage == null)
            {
                return NotFound();
            }
            return OkList(customerPackage);
        }

        private bool CustomerPackageExists(long id)
        {
            return _customerPackage.Any<CustomerPackage>(e => e.Id == id);
        }
    }
}

