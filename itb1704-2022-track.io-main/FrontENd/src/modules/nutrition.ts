
import { MealPlanNutrition } from "./mealPlanNutrition";

export interface Nutrition {
  id: number;
  description?: string;
  amountInGrams?: number;
  carbohydrates?: number;
  fats?: number;
  proteins?: number;
  totalCalories: number;
  date?: string;
  mealTime : MealTime;
  weekDay? : WeekDay;
  mealPlanNutrition?: MealPlanNutrition
  nutritionFinished: boolean;
}

export interface State {
  nutritions: Nutrition[];
}

export enum MealTime{
  Breakfast = "Hommikusöök",
  Lunch = "Lõuna",
  Dinner = "Õhtusöök",
  Snack = "Snack",
  Other = "Muu"
}

export enum WeekDay{
  Monday = "Esmaspäev",
  Tuesday = "Teisipäev",
  Wednesday = "Kolmapäev",
  Thursday = "Neljapäev",
  Friday = "Reede",
  Saturday = "Laupäev",
  Sunday = "Pühapäev"
}