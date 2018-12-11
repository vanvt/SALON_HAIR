
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
    public class WarehouseStatusController : CustomControllerBase
    {
        private readonly IWarehouseStatus _warehouseStatus;
        private readonly IUser _user;

        public WarehouseStatusController(IWarehouseStatus warehouseStatus, IUser user)
        {
            _warehouseStatus = warehouseStatus;
            _user = user;
        }

        // GET: api/WarehouseStatuss
        [HttpGet]
        public IActionResult GetWarehouseStatus(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _warehouseStatus.SearchAllFileds(keyword);
            var dataReturn =   _warehouseStatus.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/WarehouseStatuss/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouseStatus([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var warehouseStatus = await _warehouseStatus.FindAsync(id);

                if (warehouseStatus == null)
                {
                    return NotFound();
                }
                return Ok(warehouseStatus);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/WarehouseStatuss/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouseStatus([FromRoute] long id, [FromBody] WarehouseStatus warehouseStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != warehouseStatus.Id)
            {
                return BadRequest();
            }
            try
            {
                warehouseStatus.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _warehouseStatus.EditAsync(warehouseStatus);
                return CreatedAtAction("GetWarehouseStatus", new { id = warehouseStatus.Id }, warehouseStatus);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!WarehouseStatusExists(id))
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

                  throw new UnexpectedException(warehouseStatus,e);
            }
        }

        // POST: api/WarehouseStatuss
        [HttpPost]
        public async Task<IActionResult> PostWarehouseStatus([FromBody] WarehouseStatus warehouseStatus)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                warehouseStatus.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                await _warehouseStatus.AddAsync(warehouseStatus);
                return CreatedAtAction("GetWarehouseStatus", new { id = warehouseStatus.Id }, warehouseStatus);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(warehouseStatus,e);
            }
          
        }

        // DELETE: api/WarehouseStatuss/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouseStatus([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var warehouseStatus = await _warehouseStatus.FindAsync(id);
                if (warehouseStatus == null)
                {
                    return NotFound();
                }

                await _warehouseStatus.DeleteAsync(warehouseStatus);

                return Ok(warehouseStatus);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool WarehouseStatusExists(long id)
        {
            return _warehouseStatus.Any<WarehouseStatus>(e => e.Id == id);
        }
    }
}

