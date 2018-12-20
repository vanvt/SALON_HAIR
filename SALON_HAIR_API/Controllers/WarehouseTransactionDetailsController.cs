
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
        public IActionResult GetWarehouseTransactionDetail(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = ""
            ,long productId = 0,string action = "", string start = "",string end ="")
        {

            var data = _warehouseTransactionDetail.SearchAllFileds(keyword);
            data = GetByCurrentSalon(data);
            data = GetByCurrentSpaBranch(data);
            var dateRange = GetDateRangeQueryBack(start, end);
            data = productId == 0 ? data : data.Where(e => e.ProductId == productId);
            data = string.IsNullOrEmpty(action)? data : data.Where(e => e.WarehouseTransaction.Action.Equals(action));
            data = data.Where(e => e.Created.Value.Date >= dateRange.Item1.Date && e.Created.Value.Date <= dateRange.Item2.Date.Date);
            var dataReturn =  _warehouseTransactionDetail.LoadAllInclude(data);
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
                warehouseTransactionDetail.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
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
                warehouseTransactionDetail.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
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
        private IQueryable<WarehouseTransactionDetail> GetByCurrentSpaBranch(IQueryable<WarehouseTransactionDetail> data)
        {
            var currentSalonBranch = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;

            if (currentSalonBranch != default || currentSalonBranch != 0)
            {
                data = data.Where(e => e.WarehouseTransaction.SalonBranchId == currentSalonBranch);
            }
            return data;
        }
        private IQueryable<WarehouseTransactionDetail> GetByCurrentSalon(IQueryable<WarehouseTransactionDetail> data)
        {
            var salonId = JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals(CLAIMUSER.SALONID));
            data = data.Where(e => e.WarehouseTransaction.SalonId == salonId);
            return data;
        }
        private Tuple<DateTime, DateTime> GetDateRangeQuery(string start, string end)
        {
            start += "";
            end += "";
            var st = DateTime.Now;
            var en = DateTime.Now.AddDays(30.0);
            if (!string.IsNullOrEmpty(start))
            {

                st = DateTime.Parse(start);
            }
            if (!string.IsNullOrEmpty(end))
            {
                en = DateTime.Parse(end);
            }
            return Tuple.Create(st, en);
        }
        private Tuple<DateTime, DateTime> GetDateRangeQueryBack(string start, string end)
        {
            start += "";
            end += "";
            var st = DateTime.Now.AddDays(-30); 
            var en = DateTime.Now;
            if (!string.IsNullOrEmpty(start))
            {

                st = DateTime.Parse(start);
            }
            if (!string.IsNullOrEmpty(end))
            {
                en = DateTime.Parse(end);
            }
            return Tuple.Create(st, en);
        }
    }
}

