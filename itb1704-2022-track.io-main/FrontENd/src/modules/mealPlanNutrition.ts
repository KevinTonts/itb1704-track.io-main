
import { MealPlan } from "./mealplan";
import { Nutrition } from "./nutrition";

export interface MealPlanNutrition {
    mealPlanId: number;
    nutritionId: number;
    mealPlan: MealPlan;
    nutrition: Nutrition;
  }