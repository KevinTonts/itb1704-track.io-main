using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    [Table("mealplan")]
    public class MealPlan
    {
        [Column("id")]
        public int Id { get; init; }
        [Column("name")]
        public string? Name { get; init; }
        [Column("lenghtInDays")]
        public int LenghtInDays { get; init; }
        [Column("selected")]
        public bool Selected { get; set; }
        [Column("totalCals")]
        public int TotalCals { get; set; }
        [Column("calsInDay")]
        public int CalsInDay { get; set; }

        [Column("mealPlanNutritions")]
        [JsonIgnore]
        public virtual ICollection<MealPlanNutrition> MealPlanNutritions { get; init; } = new List<MealPlanNutrition>();
    }
}