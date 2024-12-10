using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Model
{
    [Table("nutrition")]
    public record Nutrition
    {
        [Column("id")]
        public int Id { get; init; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("amountInGrams")]
        public double? AmountInGrams { get; set; }

        [Column("carbohydrates")]
        public double? Carbohydrates { get; set; }

        [Column("fats")]
        public double? Fats { get; set; }

        [Column("proteins")]
        public double? Proteins { get; set; }

        [Column("totalCalories")]
        public int TotalCalories { get; set; }

        [Column("date")]
        public string? Date { get; set; }
        [Column("mealTime")]
        public MealTime MealTime { get; init; }
        [Column("weekDay")]
        public WeekDay? WeekDay { get; init; }
        [Column("nutritionFinished")]
        public bool NutritionFinished { get; set; }

        [Column("mealPlanNutritions")]
        [JsonIgnore]
        public virtual ICollection<MealPlanNutrition> MealPlanNutritions { get; init; } = new List<MealPlanNutrition>();

    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MealTime
    {
        Hommikusöök = 1,
        Lõunasöök = 2,
        Õhtusöök = 3,
        Snack = 4,
        Muu = 5
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum WeekDay
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