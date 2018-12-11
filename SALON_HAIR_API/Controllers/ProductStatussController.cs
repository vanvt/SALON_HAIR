
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
    
    public class ProductStatussController : CustomControllerBase
    {
        private readonly IProductStatus _productStatus;
        private readonly IUser _user;

        public ProductStatussController(IProductStatus productStatus, IUser user)
        {
            _productStatus = productStatus;
            _user = user;
        }

        // GET: api/ProductStatuss
        [HttpGet]
        public IActionResult GetProductStatus(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _productStatus.SearchAllFileds(keyword).Where
                (e => e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals("salonId"))); ;
            var dataReturn =   _productStatus.LoadAllInclude(data);
            return  OkList(dataReturn);
        }
        //// GET: api/ProductStatuss/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetProductStatus([FromRoute] long id)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        var productStatus = await _productStatus.FindAsync(id);

        //        if (productStatus == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(productStatus);
        //    }
        //    catch (Exception e)
        //    {

        //          throw new UnexpectedException(id, e);
        //    }
        //}

        //// PUT: api/ProductStatuss/5
        //[HttpPut("{id}")]
        //[Authorize]
        //public async Task<IActionResult> PutProductStatus([FromRoute] long id, [FromBody] ProductStatus productStatus)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    if (id != productStatus.Id)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        productStatus.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
        //        await _productStatus.EditAsync(productStatus);
        //        return CreatedAtAction("GetProductStatus", new { id = productStatus.Id }, productStatus);
        //    }

        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductStatusExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }           
        //    catch (Exception e)
        //    {

        //          throw new UnexpectedException(productStatus,e);
        //    }
        //}

        //// POST: api/ProductStatuss
        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> PostProductStatus([FromBody] ProductStatus productStatus)
        //{

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        productStatus.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
        //        await _productStatus.AddAsync(productStatus);
        //        return CreatedAtAction("GetProductStatus", new { id = productStatus.Id }, productStatus);
        //    }
        //    catch (Exception e)
        //    {

        //        throw new UnexpectedException(productStatus,e);
        //    }
          
        //}

        //// DELETE: api/ProductStatuss/5
        //[HttpDelete("{id}")]
        //[Authorize]
        //public async Task<IActionResult> DeleteProductStatus([FromRoute] long id)
        //{

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        var productStatus = await _productStatus.FindAsync(id);
        //        if (productStatus == null)
        //        {
        //            return NotFound();
        //        }

        //        await _productStatus.DeleteAsync(productStatus);

        //        return Ok(productStatus);
        //    }
        //    catch (Exception e)
        //    {

        //        throw new UnexpectedException(id,e);
        //    }
          
        //}

        //private bool ProductStatusExists(long id)
        //{
        //    return _productStatus.Any<ProductStatus>(e => e.Id == id);
        //}
    }
}

