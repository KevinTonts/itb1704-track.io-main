using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Test.itb1704.BackEnd.Model;
using Microsoft.EntityFrameworkCore;
using BackEnd.Model;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealPlanController : ControllerBase
    {
        private readonly DataContext _context;

        public MealPlanController(DataContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public ActionResult GetMealPlan(int id)
        {
            var mealPlan = _context.MealPlans!.FirstOrDefault(x => x.Id == id);
            if (mealPlan == null)
            {
                return NotFound();
            }
            return Ok(mealPlan);
        }

        [HttpGet]
        public IActionResult GetMealPlans()
        {
            var mealPlans = _context.MealPlans!.ToList();

            foreach (var mealPlan in mealPlans)
            {
                var nutritions = GetMealPlanNutritionsNoHttp(mealPlan.Id);
                foreach (var nutrition in nutritions)
                {
                    mealPlan.TotalCals += nutrition.TotalCalories;
                }
                mealPlan.CalsInDay = mealPlan.TotalCals / 7;
            }

            return Ok(_context.MealPlans);
        }

        [HttpGet("{id}/nutritions")]
        public ActionResult<IEnumerable<Nutrition>> GetMealPlanNutritions(int id)
        {
            var mealplanNutritions = _context.MealPlans!
            .Include(x => x.MealPlanNutritions)
            .ThenInclude(x => x.Nutrition)
            .First(x => x.Id == id)
            .MealPlanNutritions
            .Select(x => x.Nutrition)
            .ToList();

            return Ok(mealplanNutritions);
        }

        [HttpPost("{id}/nutritions")]
        public ActionResult<MealPlanNutrition> AddNutritionToMealPlan(int id, [FromBody] MealPlanNutrition value)
        {
            var mealPlan = _context.MealPlans!.Include(x => x.MealPlanNutritions)
            .FirstOrDefault(x => x.Id == id);
            if (mealPlan == null)
            {
                return NotFound();
            }

            value.MealPlanId = id;

            mealPlan.MealPlanNutritions.Add(value);
            _context.SaveChanges();

            return value;
        }

        [HttpPut("{id}/selected")]
        public async Task<ActionResult> ChangeSelected(int id)
        {
            //get selected mealplan
            var mealPlan = _context.MealPlans!.Find(id);

            if (mealPlan != null)
            {
                mealPlan.Selected = !mealPlan.Selected;
            }
            else
            {
                return NotFound();
            }

            //get all other mealplans
            var otherMealplans = _context.MealPlans!.Where(x => x.Id != mealPlan.Id);
            foreach (var mealplan in otherMealplans)
            {
                mealplan.Selected = false;
            }
            await _context.SaveChangesAsync();
            return Ok(mealPlan);
        }

        [HttpGet("selected/nutritions")]
        public IActionResult GetSelectedMealPlanNutritions()
        {
            var selectedMealplans = _context.MealPlans!.Where(x => x.Selected == true);

            if (!selectedMealplans.Any())
            {
                return NotFound();
            }

            var selectedId = selectedMealplans.Select(x => x.Id).ToArray();
            var id = selectedId[0];

            var nutritions = GetMealPlanNutritionsNoHttp(id);
            return Ok(nutritions);
        }

        [HttpPost("{id}/nutrition")]
        public IActionResult AddExerciseToWorkoutPlan(int id, [FromBody] Nutrition nutrition)
        {
            var dbMealPlan = _context.MealPlans?.Find(id);
            var dbMealPlanNutrition = _context.MealPlanNutritions!;
            var plan = new MealPlanNutrition();


            if (dbMealPlan != null)
            {
                plan.NutritionId = nutrition.Id;
                plan.MealPlanId = id;
                plan.Nutrition = nutrition;

                dbMealPlanNutrition.Add(plan);
                _context.SaveChanges();


                return CreatedAtAction(nameof(GetMealPlan), new { Id = nutrition.Id }, nutrition);
            }
            else
            {
                return Conflict();
            }
        }

        [HttpGet("selected")]
        public ActionResult GetSelectedMealPlanId()
        {

            var selectedPlan = _context.MealPlans!.Where(x => x.Selected == true);

            if (!selectedPlan.Any())
            {
                return NotFound();
            }

            var selectedId = selectedPlan.Select(x => x.Id).ToArray();

            var id = selectedId[0];

            return Ok(id);
        }

        private List<Nutrition> GetMealPlanNutritionsNoHttp(int id)
        {
            var mealplanNutritions = _context.MealPlans!
            .Include(x => x.MealPlanNutritions)
            .ThenInclude(x => x.Nutrition)
            .First(x => x.Id == id)
            .MealPlanNutritions
            .Select(x => x.Nutrition)
            .ToList();


            return mealplanNutritions;
        }

        private bool MealplanExists(int id)
        {
            return _context.MealPlans!.Any(c => c.Id == id);
        }
    }
}