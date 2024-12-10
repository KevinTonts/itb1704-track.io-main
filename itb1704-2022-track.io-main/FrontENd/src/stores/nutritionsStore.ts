import useApi, {useApiRawRequest} from '@/modules/api';
import { defineStore } from 'pinia';
import { ref } from 'vue';
import { Nutrition, WeekDay } from '@/modules/nutrition';

export const useNutritionsStore = defineStore('nutritionsStore', () => {
  const apiGetNutritions = useApi<Nutrition[]>('nutrition');
  let allNutritions : Nutrition[];
  let nutritions = ref<Nutrition[]>([]);
  let nutritionsByWeekday=ref<Nutrition[]>([]);
  let nutritionsByDate=ref<Nutrition[]>([]);
  let finishedNutritions = ref<Nutrition[]>([]);
  let totalCals=0;

  const loadNutritions = async () => {
    await apiGetNutritions.request();

    if (apiGetNutritions.response.value) {
      return apiGetNutritions.response.value!;
    }

    return [];
  };

  const load = async () => {
    allNutritions = await loadNutritions();
    nutritions.value=allNutritions;
  };

  const addNutrition = async (nutrition: Nutrition) => {
    const apiAddNutrition = useApi<Nutrition>('nutrition', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(nutrition),
    });

    await apiAddNutrition.request();
    if (apiAddNutrition.response.value) {
      nutritions.value.push(apiAddNutrition.response.value!);
    }
  };
  
  const deleteNutrition = async (nutrition: Nutrition) => {
    const deleteNutritionRequest = useApiRawRequest(`nutrition/${nutrition.id}`, {
      method: 'DELETE',
    });

    const res = await deleteNutritionRequest();

    if (res.status === 204) {
      let id = nutritions.value.indexOf(nutrition);

      if (id !== -1) {
        nutritions.value.splice(id, 1);
      }

      id = nutritions.value.indexOf(nutrition);

      if (id !== -1) {
        nutritions.value.splice(id, 1);
      }
    }
  };

  const filterNutritionByWeekday = async (weekday: WeekDay) => {
    nutritionsByWeekday.value=allNutritions.filter( x => x.weekDay == weekday)
  }

  const finishNutrition = async (nutrition: Nutrition) =>{
    const apiFinishExercise=useApi<Nutrition>(`nutrition/${nutrition.id}/finished`,{
      method: 'PUT',
      headers:{
        Accept: 'application/json',
        'Content-Type': 'application/json',
      }
    });

    await apiFinishExercise.request();
    if(apiFinishExercise.response.value){
      nutritions.value.push(apiFinishExercise.response.value);
    }
  }

  const unFinishNutrition = async (nutrition: Nutrition) =>{
    const apiFinishExercise=useApi<Nutrition>(`nutrition/${nutrition.id}/unfinish`,{
      method: 'PUT',
      headers:{
        Accept: 'application/json',
        'Content-Type': 'application/json',
      }
    });
    await apiFinishExercise.request();
    if(apiFinishExercise.response.value){
      nutritions.value.push(apiFinishExercise.response.value);
    }
  }

  const loadFinishedNutritionCals = async() =>{
    totalCals = 0;
    finishedNutritions.value = await getFinishedNutritions();
    finishedNutritions.value.forEach(element => {
      totalCals += element.totalCalories
      console.log(totalCals)
    });

    console.log(finishedNutritions.value);
    console.log(totalCals);

    return totalCals;
  }

  
  const getFinishedNutritions = async() =>{
    const apiGetFinishedNutritions=useApi<Nutrition[]>('nutrition/finished');

    await apiGetFinishedNutritions.request();

    if(apiGetFinishedNutritions.response.value){
      return apiGetFinishedNutritions.response.value!;
    }
    return [];
  }

  const loadFinishedNutritions =async () => {
    allNutritions = await getFinishedNutritions();
    finishedNutritions.value = allNutritions;
    console.log(allNutritions.at(0)?.date?.toString());
  }

  const filterNutritonByDate =async (date:String) => {
    nutritionsByDate.value = allNutritions.filter((x) => x.date == date);
    if(nutritionsByDate.value.length != 0){
      return true;
    }
    else
    {
      return false;
    }
  }

  return {
    nutritions, 
    nutritionsByWeekday, 
    finishedNutritions,
    nutritionsByDate,
    load, 
    addNutrition, 
    deleteNutrition, 
    filterNutritionByWeekday,
    unFinishNutrition,
    finishNutrition,
    loadFinishedNutritionCals,
    getFinishedNutritions,
    loadFinishedNutritions,
    filterNutritonByDate };
});