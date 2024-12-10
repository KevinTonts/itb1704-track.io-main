using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.Model;

namespace BackEnd.Model
{
    [Table("exercise")]
    public record Exercise
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string? Title { get; init; }

        [Column("description")]
        public string? Description { get; init; }

        [Column("date")]
        public string? Date { get; init; }

        [Column("burnedCalories")]
        public int BurnedCalories { get; init; }

        [Column("exerciseFinished")]
        public bool ExerciseFinished { get; set; }

        [Column("weekDay")]
        public DayOfTheWeek? WeekDay { get; set; }
        [Column("organzation_id")]
        public int OrganizationId { get; set; }


        [JsonIgnore]
        public virtual ICollection<WorkoutPlanExercise> WorkoutPlanExercises { get; init; } = new List<WorkoutPlanExercise>();
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DayOfTheWeek
    {
        Esmaspäev = 1,
        Teisipäev = 2,
        Kolmapäev = 3,
        Neljapäev = 4,
        Reede = 5,
        Laupäev = 6,
        Pühapäev = 7
    }
}
