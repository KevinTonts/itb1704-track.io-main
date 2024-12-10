using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Test.itb1704.BackEnd.Model;

namespace BackEnd.Model
{
    public class WorkoutPlanExercise
    {
        public int ExerciseId { get; set; }

        public int WorkoutPlanId { get; set; }

        [JsonIgnore]
        public virtual Exercise? Exercise { get; set; }

        [JsonIgnore]
        public virtual WorkoutPlan? WorkoutPlan { get; set; }
    }
}