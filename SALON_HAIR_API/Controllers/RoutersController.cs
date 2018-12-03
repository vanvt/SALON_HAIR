
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
    public class RoutersController : CustomControllerBase
    {
        private readonly IRouter _router;
        private readonly IUser _user;
        public RoutersController(IRouter router, IUser user)
        {
            _router = router;
            _user = user;
        }

        // GET: api/Routers
        [HttpGet]
        public IActionResult GetRouter(int page = 1, int rowPerPage = 50, string keyword = "", string orderBy = "", string orderType = "")
        {
            var data = _router.SearchAllFileds(keyword);              
            var dataReturn =   _router.LoadAllInclude(data);
            return OkList(dataReturn);
        }
        // GET: api/Routers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRouter([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var router = await _router.FindAsync(id);

                if (router == null)
                {
                    return NotFound();
                }
                return Ok(router);
            }
            catch (Exception e)
            {

                  throw new UnexpectedException(id, e);
            }
        }

        // PUT: api/Routers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRouter([FromRoute] long id, [FromBody] Router router)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != router.Id)
            {
                return BadRequest();
            }
            try
            {
              
                await _router.EditAsync(router);
                return CreatedAtAction("GetRouter", new { id = router.Id }, router);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!RouterExists(id))
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

                  throw new UnexpectedException(router,e);
            }
        }

        // POST: api/Routers
        [HttpPost]
        public async Task<IActionResult> PostRouter([FromBody] Router router)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
              
                await _router.AddAsync(router);
                return CreatedAtAction("GetRouter", new { id = router.Id }, router);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(router,e);
            }
          
        }

        // DELETE: api/Routers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRouter([FromRoute] long id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var router = await _router.FindAsync(id);
                if (router == null)
                {
                    return NotFound();
                }

                await _router.DeleteAsync(router);

                return Ok(router);
            }
            catch (Exception e)
            {

                throw new UnexpectedException(id,e);
            }
          
        }

        private bool RouterExists(long id)
        {
            return _router.Any<Router>(e => e.Id == id);
        }
    }
}

