using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    public class ExerciseNutritionHistory
    {
        public int HistoryId { get; set; }
        public int ExerciseId { get; set; }
        public int NutritionId { get; set; }



        [JsonIgnore]
        public virtual History? History { get; set; }
        [JsonIgnore]
        public virtual Exercise? Exercise { get; set; }
        [JsonIgnore]
        public virtual Nutrition? Nutrition { get; set; }

    }
}