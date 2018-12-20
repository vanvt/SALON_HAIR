
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
    public class WarehousesController : CustomControllerBase
    {
        private readonly IWarehouse _warehouse;
        private readonly IProduct _product;
        private readonly IUser _user;
        public WarehousesController(IProduct product, IWarehouse warehouse, IUser user)
        {
            _product = product;
            _warehouse = warehouse;
            _user = user;
        }

        // GET: api/Warehouses
        [HttpGet]
        public IActionResult GetWarehouse(long warehouseStatusId=0, int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {

            var data = _warehouse.GetAll();

            data = GetByCurrentSalon(data);
            data = GetByCurrentSpaBranch(data);
            data = data.Where(e => !e.Product.Status.Equals("DELETED"));
            if (!string.IsNullOrEmpty(keyword))
            {
                var listProductSearch = _product.SearchAllFileds(keyword)
                .Where(e => e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals("salonId"))).Select(e => e.Id).ToList();
                data = data.Where(e => listProductSearch.Contains(e.ProductId.Value));
            }          
            if (warehouseStatusId != 0)
            {
                data = data.Where(e => e.WarehouseStatusId == warehouseStatusId);
            }
            var dataReturn = _warehouse.LoadAllInclude(data);
            return OkList(dataReturn);
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
                var warehouse = await _warehouse.FindBy(e=>e.Id==id)
                    .Include(e=>e.Product)
                        .ThenInclude(e=>e.ProductCountUnit)
                    .Include(e => e.Product)
                        .ThenInclude(e=>e.Unit)
                    .Include(e => e.Product)
                        .ThenInclude(e => e.ProductCategory)
                    .Include(e => e.Product)
                        .ThenInclude(e => e.ProductStatus)
                    .Include(e => e.Product)
                        .ThenInclude(e => e.Source)
                    .FirstOrDefaultAsync();

                if (warehouse == null)
                {
                    return NotFound();
                }
                return Ok(warehouse);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id, e);
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
                warehouse.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
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

                throw new UnexpectedException(warehouse, e);
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
                warehouse.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                await _warehouse.AddAsync(warehouse);
                return CreatedAtAction("GetWarehouse", new { id = warehouse.Id }, warehouse);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(warehouse, e);
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

                throw new UnexpectedException(id, e);
            }

        }

        private bool WarehouseExists(long id)
        {
            return _warehouse.Any<Warehouse>(e => e.Id == id);
        }
        private IQueryable<Warehouse> GetByCurrentSpaBranch(IQueryable<Warehouse> data)
        {
            var currentSalonBranch = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;

            if (currentSalonBranch != default || currentSalonBranch != 0)
            {
                data = data.Where(e => e.SalonBranchId == currentSalonBranch);
            }
            return data;
        }
        private IQueryable<Warehouse> GetByCurrentSalon(IQueryable<Warehouse> data)
        {
            var salonId = JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals(CLAIMUSER.SALONID));
            data = data.Where(e => e.SalonId == salonId);
            return data;
        }
    }
}

