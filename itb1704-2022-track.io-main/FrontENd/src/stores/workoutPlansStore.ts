import useApi, { useApiRawRequest } from '@/modules/api';
import { defineStore } from 'pinia';
import { ref } from 'vue';
import { WorkoutPlan } from '@/modules/workoutPlan';
import { DayOfTheWeek, Exercise } from '@/modules/exercise';
import { WeekDay } from '@/modules/nutrition';

export const useWorkoutPlansStore = defineStore('workoutPlansStore', () => {
  const apiGetWorkoutPlans = useApi<WorkoutPlan[]>('workoutPlan');
  let workoutPlans = ref<WorkoutPlan[]>([]);
  let Exercises= ref<Exercise[]>([]);
  let exercisesByWeekday= ref<Exercise[]>([]);
  let planExercises= ref<Exercise[]>([]);
  let selectedExercises=<Exercise[]>([]);
  let allWorkoutplans=<WorkoutPlan[]>([]);
  let allExercises=<Exercise[]>([]);

  

  const loadWorkoutPlans = async () => {
    await apiGetWorkoutPlans.request();

    if (apiGetWorkoutPlans.response.value) {
      return apiGetWorkoutPlans.response.value!;
    }

    return [];
  };

  const load = async () => {
    allWorkoutplans = await loadWorkoutPlans();
    workoutPlans.value=allWorkoutplans;
  };

  const addWorkoutPlan = async (workoutPlan: WorkoutPlan) => {
    const apiAddWorkoutPlan = useApi<WorkoutPlan>('workoutPlan', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(workoutPlan),
    });

    await apiAddWorkoutPlan.request();
    if (apiAddWorkoutPlan.response.value) {
      workoutPlans.value.push(apiAddWorkoutPlan.response.value!);
    }
  };

  const changeSelected = async(workoutPlan: WorkoutPlan) =>{
    const apiChangeSelectedWorkoutPlan = useApi<WorkoutPlan>(`workoutplan/${workoutPlan.id}/selected`,{
      method: 'PUT',
      headers:{
        Accept: 'application/json',
        'Content-Type': 'application/json',
      }
    });

    await apiChangeSelectedWorkoutPlan.request();
    if(apiChangeSelectedWorkoutPlan.response.value){
      let id=workoutPlans.value.indexOf(workoutPlan);
      allWorkoutplans.splice(id,1);
      workoutPlans.value=allWorkoutplans;
    }
  }

  const getSelectedWorkoutplanExercises = async(workoutplan: WorkoutPlan) => {
    const apiGetSelectedWorkoutplan = useApi<Exercise[]>(`workoutplan/${workoutplan.id}/exercises`)
    await apiGetSelectedWorkoutplan.request();

    if (apiGetSelectedWorkoutplan.response.value) {
      return apiGetSelectedWorkoutplan.response.value;
    }
    return [];
  }

  const load2 = async(workoutPlan: WorkoutPlan) =>{
    allExercises=await getSelectedWorkoutplanExercises(workoutPlan);
    planExercises.value=allExercises
  }

  const getSelectedWorkoutPlanExercises = async() => {
    const apiGetSelectedWorkoutPlanExercises = useApi<Exercise[]>('workoutplan/selected/exercises')
    await apiGetSelectedWorkoutPlanExercises.request();

    if (apiGetSelectedWorkoutPlanExercises.response.value) {
      return apiGetSelectedWorkoutPlanExercises.response.value;
    }
    return [];
  }

  const getSelectedWorkoutPlanId = async() => {
    const apiGetSelectedWorkoutplan = useApi<Exercise[]>('workoutplan/selected')
    await apiGetSelectedWorkoutplan.request();

    if (apiGetSelectedWorkoutplan.response.value) {
      return apiGetSelectedWorkoutplan.response.value;
    }
    return [];
  }

  const loadSelected = async () => {
    selectedExercises = await getSelectedWorkoutPlanExercises();
    Exercises.value=selectedExercises;
  }

  //seda kasutan SelectWorkoutPlanList
  const filterSelectedExercisesByWeekday = (exercise: DayOfTheWeek) => {
    exercisesByWeekday.value= selectedExercises.filter((x) =>
    x.weekDay == exercise);

    return exercisesByWeekday.value;
  }

    //seda kasutan WorkoutPlanList. Siit jooksis mõte veits kokku
  const filterExercisesByWeekday = async (weekday: DayOfTheWeek) => {
    exercisesByWeekday.value=allExercises.filter( (x) => 
    x.weekDay == weekday)
  }
  
  const getStatus = async () : Promise<boolean> => {
    const apiGetSelectedRaw = useApiRawRequest('workoutplan/selected', {
      method: 'GET',
    });

    const res = await apiGetSelectedRaw();
    if (res.status == 200) {
      return false;
    }
    return true;
  }

  const addExerciseToSelectedWorkoutplan = async (exercise: Exercise) => {
    var id = await getSelectedWorkoutPlanId();
    const apiExerciseToSelectedWorkoutplan = useApi<Exercise>(`workoutplan/${id}/exercise`, {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(exercise),
    });

    await apiExerciseToSelectedWorkoutplan.request();
    if (apiExerciseToSelectedWorkoutplan.response.value) {
      exercisesByWeekday.value.push(apiExerciseToSelectedWorkoutplan.response.value!);
    }
  };

  return { 
    load, 
    load2,
    loadSelected, 
    addWorkoutPlan, 
    changeSelected, 
    getSelectedWorkoutplan: getSelectedWorkoutPlanExercises,
    filterSelectedExercisesByWeekday,
    filterExercisesByWeekday,
    getStatus,
    addExerciseToSelectedWorkoutplan,
    Exercises,
    workoutPlans,
    exercisesByWeekday,
    planExercises};
});