
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALON_HAIR_ENTITY.Entities;
using SALON_HAIR_CORE.Interface;
using ULTIL_HELPER;
using Microsoft.AspNetCore.Authorization;
namespace SALON_HAIR_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class WarehousesController : CustomControllerBase
    {
        private readonly IWarehouse _warehouse;
        private readonly IUser _user;
        public WarehousesController(IWarehouse warehouse, IUser user)
        {
            _warehouse = warehouse;
            _user = user;
        }

        // GET: api/Warehouses
        [HttpGet]
        public IActionResult GetWarehouse(int page = 1, int rowPerPage = 50, string keyword = "")
        {
            return OkList(_warehouse.Paging( _warehouse.SearchAllFileds(keyword),page,rowPerPage));
        }
        // GET: api/Warehouses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouse([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var warehouse = await _warehouse.FindAsync(id);

                if (warehouse == null)
                {
                    return NotFound();
                }
                return Ok(warehouse);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        // PUT: api/Warehouses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse([FromRoute] long id, [FromBody] Warehouse warehouse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != warehouse.Id)
            {
                return BadRequest();
            }
            try
            {
                warehouse.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));
                await _warehouse.EditAsync(warehouse);
                return CreatedAtAction("GetWarehouse", new { id = warehouse.Id }, warehouse);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!WarehouseExists(id))
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

        // POST: api/Warehouses
        [HttpPost]
        public async Task<IActionResult> PostWarehouse([FromBody] Warehouse warehouse)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                warehouse.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("email"));
                await _warehouse.AddAsync(warehouse);
                return CreatedAtAction("GetWarehouse", new { id = warehouse.Id }, warehouse);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        // DELETE: api/Warehouses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var warehouse = await _warehouse.FindAsync(id);
                if (warehouse == null)
                {
                    return NotFound();
                }

                await _warehouse.DeleteAsync(warehouse);

                return Ok(warehouse);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        private bool WarehouseExists(long id)
        {
            return _warehouse.Any<Warehouse>(e => e.Id == id);
        }
    }
}

