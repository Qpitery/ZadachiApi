using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using ZadachiApi.DB;

namespace ZadachiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : Controller
    {

        private User07Context _user07Context;

        public StatusController(User07Context context)
        {
            _user07Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatuses()
        {
            if (_user07Context.Statuses == null)
            {
                return NotFound();
            }
            return await _user07Context.Statuses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatuses(int id)
        {
            if (_user07Context.Statuses == null)
            {
                return NotFound();
            }
            var status = await _user07Context.Statuses.FindAsync(id);
            if (status == null)
            {
                return NotFound(status);

            }
            return status;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutStatus(int id, Status status)
        {
            if (id != status.Idstatus)
            {
                return BadRequest();
            }
            _user07Context.Entry(status).State = EntityState.Modified;
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

        [HttpGet("Getstatus")]
        public async Task<ActionResult<Status>> PostStatus(Status status)
        {
            if (_user07Context.Statuses == null)
            {
                return NotFound();
            }
            _user07Context.Statuses.Add(status);
            try
            {
                await _user07Context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StatusExists(status.Idstatus))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetStatus", new { id = status.Idstatus }, status);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            if (_user07Context.Statuses == null)
            {
                return NotFound();
            }
            var status = await _user07Context.Statuses.FindAsync(id);
            if (status == null)
            {
                return NotFound(status);

            }
            _user07Context.Statuses.Remove(status);
            await _user07Context.SaveChangesAsync();
            return NoContent();
        }
        private bool StatusExists(int id)
        {
            return (_user07Context.Statuses?.Any(e => e.Idstatus == id)).GetValueOrDefault();
        }


    }
}
