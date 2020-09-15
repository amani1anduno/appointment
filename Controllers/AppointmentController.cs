using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using appointment.Models;
using appointment.Data;

namespace appointment.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly AppointmentContext _context;
        //constructor of the controller
        public AppointmentController(AppointmentContext context){
            _context =context;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Appointment
                                            orderby m.date
                                            select m.name;    
            var appointments = from m in _context.Appointment
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                appointments = appointments.Where(s => s.name.Contains(searchString));
            }
            var tla = new Appointlist();
            tla.appointments = await appointments.ToListAsync();
            return View(tla);
        }



            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .SingleOrDefaultAsync(m => m.id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }
            public IActionResult Create()
            {
                return View();
            }
            [HttpPost]
            [ValidateAntiForgeryToken]   
            public async Task<IActionResult> Create([Bind("id,name,email,date")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(appointment);
        }
            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.SingleOrDefaultAsync(m => m.id == id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,email,date")] Appointment appointment)
        {
            if (id != appointment.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(appointment);
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .SingleOrDefaultAsync(m => m.id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointment.SingleOrDefaultAsync(m => m.id == id);
            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
























        private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.id == id);
        }
    }
    
}