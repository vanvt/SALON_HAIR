
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
    public class CommissionServicesController : CustomControllerBase
    {
        private readonly ICommissionService _commissionService;
        private readonly IUser _user;

        public CommissionServicesController(ICommissionService commissionService, IUser user)
        {
            _commissionService = commissionService;
            _user = user;
        }

        // GET: api/CommissionServices
        [HttpGet("{salonBranchId}/{staffId}")]
        public IActionResult GetCommissionService(long salonBranchId, long staffId,  int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _commissionService.SearchAllFileds(keyword).Where(e => e.StaffId == staffId)
                .Where(e => e.SalonBranchId == salonBranchId);
            var dataReturn = _commissionService.LoadAllInclude(data);
            dataReturn = dataReturn.Include(e => e.Service).ThenInclude(e => e.ServiceCategory);
            return OkList(dataReturn);
        }

        // PUT: api/CommissionServices/5
      [HttpPut]
        public async Task<IActionResult> PutCommissionService( [FromBody] CommissionServiceVM commissionService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                commissionService.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals("emailAddress"));
                //Edit level Product
                if (commissionService.ServiceId != 0)
                {
                    await _commissionService.EditAsync(commissionService);
                    return Ok(commissionService);
                }

                //Edit lever Category Product
                if (commissionService.ServiceCategoryId != 0)
                {
                    await _commissionService.EditLevelGroupAsync(commissionService, commissionService.ServiceCategoryId);
                    return Ok(commissionService);
                }

                //Edit lever Branch
                if (commissionService.SalonBranchId != 0)
                {
                    await _commissionService.EditLevelBranchAsync(commissionService);
                    return Ok(commissionService);
                }
                return BadRequest("Are you kidding me ?");
            }

            catch (Exception e)
            {

                throw new UnexpectedException(commissionService, e);
            }
        }


    }
}

