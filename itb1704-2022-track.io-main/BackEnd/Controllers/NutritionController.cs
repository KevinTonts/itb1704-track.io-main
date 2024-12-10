using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Model;
using Microsoft.AspNetCore.Mvc;
using Test.itb1704.BackEnd.Model;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NutritionController : ControllerBase
    {
        private readonly DataContext _context;

        public NutritionController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetNutrition()
        {
            return Ok(_context.NutritionList);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Nutrition nutrition)
        {
            var dbNutrition = _context.NutritionList?.Find(nutrition.Id);
            if (dbNutrition == null)
            {
                _context.Add(nutrition);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetDetails), new { Id = nutrition.Id }, nutrition);
            }
            else
            {
                return Conflict();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetDetails(int? id)
        {
            var nutrition = _context.NutritionList?.FirstOrDefault(e => e.Id == id);
            if (nutrition == null)
            {
                return NotFound();
            }

            return Ok(nutrition);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNutrition(int? id, [FromBody] Nutrition nutrition)
        {
            if (id != nutrition.Id || !_context.NutritionList!.Any(e => e.Id == id))
            {
                return NotFound();
            }

            _context.Update(nutrition);
            _context.SaveChanges();

            return Ok(nutrition);

        }

        [HttpDelete("{id}")]
        public ActionResult<Nutrition> DeleteNutrition(int? id)
        {
            var nutrition = _context.NutritionList!.FirstOrDefault(e => e.Id == id);
            if (nutrition == null)
            {
                return NotFound();
            }

            _context.NutritionList!.Remove(nutrition);
            _context.SaveChanges();

            return nutrition;
        }

        [HttpPut("{id}/finished")]
        public ActionResult FinishNutrition(int id)
        {
            var nutrition = _context.NutritionList!.Find(id);

            if (nutrition != null)
            {
                nutrition.NutritionFinished = true;
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }


            return Ok(nutrition);
        }

        [HttpPut("{id}/unfinish")]
        public ActionResult UnFinishNutrition(int? id)
        {
            var nutrition = _context.NutritionList!.Find(id);

            if (nutrition != null)
            {
                nutrition.NutritionFinished = false;
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }


            return Ok(nutrition);
        }

        [HttpGet("finished")]
        public ActionResult GetFinishedNutritions(string? date)
        {
            var finishedNutritions = _context.NutritionList!.Where(x => x.NutritionFinished == true);

            if (finishedNutritions == null)
            {
                return NotFound();
            }
            if (date != null)
            {
                finishedNutritions = finishedNutritions.Where(x => x.Date == date);
            }

            return Ok(finishedNutritions);
        }
    }
}