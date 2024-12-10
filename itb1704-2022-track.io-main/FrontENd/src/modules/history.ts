
import { ExerciseHistory } from "./exerciseHistory";

export interface History {
  id: number;
  date: Date;
  exerciseHistories: ExerciseHistory
}


export interface State {
  history: History[];
}