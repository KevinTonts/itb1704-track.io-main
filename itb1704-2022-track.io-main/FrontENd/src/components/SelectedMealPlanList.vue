<!-- Oleks vaja, et iga päeva kohta kuvatakse eraldi alam link, millele vajutades avaneb päevane kava -->
<template>
  <div id="body">
    <div id="app">
      <div class="bigbox py-12 px-4 sm:px-6">
        <div class="text-center">
          <div v-if="showError">
            <div class="h2">Söögikava pole valitud</div>
            <br />
            <div class="text-right">
              <router-link to="/mealplan">
                <button class="button">Vali söögikava siit</button>
              </router-link>
            </div>
          </div>
          <div v-else>
            <h1 class="font-bold">{{ title }}</h1>

            <ul>
              <button
                v-for="days in WeekDay"
                v-on:click="filterWeekday(days)"
                type="button"
                class="button"
              >
                {{ days }}
              </button>
            </ul>

            <DataTable :value="nutritionsByWeekday">
              <Column field="description" header="Kirjeldus" />
              <Column field="amountInGrams" header="Kogus(g)" />
              <Column field="carbohydrates" header="Süsivesikud" />
              <Column field="fats" header="Rasvad" />
              <Column field="proteins" header="Proteiin" />
              <Column field="totalCalories" header="Kokku kalorid" />
              <Column field="mealTime" header="Söögikord" />
              <Column>
                <template #body="{ data }">
                <input
                    id="check"
                    v-if="data.nutritionFinished == true"
                    type="checkbox"
                    @click="untickNutrition(data)"
                    checked
                  />
                  <input
                    id="check"
                    v-if="data.nutritionFinished == false"
                    type="checkbox"
                    @click="tickNutrition(data)"
                  />
                </template>
              </Column>
          <Column>
            <template #body="{ data }">
              <button class="deleteButton" @click="remove(data)">
                <i class="fa fa-trash-o"></i>
              </button>
            </template>
          </Column>
            </DataTable>
          </div>
        </div>

        <div class="text-right">
          <router-link to="/">
            <button class="button">Tagasi</button>
          </router-link>
          <router-link to="/newnutrition">
            <button class="button">Lisa Söögikord</button>
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { MealPlan } from '@/modules/mealplan';
import { Nutrition, WeekDay } from '@/modules/nutrition';
import { useMealPlansStore } from '@/stores/mealPlansStore';
import { useNutritionsStore } from '@/stores/nutritionsStore';
import { storeToRefs } from 'pinia';
import { onMounted, ref } from 'vue';

defineProps<{ title: string }>();

const mealPlansStore = useMealPlansStore();
const nutritionsStore = useNutritionsStore();
const { nutritionsByWeekday } = storeToRefs(mealPlansStore);

let showError = ref(false);

const getPageStatus = async () => {
  showError.value = await mealPlansStore.getStatus();
};

const filterWeekday = (weekDay: WeekDay) => {
  mealPlansStore.filterSelectedMealPlanNutritionsByWeekday(weekDay);

   var inputs = <HTMLInputElement>document.getElementById('check');
   
  nutritionsByWeekday.value.forEach(element =>{
      if(element.nutritionFinished==true){
        inputs.checked=true;
      }
      if(element.nutritionFinished==false){
        inputs.checked=false;
      }

    });
};

const tickNutrition = (nutrition: Nutrition) => {
  nutritionsStore.finishNutrition(nutrition);
};

const untickNutrition = (nutrition: Nutrition) => {
  nutritionsStore.unFinishNutrition(nutrition);
};

const remove = (nutrition: Nutrition) => {
  nutritionsStore.deleteNutrition(nutrition);
  location.reload();
};

onMounted(() => {
  getPageStatus();
  mealPlansStore.loadSelected();
});
</script>
