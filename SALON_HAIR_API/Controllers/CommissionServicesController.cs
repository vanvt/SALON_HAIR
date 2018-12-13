
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
            var data = _commissionService.SearchAllFileds(keyword)
                .Where(e => e.StaffId == staffId)
                .Where(e => e.SalonBranchId == salonBranchId)
                .Where(e=>! e.Service.Status.Equals("DELETED"))
                .Where(e => !e.Service.ServiceCategory.Status.Equals("DELETED"));
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
                var currentSalonBranch = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;
                if (currentSalonBranch == null)
                {
                    return BadRequest("Are you kidding me ?");
                }

                commissionService.SalonBranchId = currentSalonBranch.Value;
                //Edit lever Category Product
                if (commissionService.ServiceCategoryId != 0)
                {

                  
                    var data =  await _commissionService.EditGetLevelGroupAsync(commissionService, commissionService.ServiceCategoryId);
                    data = data.Include(e=>e.Service).ThenInclude(e => e.ServiceCategory);
                    return Ok(data);
                }
                //Edit lever Branch
                if (commissionService.SalonBranchId != 0)
                {
                    var data  = await _commissionService.EditGetLevelBranchAsync(commissionService);
                    data = data.Include(e => e.Service).ThenInclude(e => e.ServiceCategory);
                    return Ok(data);
                }
                return BadRequest("Are you kidding me ?");
            }

            catch (Exception e)
            {

                throw new UnexpectedException(commissionService, e);
            }
        }
        private IQueryable<CommissionService> GetByCurrentSpaBranch(IQueryable<CommissionService> data)
        {
            var currentSalonBranch = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;

            if (currentSalonBranch != default || currentSalonBranch != 0)
            {
                data = data.Where(e => e.SalonBranchId == currentSalonBranch);
            }
            return data;
        }
        private IQueryable<CommissionService> GetByCurrentSalon(IQueryable<CommissionService> data)
        {
            data = data.Where(e => e.SalonBranch.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals(CLAIMUSER.SALONID)));
            return data;
        }


    }
}

