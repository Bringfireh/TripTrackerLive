using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripTracker.BackService.Data;
using TripTracker.BackService.Models;

namespace TripTracker.BackService.Controllers
{
    [Route("api/[controller]")]
    public class TripsController : Controller
    {
        public TripsController(TripContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        private TripContext _context;

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var trips = await _context.trips.AsNoTracking().ToListAsync();
            return Ok(trips);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Trip> Get(int id)
        {
            return _context.trips.Find(id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Trip value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.trips.Add(value);
            _context.SaveChanges();
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Trip value)
        {
            if (!_context.trips.Any(t => t.Id==id))
                return NotFound();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.trips.Update(value);
           await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var mytrips = _context.trips.Find(id);

            if (mytrips == null)
                return NotFound();
            _context.trips.Remove(mytrips);
            _context.SaveChanges();
            return NoContent();
        }
    }
}