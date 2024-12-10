import useApi, {useApiRawRequest} from '@/modules/api';
import { Exercise } from '@/modules/exercise';
import { WorkoutPlan } from '@/modules/workoutPlan';
import { defineStore } from 'pinia';
import { ref } from 'vue';
import { useAuthStore } from './authStore';

export const useExercisesStore = defineStore('exercisesStore', () => {
  // const apiGetExercises = useApi<Exercise[]>('exercises');
  const authStore = useAuthStore();
  let allExercises : Exercise[];
  let exercises = ref<Exercise[]>([]);
  let exercise = ref<Exercise[]>([]);
  let finishedExercises = ref<Exercise[]>([]);
  let exercisesByDate = ref<Exercise[]>([]);
  let totalCals=0;

  const loadExercises = async () => {
    const apiGetExercises = useApi<Exercise[]>('exercises', {
      headers: {Authorization: 'Bearer' + authStore.token},
    });
    await apiGetExercises.request();

    if (apiGetExercises.response.value) {
      return apiGetExercises.response.value!;
    }
    return [];
  };

  const load = async () => {
    allExercises = await loadExercises();
    exercises.value = allExercises
  };

  const loadExercise =async (id:number) => {
    var idString = id.toString();
    const apiGetExercise = useApi<Exercise[]>(`exercises/${idString}`)
    await apiGetExercise.request();

    if (apiGetExercise.response.value) {
      return apiGetExercise.response.value!;
    }
    return [];
  }

  const loadExerciseDetails =async (ex:Exercise) => { 
    exercise.value = await loadExercise(ex.id);
    console.log(exercise.value);
  }

  const addExercise = async (exercise: Exercise) => {
    const apiAddExercise = useApi<Exercise>('exercises', {
      method: 'POST',
      headers: {
        Authorization: 'Bearer ' + authStore.token,
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(exercise),
    });

    await apiAddExercise.request();
    if (apiAddExercise.response.value) {
      exercises.value.push(apiAddExercise.response.value!);
    }
  };
  const deleteExercise = async (exercise: Exercise) => {
    const deleteExerciseRequest = useApiRawRequest(`exercises/${exercise.id}`, {
      method: 'DELETE',
      headers: { Authorization: 'Bearer ' + authStore.token },
    });

    const res = await deleteExerciseRequest();

    if (res.status === 204) {
      let id = exercises.value.indexOf(exercise);

      if (id !== -1) {
        exercises.value.splice(id, 1);
      }

      id = exercises.value.indexOf(exercise);

      if (id !== -1) {
        exercises.value.splice(id, 1);
      }
    }
  };

  const finishExercise = async (exercise: Exercise) =>{
    const apiFinishExercise=useApi<Exercise>(`exercises/${exercise.id}/finished`,{
      method: 'PUT',
      headers:{
        Accept: 'application/json',
        'Content-Type': 'application/json',
      }
    });

    await apiFinishExercise.request();
    if(apiFinishExercise.response.value){
      exercises.value.push(apiFinishExercise.response.value);
    }
  }

  const unFinishExercise = async (exercise: Exercise) =>{
    const apiFinishExercise=useApi<Exercise>(`exercises/${exercise.id}/unfinish`,{
      method: 'PUT',
      headers:{
        Accept: 'application/json',
        'Content-Type': 'application/json',
      }
    });

    await apiFinishExercise.request();
    if(apiFinishExercise.response.value){
      exercises.value.push(apiFinishExercise.response.value);
    }
  }
// ------------------------------------------------------------ annab välja "promise void"
  const loadFinishedExerciseCals = async() =>{
    totalCals = 0;
    finishedExercises.value = await getFinishedExercises();
    finishedExercises.value.forEach(element => {
      totalCals += element.burnedCalories
      console.log(totalCals)
    });

    console.log(finishedExercises.value);
    console.log(totalCals);

    return totalCals;
  }

  
  const getFinishedExercises = async() =>{
    const apiGetFinishedExercises=useApi<Exercise[]>('exercises/finished');

    await apiGetFinishedExercises.request();

    if(apiGetFinishedExercises.response.value){
      return apiGetFinishedExercises.response.value!;
    }
    return [];
  }

  const loadFinishedExercises =async () => {
    allExercises = await getFinishedExercises();
    finishedExercises.value = allExercises;
    console.log(allExercises);
  }

  const filterExercisesByDate =async (date:String) => {
    exercisesByDate.value = allExercises.filter((x) => x.date == date);
    if(exercisesByDate.value.length != 0){
      return true;
    }
    else
    {
      return false;
    }
    
  }

  return { 
    exercise,
    exercises, 
    totalCals,
    exercisesByDate,
    finishedExercises,
    loadFinishedExerciseCals,
    getFinishedExercises,
    load, 
    addExercise, 
    loadExerciseDetails, 
    deleteExercise, 
    finishExercise, 
    unFinishExercise,
    filterExercisesByDate,
    loadFinishedExercises };
});
