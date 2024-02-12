using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZadachiApi.DB;

namespace ZadachiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZadachiController : Controller
    {

        private User07Context _user07Context;

        public ZadachiController(User07Context context)
        {
            _user07Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zadachi>>> GetZadachis()
        {
            if (_user07Context.Zadachis == null)
            {
                return NotFound();
            }
            return await _user07Context.Zadachis.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Zadachi>> GetZadachis(int id)
        {
            if (_user07Context.Zadachis == null)
            {
                return NotFound();
            }
            var zadachi = await _user07Context.Zadachis.FindAsync(id);
            if (zadachi == null)
            {
                return NotFound(zadachi);

            }
            return zadachi;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutZadachi(int id, Zadachi zadachi)
        {
            if (id != zadachi.Idzadachi)
            {
                return BadRequest();
            }
            _user07Context.Entry(zadachi).State = EntityState.Modified;
            try
            {
                await _user07Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Status>> PostZadachi(int id, Zadachi zadachi)
        {
            if (_user07Context.Zadachis == null)
            {
                return NotFound();
            }
            _user07Context.Zadachis.Add(zadachi);
            try
            {
                await _user07Context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StatusExists(zadachi.Idzadachi))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetStatus", new { id = zadachi.Idzadachi }, zadachi);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZadachi(int id)
        {
            if (_user07Context.Zadachis == null)
            {
                return NotFound();
            }
            var zadachi = await _user07Context.Zadachis.FindAsync(id);
            if (zadachi == null)
            {
                return NotFound(zadachi);

            }
            _user07Context.Zadachis.Remove(zadachi);
            await _user07Context.SaveChangesAsync();
            return NoContent();
        }
        private bool StatusExists(int id)
        {
            return (_user07Context.Zadachis?.Any(e => e.Idzadachi == id)).GetValueOrDefault();
        }



    }
}
