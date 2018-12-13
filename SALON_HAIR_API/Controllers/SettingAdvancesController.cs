
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
    public class SettingAdvancesController : CustomControllerBase
    {
        private readonly ISettingAdvance _settingAdvance;
        private readonly IUser _user;

        public SettingAdvancesController(ISettingAdvance settingAdvance, IUser user)
        {
            _settingAdvance = settingAdvance;
            _user = user;
        }

        // GET: api/SettingAdvances
        [HttpGet]
        public IActionResult GetSettingAdvance(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "",string group="")
        {
            var data = _settingAdvance.SearchAllFileds(keyword)
                .Where(e => e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals("salonId")));
            if (!string.IsNullOrEmpty(group))
            {
                data = data.Where(e => e.Group.Equals(group));
            }
            var dataReturn =   _settingAdvance.LoadAllInclude(data);
            SettingAdvanceVM settingAdvanceVM = new SettingAdvanceVM();
            settingAdvanceVM.settingAdvances = dataReturn.ToList() ;
            return Ok(settingAdvanceVM);
        }
      
        // PUT: api/SettingAdvances/5
        [HttpPut("{salonId}")]
        public async Task<IActionResult> PutSettingAdvance([FromRoute] long salonId, [FromBody] SettingAdvanceVM settingAdvance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            try
            {
              
                await _settingAdvance.EditRangeAsync(settingAdvance.settingAdvances);
                return Ok(settingAdvance);
            }

           
            catch (Exception e)
            {

                  throw new UnexpectedException(settingAdvance,e);
            }
        }

      
    }
}

