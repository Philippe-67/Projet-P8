using ApiRdv.Data;
using ApiRdv.Models.Rdvs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    // POST: api/Rdv
    [HttpPost]
    public async Task<ActionResult<Rdv>> Create(Rdv rdv)
    {
        _context.Rdvs.Add(rdv);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRdv), new { id = rdv.Id }, rdv);
    }

    /// ////////////////////////////////////////////////////////////////////////////////////////////////

    //[HttpGet("Praticien/{praticienId}")]
    //public async Task<ActionResult<IEnumerable<Rdv>>> GetRdvsByPraticien(int praticienId)
    //{
    //    var rdvs = await _context.Rdvs.Where(r => r.Id == praticienId).ToListAsync();

    //    if ( !rdvs.Any())
    //    {
    //        return NotFound();
    //    }

    //    return rdvs;
    //}





    //// PUT: api/Rdv/5
    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdateRdv(int id, Rdv rdv)
    //{
    //    if (id != rdv.Id)
    //    {
    //        return BadRequest();
    //    }

    //    _context.Entry(rdv).State = EntityState.Modified;

    //    try
    //    {
    //        await _context.SaveChangesAsync();
    //    }
    //    catch (DbUpdateConcurrencyException)
    //    {
    //        if (!RdvExists(id))
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

    //// DELETE: api/Rdv/5
    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteRdv(int id)
    //{
    //    var rdv = await _context.Rdvs.FindAsync(id);
    //    if (rdv == null)
    //    {
    //        return NotFound();
    //    }

    //    _context.Rdvs.Remove(rdv);
    //    await _context.SaveChangesAsync();

    //    return NoContent();
    //}

    ////// POST: api/Rdv
    //[HttpPost("PrendreRendezVous")]
    //public async Task<IActionResult> PrendreRendezVous(int praticienId,string nomPraticien, DateTime date)
    //{
    //    try
    //    {
    //        // Vérifie si le praticien existe dans la base de données
    //        var praticienExiste = await _context.Rdvs.AnyAsync(r => r.Id == praticienId);

    //        if (!praticienExiste)
    //        {
    //            return NotFound($"Praticien avec l'ID {praticienId} non trouvé.");
    //        }
    //        // Crée un nouvel objet RendezVous avec le praticienId
    //        var rendezVous = new Rdv
    //        {
    //           // PraticienId = praticienId,
    //            NomPraticien = nomPraticien,
    //            // Ajoutez d'autres propriétés du rendez-vous si nécessaire
    //            NomPatient = "NomPatient", // Remplacez par la valeur réelle
    //            Date = DateTime.Now // Remplacez par la valeur réelle
    //        };

    //        // Ajoute le rendez-vous à la base de données
    //        _context.Rdvs.Add(rendezVous);
    //        _context.SaveChanges();

    //        // Renvoie un statut Ok si tout s'est bien déroulé
    //        return Ok("Rendez-vous pris avec succès");
    //    }
    //    catch (Exception ex)
    //    {
    //        // En cas d'échec de la prise de rendez-vous, renvoie un statut d'erreur
    //        return StatusCode(500, $"Erreur lors de la prise de rendez-vous : {ex.Message}");
    //    }
    //}



    private bool RdvExists(int id)
    {
        return _context.Rdvs.Any(e => e.Id == id);
    }
}
