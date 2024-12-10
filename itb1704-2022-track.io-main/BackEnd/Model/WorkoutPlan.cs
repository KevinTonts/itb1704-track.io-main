using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    public class WorkoutPlan
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public Intensity ExercisePlanIntensity { get; set; }

        public Category AgeCategory { get; set; }

        public bool Selected { get; set; }

        public int TotalCals { get; set; }

        [JsonIgnore]
        public virtual ICollection<WorkoutPlanExercise> WorkoutPlanExercises { get; init; } = new List<WorkoutPlanExercise>();
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Intensity
    {
        Algajatele = 1,
        Edasijõudnutele = 2,
        Karastunutele = 3,
        Proffidele = 4
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Category
    {
        Lastele = 1,
        Täiskasvanutele = 2,
        Seenioridele = 3

    }
}
