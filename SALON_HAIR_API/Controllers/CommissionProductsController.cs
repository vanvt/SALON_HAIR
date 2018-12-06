
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
        public IActionResult GetCommissionProduct(long salonBranchId,long staffId ,int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _commissionProduct.SearchAllFileds(keyword)
                 .Where(e => e.StaffId == staffId)
                .Where(e => e.SalonBranchId == salonBranchId); 

            var dataReturn =   _commissionProduct.LoadAllInclude(data);
            dataReturn = dataReturn.Include(e => e.Product).ThenInclude(e => e.ProductCategory);
            return OkList(dataReturn);
        }
    
        // PUT: api/CommissionProducts/5
        [HttpPut]
        public async Task<IActionResult> PutCommissionProduct([FromBody] CommissionProductVM commissionProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }         
            try
            {
                commissionProduct.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                //Edit level Product
                if (commissionProduct.ProductId != 0)
                {
                    await _commissionProduct.EditAsync(commissionProduct);
                    return CreatedAtAction("GetCommissionProduct", commissionProduct);
                }

                //Edit lever Category Product
                if (commissionProduct.ProductCategoryId != 0)
                {
                    await _commissionProduct.EditLevelGroupAsync(commissionProduct, commissionProduct.ProductCategoryId);
                    return CreatedAtAction("GetCommissionProduct", commissionProduct);
                }

                //Edit lever Branch
                if (commissionProduct.SalonBranchId != 0)
                {
                    await _commissionProduct.EditLevelBranchAsync(commissionProduct);
                    return CreatedAtAction("GetCommissionProduct", commissionProduct);
                }
                return BadRequest("Are you kidding me ?");
            }

            catch (Exception e)
            {

                  throw new UnexpectedException(commissionProduct,e);
            }
        }    
    }
}

