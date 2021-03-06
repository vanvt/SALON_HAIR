﻿
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
    public class ServicesController : CustomControllerBase
    {
        private readonly IService _service;
        private readonly IServiceProduct _serviceProduct;
        private readonly IUser _user;
        private readonly IServiceSalonBranch _serviceSalonBranch;
        public ServicesController(IServiceSalonBranch serviceSalonBranch,IServiceProduct serviceProduct, IService service, IUser user)
        {
            _serviceSalonBranch = serviceSalonBranch;
            _service = service;
            _user = user;
            _serviceProduct = serviceProduct;
        }

        // GET: api/Services
        [HttpGet]
        public IActionResult GetService(int page = 1, int rowPerPage = 50, string keyword = "", long serviceCategoryId = 0, string orderBy = "", string orderType = "")
        {
            var firstQuery = _service.SearchAllFileds(keyword, orderBy, orderType);
            firstQuery = GetByCurrentSalon(firstQuery);
            firstQuery = GetByCurrentSpaBranch(firstQuery);
            firstQuery = firstQuery.Where(e => e.ServiceCategory.Status.Equals(OBJECTSTATUS.ENABLE));
            var data = firstQuery.Include(e => e.ServiceProduct).ThenInclude(x => x.Product).ThenInclude(t => t.Unit);
            if (serviceCategoryId != 0)
            {
                data = data.Where(e => e.ServiceCategoryId == serviceCategoryId)
                    .Include(e => e.ServiceProduct)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(t => t.Unit);
            }
            return OkList(data);
        }
        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetService([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var service = await _service.FindAsync(id);

                if (service == null)
                {
                    return NotFound();
                }
                return Ok(service);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/Services/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService([FromRoute] long id, [FromBody] Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != service.Id)
            {
                return BadRequest();
            }
            try
            {
                if (service.ServiceProduct.Select(e => e.ProductId).Count() != service.ServiceProduct.Select(e => e.ProductId).Distinct().Count())
                {
                    throw new BadRequestException("Không th? t?o serive có hai s?n ph?m gi?ng nhau du?c babe");
                }
                service.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                await _service.EditMany2ManyAsync(service);

                var serviceProduct = _serviceProduct.GetAll().Where(e => e.ServiceId == service.Id).Include(e => e.Product).ThenInclude(x => x.Unit);

                service.ServiceProduct = serviceProduct.ToList();
                return CreatedAtAction("GetService", new { id = service.Id }, service);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

                throw new UnexpectedException(service, e);
            }
        }

        // POST: api/Services
        [HttpPost]
        public async Task<IActionResult> PostService([FromBody] Service service)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                service.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
               
                await _service.AddAsync(service);
                var serviceProduct = _serviceProduct.GetAll().Where(e => e.ServiceId == service.Id).Include(e => e.Product).ThenInclude(x => x.Unit);
                service.ServiceProduct = serviceProduct.ToList();
                return CreatedAtAction("GetService", new { id = service.Id }, service);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(service, e);
            }

        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var service = await _service.FindAsync(id);
                if (service == null)
                {
                    return NotFound();
                }

                await _service.DeleteAsync(service);

                return Ok(service);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id, e);
            }

        }

        private bool ServiceExists(long id)
        {
            return _service.Any<Service>(e => e.Id == id);
        }
        private IQueryable<Service> GetByCurrentSpaBranch(IQueryable<Service> data)
        {
            var currentSalonBranch = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;

            if (currentSalonBranch != default || currentSalonBranch != 0)
            {
                var listPackageAvailable = _serviceSalonBranch
               .FindBy(e => e.SalonBranchId == currentSalonBranch)
               .Where(e => e.Status.Equals("ENABLE"))
               .Select(e => e.ServiceId);
                data = data.Where(e => listPackageAvailable.Contains(e.Id));
            }
            return data;
        }
        private IQueryable<Service> GetByCurrentSalon(IQueryable<Service> data)
        {
            var salonId = JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals(CLAIMUSER.SALONID));
            data = data.Where(e => e.SalonId == salonId);
            return data;
        }
    }
}

