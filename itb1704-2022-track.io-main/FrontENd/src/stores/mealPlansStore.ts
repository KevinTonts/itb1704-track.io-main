import useApi, { useApiRawRequest } from '@/modules/api';
import { defineStore } from 'pinia';
import { ref } from 'vue';
import { MealPlan } from '@/modules/mealplan';
import { Nutrition, WeekDay } from '@/modules/nutrition';

export const useMealPlansStore = defineStore('mealPlansStore', () => {
  let allMealPlans : MealPlan[] = [];
  let selectedNutritions: Nutrition[] = [];
  let allNutritions: Nutrition[] = [];
  let mealplans = ref<MealPlan[]>([]);
  let Nutritions = ref<Nutrition[]>([]);
  let selectedMealPlan = ref<MealPlan[]>([]);
  let nutritionsByWeekday=ref<Nutrition[]>([]);

  const loadMealPlans = async () => {
    const apiGetMealPlans = useApi<MealPlan[]>('mealplan');
    await apiGetMealPlans.request();

    if (apiGetMealPlans.response.value) {
      return apiGetMealPlans.response.value!;
    }

    return [];
  };

  const load = async () => {
    allMealPlans = await loadMealPlans();
    mealplans.value = allMealPlans;
  };

  const loadMealNutritions = async(mealplan: MealPlan) => {
    const apiGetMealNutritions = useApi<Nutrition[]>(`mealplan/${mealplan.id}/nutritions`);
      await apiGetMealNutritions.request();

      if(apiGetMealNutritions.response.value){
          return apiGetMealNutritions.response.value!;
      }

      return [];
  }

  const loadNutritions = async (mealplan : MealPlan) => {
    allNutritions = await loadMealNutritions(mealplan);
    Nutritions.value = allNutritions;
  };

  const addMealPlan = async (mealPlan: MealPlan) => {
    const apiAddMealPlan = useApi<MealPlan>('mealplan', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(mealPlan),
    });

    await apiAddMealPlan.request();
    if (apiAddMealPlan.response.value) {
      allMealPlans.push(apiAddMealPlan.response.value!);
      mealplans.value = allMealPlans;
    }
  };

  const changeSelected =async (mealplan:MealPlan) => {
    const apiChangeSelected = useApi<MealPlan>(`mealplan/${mealplan.id}/selected`, {
      method: 'PUT',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      });

      await apiChangeSelected.request();
      if(apiChangeSelected.response.value){
        let id = mealplans.value.indexOf(mealplan);
        allMealPlans.splice(id, 1, apiChangeSelected.response.value).sort((a,b) => (a.id > b.id) ? -1 : 1); //tuleb otsida sort mis tootab ka
        mealplans.value = allMealPlans;
        // proovin erinevaid voimalusi et peale valimist array kohti ei muudaks
      }
    };


  const getSelected = async () => {
    const apiGetSelected = useApi<Nutrition[]>('mealplan/selected/nutritions');
    await apiGetSelected.request();

    if(apiGetSelected.response.value){
      return apiGetSelected.response.value;
    }
    return [];
  }

  const getStatus = async () : Promise<boolean> => {
    const apiGetSelectedRaw = useApiRawRequest('mealplan/selected/nutritions', {
      method: 'GET',
    });

    const res = await apiGetSelectedRaw();
    if (res.status == 200) {
      return false;
    }
    return true;
  }

  const loadSelected = async () => {
    selectedNutritions = await getSelected();
  }

  //seda kasutan SelectWorkoutPlanList
  const filterSelectedMealPlanNutritionsByWeekday = async (weekday: WeekDay) => {
    nutritionsByWeekday.value=selectedNutritions.filter( (x) => 
    x.weekDay == weekday)

    return nutritionsByWeekday.value;
  }

  //seda kasutan MealPlanList. Siit jooksis mõte veits kokku
  const filterNutritionByWeekday = async (weekday: WeekDay) => {
    nutritionsByWeekday.value=allNutritions.filter( (x) => 
    x.weekDay == weekday)
  }

  const getSelectedMealPlanId = async() => {
    const apiGetSelectedMealPlan = useApi<Nutrition[]>('mealplan/selected')
    await apiGetSelectedMealPlan.request();

    if (apiGetSelectedMealPlan.response.value) {
      return apiGetSelectedMealPlan.response.value;
    }
    return [];
  }

  const addNutritionToSelectedMealPlan = async (exercise: Nutrition) => {
    var id = await getSelectedMealPlanId();
    const apiNutritionToSelectedMealPlan = useApi<Nutrition>(`mealplan/${id}/nutrition`, {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(exercise),
    });

    await apiNutritionToSelectedMealPlan.request();
    if (apiNutritionToSelectedMealPlan.response.value) {
      nutritionsByWeekday.value.push(apiNutritionToSelectedMealPlan.response.value!);
    }
  };

  return { 
    getStatus,
    filterSelectedMealPlanNutritionsByWeekday,
    load, 
    filterNutritionByWeekday,
    addMealPlan, 
    loadNutritions,  
    changeSelected, 
    loadSelected, 
    addNutritionToSelectedMealPlan,
    selectedMealPlan,
    selectedNutritions,
    mealplans, 
    nutritionsByWeekday
    };
});