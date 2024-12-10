
import { Exercise } from "./exercise";
import { History } from "./history";
import { Nutrition } from "./nutrition";

export interface ExerciseHistory {
    exerciseId: number;
    historyId: number;
    nutritionId: number
    exercise: Exercise;
    history: History;
    nutrition : Nutrition;
}