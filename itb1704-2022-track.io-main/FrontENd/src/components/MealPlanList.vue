<template>
  <div id="body">
    <div id="app">
      <div class="bigbox py-12 px-4 sm:px-6">
        <div class="text-center">
          <h1 class="font-bold">{{ title }}</h1>
          <DataTable :value="mealplans">
            <Column field="name" header="Toidukava nimi" />
            <Column field="lenghtInDays" header="Kava pikkus" />
            <Column field="totalCals" header="Calorite tarbitav hulk nädalas" />
            <Column field="calsInDay" header="Calorite tarbitav hulk keskmiselt päevas" />
            <Column>
              <template #body="{ data }">
                <button
                  v-if="data.selected == false"
                  class="button"
                  @click="changeSelect(data)"
                  style="float: right"
                >
                  Vali Toidukava
                </button>
                <button
                  v-if="data.selected == true"
                  class="button"
                  @click="changeSelect(data)"
                  style="float: right"
                >
                  Eemalda toidukava
                </button>
                <button
                  style="float: right"
                  class="selectButton"
                  @click="getMealPlan(data)"
                >
                  <i class="fa fa-caret-down"></i>
                </button>
              </template>
            </Column>
          </DataTable>

          <ul id="weekDays" class="hidden">
            <button
              v-for="days in WeekDay"
              v-on:click="filterWeekday(days)"
              type="button"
              class="button"
            >
              {{ days }}
            </button>
          </ul>

          <DataTable
            :value="nutritionsByWeekday"
            id="nutritions"
            class="hidden"
          >
            <Column field="description" header="Kirjeldus" />
            <Column field="amountInGrams" header="Kogus(g)" />
            <Column field="carbohydrates" header="Süsivesikud" />
            <Column field="fats" header="Rasvad" />
            <Column field="proteins" header="Proteiin" />
            <Column field="totalCalories" header="Kokku kalorid" />
            <Column field="date" header="Kuupäev" />
            <Column field="mealTime" header="Söögikord" />
          </DataTable>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { MealPlan } from '@/modules/mealplan';
import { WeekDay } from '@/modules/nutrition';
import { useMealPlansStore } from '@/stores/mealPlansStore';
import { useNutritionsStore } from '@/stores/nutritionsStore';
import { storeToRefs } from 'pinia';
import DataTable from 'primevue/datatable';
import { onMounted } from 'vue';

defineProps<{ title: string }>();

const mealPlansStore = useMealPlansStore();
const { mealplans, nutritionsByWeekday } = storeToRefs(mealPlansStore);

const getMealPlan = (mealplan: MealPlan) => {
  mealPlansStore.loadNutritions(mealplan);
  document.getElementById('weekDays')?.classList.toggle('hidden');
  document.getElementById('nutritions')?.classList.toggle('hidden');
};

const changeSelect = (mealplan: MealPlan) => {
  //mitte parim veel kuna vahetab mealplanide jarjekorda aga tootab
  mealPlansStore.changeSelected(mealplan);
  location.reload();
};

const filterWeekday = (weekDay: WeekDay) => {
  mealPlansStore.filterNutritionByWeekday(weekDay);
};

onMounted(() => {
  mealPlansStore.load();
});
</script>

<style></style>
