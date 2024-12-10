import useApi from '@/modules/api';
import { defineStore } from 'pinia';
import { ref } from 'vue';
import { History } from '@/modules/history';
import { Exercise } from '@/modules/exercise';
import { Nutrition } from '@/modules/nutrition';

export const useHistoryStore = defineStore('historyStore', () => {
  const apiGetHistory = useApi<History[]>('history');
  let histories = ref<History[]>([]);
  let exercises = ref<Exercise[]>([]);
  let nutritions = ref<Nutrition[]>([]);
  let historyInfo = ref<History[]>([])

  const loadHistory = async () => {
    await apiGetHistory.request();

    if (apiGetHistory.response.value) {
      return apiGetHistory.response.value!;
    }

    return [];
  };

  const load = async () => {
    histories.value = await loadHistory();
    console.log(histories.value);
  };

  const addHistory = async (history: History) => {
    const apiAddHistory = useApi<History>('history', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(history),
    });

    await apiAddHistory.request();
    if (apiAddHistory.response.value) {
      histories.value.push(apiAddHistory.response.value!);
    }
  };

  const loadExerciseHistory = async (history:History) => {
    const apiGetExecriseHistory = useApi<History>(`history/${history.id}/exercises`)
    await apiGetExecriseHistory.request();

    if (apiGetExecriseHistory.response.value) {
      return apiGetExecriseHistory.response.value!;
    }

    return[];
  } //saab ainult exercised

  const loadNutritionHistory = async (history:History) => {
    const apiGetNutritionHistory = useApi<History>(`history/${history.id}/nutritions`)
    await apiGetNutritionHistory.request();

    if (apiGetNutritionHistory.response.value) {
      return apiGetNutritionHistory.response.value!;
    }

    return[];
  } //saab ainult nutritionid

  const GetHistoryInfo =async (history:History) => {
    const apiGetHistoryInfo = useApi<History>(`history/${history.id}/exercises/nutritions`)
    await apiGetHistoryInfo.request();

    if(apiGetHistoryInfo.response.value){
      console.log(apiGetHistoryInfo.response.value);
      return apiGetHistory.response.value!;
    }

    return [];
  } //saab molemad aga ei tea kuidas toole panna

  // const loadHistoryInfo =async (history:History) => {
  //   exercises.value  = await loadExerciseHistory(history); //naitab viga aga tootab
  //   nutritions.value  = await loadNutritionHistory(history);
  // }

  return { histories, load, addHistory, historyInfo, exercises, nutritions };
});