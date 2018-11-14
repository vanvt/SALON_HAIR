
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
    public class ServiceCategorysController : CustomControllerBase
    {
        private readonly IServiceCategory _serviceCategory;
        private readonly IUser _user;
        public ServiceCategorysController(IServiceCategory serviceCategory, IUser user)
        {
            _serviceCategory = serviceCategory;
            _user = user;
        }

        // GET: api/ServiceCategorys
        [HttpGet]
        public IActionResult GetServiceCategory(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _serviceCategory.SearchAllFileds(keyword);
            var dataReturn =   _serviceCategory.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/ServiceCategorys/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceCategory([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                // var serviceCategory = await _serviceCategory.FindAsync(id);
                var serviceCategory = await _serviceCategory.GetAll().Where(e => e.Id == id).Include(e => e.Service).FirstOrDefaultAsync();
                //await _productCategory.GetAll().Where(e => e.Id == id).Include(e => e.Product).FirstOrDefaultAsync();

                if (serviceCategory == null)
                {
                    return NotFound();
                }
                return Ok(serviceCategory);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/ServiceCategorys/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceCategory([FromRoute] long id, [FromBody] ServiceCategory serviceCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != serviceCategory.Id)
            {
                return BadRequest();
            }
            try
            {
                serviceCategory.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
                await _serviceCategory.EditAsync(serviceCategory);
                return CreatedAtAction("GetServiceCategory", new { id = serviceCategory.Id }, serviceCategory);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceCategoryExists(id))
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

                  throw new UnexpectedException(serviceCategory,e);
            }
        }

        // POST: api/ServiceCategorys
        [HttpPost]
        public async Task<IActionResult> PostServiceCategory([FromBody] ServiceCategory serviceCategory)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                serviceCategory.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
                await _serviceCategory.AddAsync(serviceCategory);
                return CreatedAtAction("GetServiceCategory", new { id = serviceCategory.Id }, serviceCategory);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(serviceCategory,e);
            }
          
        }

        // DELETE: api/ServiceCategorys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceCategory([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var serviceCategory = await _serviceCategory.FindAsync(id);
                if (serviceCategory == null)
                {
                    return NotFound();
                }

                await _serviceCategory.DeleteAsync(serviceCategory);

                return Ok(serviceCategory);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool ServiceCategoryExists(long id)
        {
            return _serviceCategory.Any<ServiceCategory>(e => e.Id == id);
        }
    }
}

