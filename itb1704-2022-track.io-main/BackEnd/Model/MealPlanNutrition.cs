using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    public class MealPlanNutrition
    {
        public int MealPlanId { get; set; }
        public int NutritionId { get; set; }

        [JsonIgnore]
        public virtual MealPlan? MealPlan { get; set; }
        [JsonIgnore]
        public virtual Nutrition? Nutrition { get; set; }
    }
}