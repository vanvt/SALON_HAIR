
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
    public class WarehouseTransactionsController : CustomControllerBase
    {
        private readonly IWarehouseTransaction _warehouseDetail;
        private readonly IUser _user;

        public WarehouseTransactionsController(IWarehouseTransaction warehouseDetail, IUser user)
        {
            _warehouseDetail = warehouseDetail;
            _user = user;
        }

        // GET: api/WarehouseDetails
        [HttpGet]
        public IActionResult GetWarehouseDetail(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _warehouseDetail.SearchAllFileds(keyword);
            var dataReturn =   _warehouseDetail.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/WarehouseDetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouseDetail([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var warehouseDetail = await _warehouseDetail.FindAsync(id);

                if (warehouseDetail == null)
                {
                    return NotFound();
                }
                return Ok(warehouseDetail);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/WarehouseDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouseDetail([FromRoute] long id, [FromBody]WarehouseTransaction warehouseDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != warehouseDetail.Id)
            {
                return BadRequest();
            }
            try
            {
                warehouseDetail.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _warehouseDetail.EditAsync(warehouseDetail);
                return CreatedAtAction("GetWarehouseDetail", new { id = warehouseDetail.Id }, warehouseDetail);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!WarehouseDetailExists(id))
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

                  throw new UnexpectedException(warehouseDetail,e);
            }
        }

        // POST: api/WarehouseDetails
        [HttpPost]
        public async Task<IActionResult> PostWarehouseDetail([FromBody] WarehouseTransaction warehouseDetail)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                warehouseDetail.SalonId = JwtHelper.GetCurrentInformationLong(User, e => e.Type.Equals("salonId"));
                warehouseDetail.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _warehouseDetail.AddAsync(warehouseDetail);
                return CreatedAtAction("GetWarehouseDetail", new { id = warehouseDetail.Id }, warehouseDetail);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(warehouseDetail,e);
            }
          
        }

        // DELETE: api/WarehouseDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouseDetail([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var warehouseDetail = await _warehouseDetail.FindAsync(id);
                if (warehouseDetail == null)
                {
                    return NotFound();
                }

                await _warehouseDetail.DeleteAsync(warehouseDetail);

                return Ok(warehouseDetail);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool WarehouseDetailExists(long id)
        {
            return _warehouseDetail.Any<WarehouseTransaction>(e => e.Id == id);
        }
    }
}

