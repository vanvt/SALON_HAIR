
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
    public class CommissionPackagesController : CustomControllerBase
    {
        private readonly ICommissionPackage _commissionPackge;
        private readonly IUser _user;

        public CommissionPackagesController(ICommissionPackage commissionPackge, IUser user)
        {
            _commissionPackge = commissionPackge;
            _user = user;
        }

        // GET: api/CommissionPackges
        [HttpGet("{salonBranchId}/{staffId}")]
        public IActionResult GetCommissionPackge(long salonBranchId , long staffId,int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _commissionPackge.SearchAllFileds(keyword)
                .Where(e=>e.StaffId==staffId)
                .Where(e=>e.SalonBranchId==salonBranchId);
            var dataReturn =   _commissionPackge.LoadAllInclude(data);
            return OkList(dataReturn);
        }

        // PUT: api/CommissionPackges/5
        [HttpPut]
        public async Task<IActionResult> PutCommissionPackge([FromBody] CommissionPackage commissionPackge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            try
            {
                commissionPackge.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(CLAIMUSER.EMAILADDRESS));
                //Edit level Packge
                if (commissionPackge.PackageId != 0)
                {
                    await _commissionPackge.EditAsync(commissionPackge);
                    return Ok(commissionPackge);
                }

                //Edit lever Branch
                if (commissionPackge.SalonBranchId != 0)
                {
                    await _commissionPackge.EditLevelBranchAsync(commissionPackge);
                    return Ok(commissionPackge);
                }

                return BadRequest("Are you kidding me?");
            }
           
            catch (Exception e)
            {

                  throw new UnexpectedException(commissionPackge,e);
            }
        }

        private IQueryable<CommissionPackage> GetByCurrentSpaBranch(IQueryable<CommissionPackage> data)
        {
            var currentSalonBranch = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;

            if (currentSalonBranch != default || currentSalonBranch != 0)
            {
                data = data.Where(e => e.SalonBranchId == currentSalonBranch);
            }
            return data;
        }
        private IQueryable<CommissionPackage> GetByCurrentSalon(IQueryable<CommissionPackage> data)
        {
            data = data.Where(e => e.SalonBranch.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals(CLAIMUSER.SALONID)));
            return data;
        }

    }
}

