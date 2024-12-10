
import { MealPlanNutrition } from "./mealPlanNutrition";

export interface MealPlan {
    id: number;
    name?: string;
    lenghtInDays: number;
    mealPlanNutritions: MealPlanNutrition;
    selected: boolean;
    totalCals: number;
    calsInDay:number;
  }