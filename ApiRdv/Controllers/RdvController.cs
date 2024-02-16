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

    //ction pour récupérer les rendez-vous d'un praticien en utilisant le calendrier.
    [HttpGet("Calendrier/{calendrierId}")]
    public async Task<ActionResult<IEnumerable<Rdv>>> GetRdvsByCalendrier(int calendrierId)
    {
        var rdvs = await _context.Rdvs.Where(r => r.CalendrierId == calendrierId).ToListAsync();

        if (!rdvs.Any())
        {
            return NotFound();
        }

        return rdvs;
    }

    [HttpPost]
    public async Task<ActionResult<Rdv>> CreateRdv(Rdv rdv)
    {
        // Vérifiez si le praticien a déjà un calendrier, sinon créez-en un
        var calendrier = await _context.Calendriers
            .FirstOrDefaultAsync(c => c.IdPraticien == rdv.IdPraticien);

        if (calendrier == null)
        {
            calendrier = new Calendrier { IdPraticien = rdv.IdPraticien };
            _context.Calendriers.Add(calendrier);
        }

        rdv.CalendrierId = calendrier.Id;

        _context.Rdvs.Add(rdv);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetRdv", new { id = rdv.Id }, rdv);
    }

    private bool RdvExists(int id)
    {
        return _context.Rdvs.Any(e => e.Id == id);
    }
}
