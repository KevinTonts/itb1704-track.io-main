using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Model
{
    [Table("history")]
    public class History
    {
        [Column("id")]
        public int Id { get; init; }

        // [Column("exerciseId")]
        // public int ExerciseId { get; init; }

        // [Column("nutritionId")]
        // public int NutritionId { get; init; }

        [Column("date")]
        public DateTime Date { get; init; }

        [Column("exerciseHistory")]
        [JsonIgnore]
        public virtual ICollection<ExerciseNutritionHistory> ExerciseNutritionHistories { get; set; } = new List<ExerciseNutritionHistory>();

        [Column("nutritionHistory")]
        [JsonIgnore]
        public virtual ICollection<NutritionHistroy> NutritionHistroies { get; init; } = new List<NutritionHistroy>(); //pole vaja vist
    }
}