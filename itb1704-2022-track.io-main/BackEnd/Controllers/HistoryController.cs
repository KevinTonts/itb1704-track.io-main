using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.itb1704.BackEnd.Model;


namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly DataContext _context;

        public HistoryController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetHistory()
        {
            return Ok(_context.HistoryList);
        }

        [HttpGet("{id}/exercises")]
        public IActionResult GetExerciseHistroy(int id)
        {
            var exHistory = _context.HistoryList!.Include(x => x.ExerciseNutritionHistories)
            .ThenInclude(x => x.Exercise)
            .First(x => x.Id == id)
            .ExerciseNutritionHistories
            .Select(x => x.Exercise)
            .ToList();

            return Ok(exHistory);
        }

        [HttpGet("{id}/nutritions")]
        public IActionResult GetNutritionHistroy(int id)
        {
            var nutHistrory = _context.HistoryList!.Include(x => x.NutritionHistroies)
            .ThenInclude(x => x.Nutrition)
            .First(x => x.Id == id)
            .NutritionHistroies
            .Select(x => x.Nutrition)
            .ToList();

            return Ok(nutHistrory);
        }

        [HttpGet("{id}/exercises/nutritions")]
        public IActionResult GetAllHistoryInfo(int id)
        {
            var history = _context.HistoryList!.Include(x => x.ExerciseNutritionHistories)
            .ThenInclude(x => x.Nutrition)
            .Include(x => x.ExerciseNutritionHistories)
            .ThenInclude(x => x.Exercise)
            .First(x => x.Id == id)
            .ExerciseNutritionHistories
            .Select(x => new
            {
                nutritions = x.Nutrition,
                exercises = x.Exercise
            })
            .ToList();

            return Ok(history); //sellega saab nii exercises kui ka nutritions katte aga ei tea kuidas fronti seda veel saada
        }
    }
}