using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitantesBack.Models;

namespace VisitantesBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly VisitantesDbContext _context;

        public PersonasController(VisitantesDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonasModel>>> GetVisitantes()
        {
            var persona = await _context.PersonasModelo.ToListAsync();
            return persona;
        }
        [HttpGet("{Sector}")]
        public ActionResult<IEnumerable<PersonasModel>> GetPersonas(string sector)
        {
            var persona = _context.PersonasModelo.Where(c => c.Sector == sector).Select(p => p).ToList();
            if (persona == null)
            {
                return NotFound();
            }
            return persona;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisitantes(int id, PersonasModel personasModel)
        {
            if (id != personasModel.Id)
            {
                return BadRequest();
            }
            _context.Entry(personasModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExist(id))
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
        public async Task<ActionResult<PersonasModel>> PostPersonas(PersonasModel visitantes)
        {
            _context.PersonasModelo.Add(visitantes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVisitantes", new { id = visitantes.Id }, visitantes);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonasModel>> DeletePersonas(int id)
        {
            var persona = await _context.PersonasModelo.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            _context.PersonasModelo.Remove(persona);
            await _context.SaveChangesAsync();
            return persona;
        }
        private bool PersonaExist(int id)
        {
            return _context.PersonasModelo.Any(e => e.Id == id);
        }
    }
}
