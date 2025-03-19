using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeporteGest.Models;
using DeporteGest.DTOs;

namespace DeporteGest.Controllers
{
    public class InscripcioneController : Controller
    {
        private readonly SportContext _context;

        public InscripcioneController(SportContext context)
        {
            _context = context;
        }

        // GET: Inscripcione
        public async Task<IActionResult> Index()
        {
            var sportContext = _context.Inscripciones.Include(i => i.Evento).Include(i => i.Participante);

            string username = HttpContext.Session.GetString("Username") ?? string.Empty;
            ViewData["Username"] = username;  // Pasar el valor de la sesión a la vista

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Users");
            }
            return View(await sportContext.ToListAsync());
        }

        // GET: Inscripcione/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcione = await _context.Inscripciones
                .Include(i => i.Evento)
                .Include(i => i.Participante)
                .FirstOrDefaultAsync(m => m.InscripcionId == id);
            if (inscripcione == null)
            {
                return NotFound();
            }

            return View(inscripcione);
        }

        // GET: Inscripcione/Create
        public IActionResult Create()
        {
            ViewData["EventoId"] = new SelectList(_context.Eventos, "EventoId", "Nombre");
            ViewData["ParticipanteId"] = new SelectList(_context.Participantes, "ParticipanteId", "Nombre");
            return View();
        }



        // POST: Inscripcione/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InscripcionId,EventoId,ParticipanteId,FechaInscripcion")] Inscripcione inscripcione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inscripcione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventoId"] = new SelectList(_context.Eventos, "EventoId", "EventoId", inscripcione.EventoId);
            ViewData["ParticipanteId"] = new SelectList(_context.Participantes, "ParticipanteId", "ParticipanteId", inscripcione.ParticipanteId);
            return View(inscripcione);
        }

        // GET: Inscripcione/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcione = await _context.Inscripciones.FindAsync(id);
            if (inscripcione == null)
            {
                return NotFound();
            }
            ViewData["EventoId"] = new SelectList(_context.Eventos, "EventoId", "EventoId", inscripcione.EventoId);
            ViewData["ParticipanteId"] = new SelectList(_context.Participantes, "ParticipanteId", "ParticipanteId", inscripcione.ParticipanteId);
            return View(inscripcione);
        }

        // POST: Inscripcione/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InscripcionId,EventoId,ParticipanteId,FechaInscripcion")] Inscripcione inscripcione)
        {
            if (id != inscripcione.InscripcionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inscripcione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InscripcioneExists(inscripcione.InscripcionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventoId"] = new SelectList(_context.Eventos, "EventoId", "EventoId", inscripcione.EventoId);
            ViewData["ParticipanteId"] = new SelectList(_context.Participantes, "ParticipanteId", "ParticipanteId", inscripcione.ParticipanteId);
            return View(inscripcione);
        }

        // GET: Inscripcione/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcione = await _context.Inscripciones
                .Include(i => i.Evento)
                .Include(i => i.Participante)
                .FirstOrDefaultAsync(m => m.InscripcionId == id);
            if (inscripcione == null)
            {
                return NotFound();
            }

            return View(inscripcione);
        }

        // POST: Inscripcione/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inscripcione = await _context.Inscripciones.FindAsync(id);
            if (inscripcione != null)
            {
                _context.Inscripciones.Remove(inscripcione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InscripcioneExists(int id)
        {
            return _context.Inscripciones.Any(e => e.InscripcionId == id);
        }

        [HttpGet("api/inscripciones")]
        public async Task<ActionResult<IEnumerable<InscripcionDTO>>> GetInscripcionesJson()
        {
            var inscripciones = await _context.Inscripciones
                .Include(i => i.Evento)
                .Include(i => i.Participante)
                .Select(i => new InscripcionDTO
                {
                    InscripcionId = i.InscripcionId,
                    FechaInscripcion = i.FechaInscripcion,

                    // Datos del Evento
                    EventoId = i.Evento != null ? i.Evento.EventoId : 0,
                    NombreEvento = i.Evento != null ? i.Evento.Nombre : "Desconocido",
                    FechaEvento = i.Evento != null ? i.Evento.Fecha : DateOnly.MinValue,
                    UbicacionEvento = i.Evento != null ? i.Evento.Ubicacion : "Desconocido",

                    // Datos del Participante
                    ParticipanteId = i.Participante != null ? i.Participante.ParticipanteId : 0,
                    NombreParticipante = i.Participante != null ? i.Participante.Nombre : "Desconocido",
                    ApellidoParticipante = i.Participante != null ? i.Participante.Apellido : "Desconocido",
                    EmailParticipante = i.Participante != null ? i.Participante.Email : "sin-email@example.com"
                })
                .ToListAsync();

            return Ok(inscripciones);
        }

    }


}
