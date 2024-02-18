using ApiRdv.Data;
using ApiRdv.Models;
using ApiRdv.Models.Rdvs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class RdvController : ControllerBase
{
    private readonly RdvDbContext _context;

    public RdvController(RdvDbContext context)
    {
        _context = context;
    }

    // GET: api/Rdv
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Rdv>>> GetRdvs()
    {
        return await _context.Rdvs.ToListAsync();
    }
  
    /// ////////////////////////////////////////////////////////////////////////////////////////////////
  
    [HttpGet("Praticien/{praticienId}")]
    public async Task<ActionResult<IEnumerable<Rdv>>> GetRdvsByPraticien(int praticienId)
    {
        var rdvs = await _context.Rdvs.Where(r => r.IdPraticien == praticienId).ToListAsync();

        if ( !rdvs.Any())
        {
            return NotFound();
        }

        return rdvs;
    }

    // GET: api/Rdv/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Rdv>> GetRdv(int id)
    {
        var rdv = await _context.Rdvs.FindAsync(id);

        if (rdv == null)
        {
            return NotFound();
        }

        return rdv;
    }

   

    // PUT: api/Rdv/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRdv(int id, Rdv rdv)
    {
        if (id != rdv.Id)
        {
            return BadRequest();
        }

        _context.Entry(rdv).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RdvExists(id))
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

    // DELETE: api/Rdv/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRdv(int id)
    {
        var rdv = await _context.Rdvs.FindAsync(id);
        if (rdv == null)
        {
            return NotFound();
        }

        _context.Rdvs.Remove(rdv);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    // POST: api/Rdv
    [HttpPost]
    public async Task<ActionResult<Rdv>> CreateRdv(Rdv rdv)
    {
        _context.Rdvs.Add(rdv);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRdv), new { id = rdv.Id }, rdv);
    }



    private bool RdvExists(int id)
    {
        return _context.Rdvs.Any(e => e.Id == id);
    }
}
