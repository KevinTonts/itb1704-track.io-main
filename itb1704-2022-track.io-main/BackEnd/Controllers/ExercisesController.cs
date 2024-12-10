using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Test.itb1704.BackEnd.Model;
using static BackEnd.Model.Exercise;
using Microsoft.EntityFrameworkCore;
using BackEnd.Model;
using static BackEnd.Model.Nutrition;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace itb1704.BackEnd.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ExercisesController : ControllerBase
    {
        private readonly DataContext _context;

        public ExercisesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetExercises([FromQuery] string? title)
        {

            var exercises = _context.ExerciseList!.AsQueryable().Where(x => x.OrganizationId == GetOrganizationId());

            if (title != null)
            {
                exercises = exercises.Where(x => x.Title == title);
            }

            return Ok(exercises);
        }

        [HttpGet("{id}")]
        public IActionResult GetDetails(int? id)
        {
            var exercise = _context.ExerciseList?.FirstOrDefault(e => e.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return Ok(exercise);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Exercise exercise)
        {
            var dbExercise = _context.ExerciseList?.Find(exercise.Id);
            if (dbExercise == null)
            {
                exercise.OrganizationId = GetOrganizationId();
                exercise.ExerciseFinished = false;

                _context.Add(exercise);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetDetails), new { Id = exercise.Id }, exercise);
            }
            else
            {
                return Conflict();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int? id, [FromBody] Exercise exercise)
        {
            var dbExercise = _context.ExerciseList!.AsNoTracking().FirstOrDefault(x => x.Id == exercise.Id);
            if (id != exercise.Id || dbExercise == null)
            {
                return NotFound();
            }

            exercise.OrganizationId = GetOrganizationId();

            if (exercise.OrganizationId != dbExercise.OrganizationId)
            {
                return Unauthorized();
            }

            _context.Update(exercise);
            _context.SaveChanges();

            return Ok(exercise);
        }

        [HttpDelete("{id}")]
        public ActionResult<Exercise> DeleteExercise(int? id)
        {
            var exercise = _context.ExerciseList!.FirstOrDefault(e => e.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }


            _context.ExerciseList!.Remove(exercise);
            _context.SaveChanges();

            return exercise;
        }

        [HttpPut("{id}/finished")]
        public ActionResult FinishExercise(int? id)
        {
            var exercise = _context.ExerciseList!.Find(id);

            if (exercise != null)
            {
                exercise.ExerciseFinished = true;
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }


            return Ok(exercise);
        }

        [HttpPut("{id}/unfinish")]
        public ActionResult UnFinishExercise(int? id)
        {
            var exercise = _context.ExerciseList!.Find(id);

            if (exercise != null)
            {
                exercise.ExerciseFinished = false;
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }


            return Ok(exercise);
        }

        [HttpGet("finished")]
        public ActionResult GetFinishedExercises()
        {
            var finishedExercises = _context.ExerciseList!.Where(x => x.ExerciseFinished == true);

            if (finishedExercises == null)
            {
                return NotFound();
            }

            return Ok(finishedExercises);
        }

        private int GetOrganizationId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity.Claims.Count() > 0)
            {
                return int.Parse(identity!.FindFirst("organizationId")!.Value);
            }
            return 0;
        }

    }
}