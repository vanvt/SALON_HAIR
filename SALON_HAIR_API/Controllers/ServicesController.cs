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
       
        public ServicesController(IServiceProduct serviceProduct,IService service, IUser user)
        {
            _service = service;
            _user = user;
            _serviceProduct = serviceProduct;
        }

        // GET: api/Services
        [HttpGet]
        public IActionResult GetService(int page = 1, int rowPerPage = 50, string keyword = "",long serviceCategoryId = 0, string orderBy = "", string orderType = "")
        {
            var data = _service.SearchAllFileds(keyword,orderBy,orderType).Include(e => e.ServiceProduct).ThenInclude(x => x.Product).ThenInclude(t => t.Unit);
            if (serviceCategoryId != 0)
            {
                data = data.Where(e => e.ServiceCategoryId == serviceCategoryId).Include(e => e.ServiceProduct).ThenInclude(x => x.Product).ThenInclude(t => t.Unit);             
            }
            return OkList(_service.Paging(data, page, rowPerPage));          
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

                throw;
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
                    throw new BadRequestException("Không thể tạo serive có hai sản phẩm giống nhau được babe");
                }
                service.UpdatedBy = JwtHelper.GetCurrentInformation(User,e=>e.Type.Equals("email"));
                await _service.EditAsync(service);
             
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

                throw;
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
                
                service.CreatedBy = _user.FindBy(e => e.Id == JwtHelper.GetIdFromToken(User.Claims)).FirstOrDefault().Email;
              
                await _service.AddAsync(service);               
                var serviceProduct = _serviceProduct.GetAll().Where(e => e.ServiceId == service.Id).Include(e => e.Product).ThenInclude(x=>x.Unit);
                service.ServiceProduct = serviceProduct.ToList();
                return CreatedAtAction("GetService", new { id = service.Id }, service);
            }
            catch (Exception e)
            {

                throw;
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

                throw;
            }
          
        }

        private bool ServiceExists(long id)
        {
            return _service.Any<Service>(e => e.Id == id);
        }
    }
}
