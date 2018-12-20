
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
using SALON_HAIR_API.ViewModels;

namespace SALON_HAIR_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CommissionProductsController : CustomControllerBase
    {
        private readonly ICommissionProduct _commissionProduct;
        private readonly IUser _user;
        public CommissionProductsController(ICommissionProduct commissionProduct, IUser user)
        {
            _commissionProduct = commissionProduct;
            _user = user;
        }
        // GET: api/CommissionProducts
        [HttpGet("{salonBranchId}/{staffId}")]
        public IActionResult GetCommissionProduct(long salonBranchId, long staffId, int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _commissionProduct.SearchAllFileds(keyword)
                 .Where(e => e.StaffId == staffId)
                .Where(e => e.SalonBranchId == salonBranchId)
                .Where(e => !e.Product.Status.Equals("DELETED"))
                .Where(e => !e.Product.ProductCategory.Status.Equals("DELETED"));
            var dataReturn = _commissionProduct.LoadAllInclude(data);

            dataReturn = dataReturn.Include(e => e.Product).ThenInclude(e => e.ProductCategory);
            return OkList(dataReturn);
        }
        // PUT: api/CommissionProducts/5
        [HttpPut]
        public async Task<IActionResult> PutCommissionProduct([FromRoute] long salonId,[FromBody] CommissionProductVM commissionProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }         
            try
            {
                commissionProduct.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                //Edit level Product
                if (commissionProduct.ProductId != 0)
                {
                    await _commissionProduct.EditAsync(commissionProduct);
                    return Ok(commissionProduct);
                }


                var currentSalonBranch = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;
                if (currentSalonBranch == null)
                {
                    return BadRequest("Are you kidding me ?");
                }
                commissionProduct.SalonBranchId = currentSalonBranch.Value;

                //Edit lever Category Product
                if (commissionProduct.ProductCategoryId != 0)
                {
                  var data =   await _commissionProduct.EditGetLevelGroupAsync(commissionProduct, commissionProduct.ProductCategoryId);
                    data = data.Include(e => e.Product).ThenInclude(e => e.ProductCategory);
                    return Ok(data);
                }

                //Edit lever Branch
                if (commissionProduct.SalonBranchId != 0)
                {
                    var data =  await _commissionProduct.EditGetLevelBranchAsync(commissionProduct);
                    data = data.Include(e => e.Product).ThenInclude(e => e.ProductCategory);
                    return Ok(data);
                }
                return BadRequest("Are you kidding me ?");
            }

            catch (Exception e)
            {

                  throw new UnexpectedException(commissionProduct,e);
            }
        }
        private IQueryable<CommissionProduct> GetByCurrentSpaBranch(IQueryable<CommissionProduct> data)
        {
            var currentSalonBranch = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;

            if (currentSalonBranch != default || currentSalonBranch != 0)
            {
                data = data.Where(e => e.SalonBranchId == currentSalonBranch);
            }
            return data;
        }
        private IQueryable<CommissionProduct> GetByCurrentSalon(IQueryable<CommissionProduct> data)
        {
            var salonId = JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals(CLAIMUSER.SALONID));
            data = data.Where(e => e.SalonBranch.SalonId == salonId);
            return data;
        }
    }
}

