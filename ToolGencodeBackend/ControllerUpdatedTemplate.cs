using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGencodeBackend
{
    public static class ControllerUpdatedTemplate
    {
        public static string tempalte = @"
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
    [Route(""[controller]"")]
    [ApiController]
    [Authorize]
    public class {ClassName}sController : CustomControllerBase
    {
        private readonly I{ClassName} _{InstanceName};
        private readonly IUser _user;

        public {ClassName}sController(I{ClassName} {InstanceName}, IUser user)
        {
            _{InstanceName} = {InstanceName};
            _user = user;
        }

        // GET: api/{ClassName}s
        [HttpGet]
        public IActionResult Get{ClassName}(int page = 1, int rowPerPage = 50, string keyword = """", string orderBy = """", string orderType = """")
        {
            var data = _{InstanceName}.SearchAllFileds(keyword);
            var dataReturn =   _{InstanceName}.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/{ClassName}s/5
        [HttpGet(""{id}"")]
        public async Task<IActionResult> Get{ClassName}([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var {InstanceName} = await _{InstanceName}.FindAsync(id);

                if ({InstanceName} == null)
                {
                    return NotFound();
                }
                return Ok({InstanceName});
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/{ClassName}s/5
        [HttpPut(""{id}"")]
        public async Task<IActionResult> Put{ClassName}([FromRoute] long id, [FromBody] {ClassName} {InstanceName})
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != {InstanceName}.Id)
            {
                return BadRequest();
            }
            try
            {
                {InstanceName}.UpdatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(""emailAddress""));
                await _{InstanceName}.EditAsync({InstanceName});
                return CreatedAtAction(""Get{ClassName}"", new { id = {InstanceName}.Id }, {InstanceName});
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!{ClassName}Exists(id))
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

                  throw new UnexpectedException({InstanceName},e);
            }
        }

        // POST: api/{ClassName}s
        [HttpPost]
        public async Task<IActionResult> Post{ClassName}([FromBody] {ClassName} {InstanceName})
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                {InstanceName}.CreatedBy = JwtHelper.GetCurrentInformation(User, e => e.Type.Equals(""emailAddress""));
                await _{InstanceName}.AddAsync({InstanceName});
                return CreatedAtAction(""Get{ClassName}"", new { id = {InstanceName}.Id }, {InstanceName});
            }
            catch (Exception e)
            {

                throw new UnexpectedException({InstanceName},e);
            }
          
        }

        // DELETE: api/{ClassName}s/5
        [HttpDelete(""{id}"")]
        public async Task<IActionResult> Delete{ClassName}([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var {InstanceName} = await _{InstanceName}.FindAsync(id);
                if ({InstanceName} == null)
                {
                    return NotFound();
                }

                await _{InstanceName}.DeleteAsync({InstanceName});

                return Ok({InstanceName});
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool {ClassName}Exists(long id)
        {
            return _{InstanceName}.Any<{ClassName}>(e => e.Id == id);
        }

        private IQueryable<{ClassName}> GetByCurrentSpaBranch(IQueryable<{ClassName}> data)
        {
            var currentSalonBranch = _user.Find(JwtHelper.GetIdFromToken(User.Claims)).SalonBranchCurrentId;

            if (currentSalonBranch != default || currentSalonBranch != 0)
            {
                data = data.Where(e => e.SalonBranchId == currentSalonBranch);
            }
            return data;
        }
        private IQueryable<{ClassName}> GetByCurrentSalon(IQueryable<{ClassName}> data)
        {
            data = data.Where(e => e.SalonId == JwtHelper.GetCurrentInformationLong(User, x => x.Type.Equals(CLAIMUSER.SALONID)));
            return data;
        }
}
}

"
            ;
    }
}
