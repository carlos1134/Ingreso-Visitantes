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

    public class VisitantesController : ControllerBase
    {
        private readonly VisitantesDbContext _context;

        public VisitantesController(VisitantesDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VisitantesModel>> GetVisitantes()
        {
            return  _context.VisitantesModelo.ToList();
        }
        [HttpGet("{dni}")]
        public ActionResult<VisitantesModel> GetVisitantes(int dni)
        {
            var visitante = _context.VisitantesModelo.FirstOrDefault(c => c.Dni == dni);
            if (visitante == null)
            {
                return new EmptyResult();
            }
            return visitante;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisitantes(int id, VisitantesModel visitantesModel)
        {
            if (id != visitantesModel.Id)
            {
                return BadRequest();
            }
            _context.Entry(visitantesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitanteExist(id))
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
        public async Task<ActionResult<VisitantesModel>> PostVisitantes(VisitantesModel visitantes)
        {
            _context.VisitantesModelo.Add(visitantes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVisitantes", new { id = visitantes.Id }, visitantes);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<VisitantesModel>> DeleteVisitantes(int id)
        {
            var visitante = await _context.VisitantesModelo.FindAsync(id);
            if (visitante == null)
            {
                return NotFound();
            }
            _context.VisitantesModelo.Remove(visitante);
            await _context.SaveChangesAsync();
            return visitante;
        }
        private bool VisitanteExist(int id)
        {
            return _context.VisitantesModelo.Any(e => e.Id == id);
        }
    }
}
