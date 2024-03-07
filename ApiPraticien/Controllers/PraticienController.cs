using ApiPraticien.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiPraticien.Models;
using ApiPraticien.Models.Praticien;
using Microsoft.Extensions.Logging;

[Route("api/[controller]")]
[ApiController]
public class PraticienController : ControllerBase
{
    private readonly PraticienDbContext _context;
    private readonly ILogger<PraticienController> logger;

    public PraticienController(PraticienDbContext context, ILogger<PraticienController> logger)
    {
        _context = context;
        this.logger = logger;

    }

    // GET: api/Praticien
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Praticien>>> GetPraticiens()
    {
        //_logger.LogInformation("Fetching list of Praticiens");
        return await _context.Praticiens.ToListAsync();
        //_logger.LogInformation("Retrieved {count} praticiens", praticiens.Count);

    }

    //// GET: api/Praticien/5
    //[HttpGet("{id}")]
    //public async Task<ActionResult<Praticien>> GetPraticien(int id)
    //{
    //    var praticien = await _context.Praticiens.FindAsync(id);

    //    if (praticien == null)
    //    {
    //        return NotFound();
    //    }

    //    return praticien;
    //}

    //// POST: api/Praticien
    //[HttpPost]
    //public async Task<ActionResult<Praticien>> CreatePraticien(Praticien praticien)
    //{
    //    _context.Praticiens.Add(praticien);
    //    await _context.SaveChangesAsync();

    //    return CreatedAtAction("GetPraticien", new { id = praticien.Id }, praticien);
    //}

    //// PUT: api/Praticien/5
    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdatePraticien(int id, Praticien praticien)
    //{
    //    if (id != praticien.Id)
    //    {
    //        return BadRequest();
    //    }

    //    _context.Entry(praticien).State = EntityState.Modified;

    //    try
    //    {
    //        await _context.SaveChangesAsync();
    //    }
    //    catch (DbUpdateConcurrencyException)
    //    {
    //        if (!PraticienExists(id))
    //        {
    //            return NotFound();
    //        }
    //        else
    //        {
    //            throw;
    //        }
    //    }

    //    return NoContent();
    //}

    //// DELETE: api/Praticien/5
    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeletePraticien(int id)
    //{
    //    var praticien = await _context.Praticiens.FindAsync(id);
    //    if (praticien == null)
    //    {
    //        return NotFound();
    //    }

    //    _context.Praticiens.Remove(praticien);
    //    await _context.SaveChangesAsync();

    //    return NoContent();
    //}

    //private bool PraticienExists(int id)
    //{
    //    return _context.Praticiens.Any(e => e.Id == id);
    //}
}
