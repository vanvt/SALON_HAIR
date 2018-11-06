
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using ULTIL_HELPER;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using SALON_HAIR_API.ViewModels;
using AutoMapper;

namespace SALON_HAIR_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProductCategorysController : CustomControllerBase
    {
        private readonly IProductCategory _productCategory;
        private readonly IMapper _mapper;
        private readonly IUser _user;
        public ProductCategorysController(IMapper mapper,IProductCategory productCategory, IUser user)
        {
            _mapper = mapper;
            _productCategory = productCategory;
            _user = user;
        }

        // GET: api/ProductCategorys
    
            private ProductVM convert(Product product)
        {
            return _mapper.Map<ProductVM>(product);
        }
        private ProductCategoryVM convert(ProductCategory productCategory) {
            
            var dataReturn = _mapper.Map<ProductCategoryVM>(productCategory);
            //dataReturn.Product = productCategory.Product.AsQueryable().Select(e=>convert(e));         
            return dataReturn;
        }
        [HttpGet]
        public IActionResult GetProductCategory(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var firstQuery  = _productCategory.SearchAllFileds(keyword, orderBy, orderType);
            var seccondsQuery = _productCategory.Paging(firstQuery, page, rowPerPage).Include(e => e.Product).ThenInclude(e=>e.Unit);                  
            return OkList(seccondsQuery);
        }
        // GET: api/ProductCategorys/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductCategory([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var productCategory = await _productCategory.GetAll().Where(e=>e.Id == id).Include(e=>e.Product).FirstOrDefaultAsync();

                if (productCategory == null)
                {
                    return NotFound();
                }
                return Ok(productCategory);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        // PUT: api/ProductCategorys/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCategory([FromRoute] long id, [FromBody] ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != productCategory.Id)
            {
                return BadRequest();
            }
            try
            {
                productCategory.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));
                await _productCategory.EditAsync(productCategory);
                return CreatedAtAction("GetProductCategory", new { id = productCategory.Id }, productCategory);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(id))
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

        // POST: api/ProductCategorys
        [HttpPost]
        public async Task<IActionResult> PostProductCategory([FromBody] ProductCategory productCategory)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                productCategory.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));
                productCategory.Created = DateTime.Now;
              
                await _productCategory.AddAsync(productCategory);
                return CreatedAtAction("GetProductCategory", new { id = productCategory.Id }, productCategory);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        // DELETE: api/ProductCategorys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCategory([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var productCategory = await _productCategory.FindAsync(id);
                if (productCategory == null)
                {
                    return NotFound();
                }

                await _productCategory.DeleteAsync(productCategory);

                return Ok(productCategory);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        private bool ProductCategoryExists(long id)
        {
            return _productCategory.Any<ProductCategory>(e => e.Id == id);
        }
    }
}

