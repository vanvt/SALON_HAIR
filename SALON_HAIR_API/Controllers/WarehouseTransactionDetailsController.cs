
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
    public class WarehouseTransactionDetailsController : CustomControllerBase
    {
        private readonly IWarehouseTransactionDetail _warehouseTransactionDetail;
        private readonly IUser _user;

        public WarehouseTransactionDetailsController(IWarehouseTransactionDetail warehouseTransactionDetail, IUser user)
        {
            _warehouseTransactionDetail = warehouseTransactionDetail;
            _user = user;
        }

        // GET: api/WarehouseTransactionDetails
        [HttpGet]
        public IActionResult GetWarehouseTransactionDetail(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _warehouseTransactionDetail.SearchAllFileds(keyword);
            var dataReturn =   _warehouseTransactionDetail.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/WarehouseTransactionDetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouseTransactionDetail([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var warehouseTransactionDetail = await _warehouseTransactionDetail.FindAsync(id);

                if (warehouseTransactionDetail == null)
                {
                    return NotFound();
                }
                return Ok(warehouseTransactionDetail);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/WarehouseTransactionDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouseTransactionDetail([FromRoute] long id, [FromBody] WarehouseTransactionDetail warehouseTransactionDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != warehouseTransactionDetail.Id)
            {
                return BadRequest();
            }
            try
            {
                warehouseTransactionDetail.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _warehouseTransactionDetail.EditAsync(warehouseTransactionDetail);
                return CreatedAtAction("GetWarehouseTransactionDetail", new { id = warehouseTransactionDetail.Id }, warehouseTransactionDetail);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!WarehouseTransactionDetailExists(id))
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

                  throw new UnexpectedException(warehouseTransactionDetail,e);
            }
        }

        // POST: api/WarehouseTransactionDetails
        [HttpPost]
        public async Task<IActionResult> PostWarehouseTransactionDetail([FromBody] WarehouseTransactionDetail warehouseTransactionDetail)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                warehouseTransactionDetail.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
                await _warehouseTransactionDetail.AddAsync(warehouseTransactionDetail);
                return CreatedAtAction("GetWarehouseTransactionDetail", new { id = warehouseTransactionDetail.Id }, warehouseTransactionDetail);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(warehouseTransactionDetail,e);
            }
          
        }

        // DELETE: api/WarehouseTransactionDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouseTransactionDetail([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var warehouseTransactionDetail = await _warehouseTransactionDetail.FindAsync(id);
                if (warehouseTransactionDetail == null)
                {
                    return NotFound();
                }

                await _warehouseTransactionDetail.DeleteAsync(warehouseTransactionDetail);

                return Ok(warehouseTransactionDetail);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool WarehouseTransactionDetailExists(long id)
        {
            return _warehouseTransactionDetail.Any<WarehouseTransactionDetail>(e => e.Id == id);
        }
    }
}

