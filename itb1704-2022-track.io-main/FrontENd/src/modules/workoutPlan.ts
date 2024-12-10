export interface WorkoutPlan {
    id: number;
    name?: string;
    description?: string;
    exercisePlanIntensity?: string;
    ageCategory?: string;
    selected: boolean;
    totalCals: number;
  }
  
  export interface State {
    nutritions: WorkoutPlan[];
  }