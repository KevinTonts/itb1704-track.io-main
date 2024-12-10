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
    public class WorkoutPlanController : ControllerBase
    {
        private readonly DataContext _context;

        public WorkoutPlanController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetWorkoutPlans()
        {

            var workOutPlans = _context.WorkoutPlans!.ToList();

            foreach (var workOutPlan in workOutPlans)
            {
                var exercises = GetWorkoutPlanExerciseNoHttp(workOutPlan.Id);
                foreach (var exercise in exercises)
                {
                    workOutPlan.TotalCals += exercise.BurnedCalories;
                }
            }



            return Ok(_context.WorkoutPlans);
        }


        [HttpGet("{id}/exercises")]
        public ActionResult<IEnumerable<Exercise>> GetWorkoutPlanExercises([FromQuery] DayOfTheWeek? weekday, int id)
        {
            var workoutPlanExercises = GetWorkoutPlanExerciseNoHttp(id).AsQueryable();

            if (weekday.HasValue)
            {
                workoutPlanExercises = workoutPlanExercises.Where(x => x.WeekDay == weekday.Value);
            }

            if (workoutPlanExercises == null)
            {
                return NotFound();
            }
            return Ok(workoutPlanExercises);
        }

        [HttpGet("{id}")]
        public ActionResult GetWorkoutPlan(int id)
        {
            var workoutPlan = _context.WorkoutPlans!.FirstOrDefault(x => x.Id == id);
            if (workoutPlan == null)
            {
                return NotFound();
            }
            return Ok(workoutPlan);
        }

        [HttpDelete("{id}/exercises")]
        public ActionResult<Exercise> DeleteExerciseFromWorkoutPlan(int? id)
        {
            var exercise = _context.WorkoutPlans!.Include(x => x.WorkoutPlanExercises)
            .FirstOrDefault(x => x.Id == id);

            if (exercise == null)
            {
                return NotFound();
            }

            _context.WorkoutPlans!.Remove(exercise);
            _context.SaveChanges();

            return Ok(exercise);
        }

        [HttpPut("{id}/selected")]
        public async Task<ActionResult> ChangeSelected(int id)
        {
            var workoutPlan = _context.WorkoutPlans!.Find(id);

            if (workoutPlan != null)
            {
                workoutPlan.Selected = !workoutPlan.Selected;
            }
            else
            {
                return NotFound();
            }

            var remainingWorkoutPlans = _context.WorkoutPlans!.Where(x => x.Id != workoutPlan.Id);
            foreach (var workoutPlans in remainingWorkoutPlans)
            {
                workoutPlans.Selected = false;
            }
            await _context.SaveChangesAsync();

            return Ok(workoutPlan);
        }

        [HttpGet("selected/exercises")]
        public ActionResult GetSelectedWorkoutPlanExercises()
        {

            var selectedPlan = _context.WorkoutPlans!.Where(x => x.Selected == true);

            if (!selectedPlan.Any())
            {
                return NotFound();
            }

            var selectedId = selectedPlan.Select(x => x.Id).ToArray();

            var id = selectedId[0];

            return Ok(GetWorkoutPlanExerciseNoHttp(id));
        }

        [HttpGet("selected")]
        public ActionResult GetSelectedWorkoutPlanId()
        {

            var selectedPlan = _context.WorkoutPlans!.Where(x => x.Selected == true);

            if (!selectedPlan.Any())
            {
                return NotFound();
            }

            var selectedId = selectedPlan.Select(x => x.Id).ToArray();

            var id = selectedId[0];

            return Ok(id);
        }

        [HttpPost("{id}/exercise")]
        public IActionResult AddExerciseToWorkoutPlan(int id, [FromBody] Exercise exercise)
        {
            var dbWorkoutPlan = _context.WorkoutPlans?.Find(id);
            var dbWorkOutPlanExercises = _context.WorkoutPlanExercises!;
            var plan = new WorkoutPlanExercise();


            if (dbWorkoutPlan != null)
            {
                plan.ExerciseId = exercise.Id;
                plan.WorkoutPlanId = id;
                plan.Exercise = exercise;

                dbWorkOutPlanExercises.Add(plan);
                _context.SaveChanges();


                return CreatedAtAction(nameof(GetWorkoutPlan), new { Id = exercise.Id }, exercise);
            }
            else
            {
                return Conflict();
            }
        }


        private List<Exercise> GetWorkoutPlanExerciseNoHttp(int id)
        {
            var workoutPlanExercise = _context.WorkoutPlans!
            .Include(x => x.WorkoutPlanExercises)
            .ThenInclude(x => x.Exercise)
            .First(x => x.Id == id)
            .WorkoutPlanExercises
            .Select(x => x.Exercise)
            .ToList();

            return workoutPlanExercise;

        }
    }
}