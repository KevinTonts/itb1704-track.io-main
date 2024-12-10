<template>
  <div id="body">
    <div class="bigbox py-12 px-4 sm:px-6">
      <div class="text-center">
        <h2 class="h2">{{ title }}</h2>
        <div class="grid-container">
          <div id="calendar" class="mx-auto mt-15">
            <Datepicker
              v-model="date"
              inline
              textInput
              inlineWithInput
              autoApply
            ></Datepicker>
            <button class="button" @click="filterByDate()">filtreeri</button>
          </div>
          <div id="myDIV" class="">
            <br />
            <h3 class="h3">Toitumised</h3>
            <br />
            <DataTable :value="nutritionsByDate">
              <Column field="description" header="Kirjeldus"></Column>
              <Column field="amountInGrams" header="Kogus(g)" />
              <Column field="carbohydrates" header="Süsivesikud" />
              <Column field="fats" header="Rasvad" />
              <Column field="proteins" header="Proteiin" />
              <Column field="totalCalories" header="Kokku kalorid" />
              <Column field="mealTime" header="Söögikord" />
            </DataTable>
            <br />
            <h3 class="h3">Treeningud</h3>
            <br />
            <DataTable :value="exercisesByDate">
              <Column field="title" header="Nimi" />
              <Column field="description" header="Kirjeldus" />
              <Column field="burnedCalories" header="Poletatud kalorid" />
            </DataTable>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useHistoryStore } from '@/stores/historyStore';
import { storeToRefs } from 'pinia';
import { onMounted, Ref, ref } from 'vue';
import { useNutritionsStore } from '@/stores/nutritionsStore';
import Datepicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css';
import { useExercisesStore } from '@/stores/exercisesStore';

defineProps<{ title: string; filtered: boolean }>();

const nutritionStore = useNutritionsStore();
const { nutritionsByDate } = storeToRefs(nutritionStore);
const exercisesStore = useExercisesStore();
const { exercisesByDate } = storeToRefs(exercisesStore);

const filterByDate = async () => {
  var finalDate = format(document.querySelectorAll('input')[1].value);

  if (
    (await nutritionStore.filterNutritonByDate(finalDate)) ||
    (await exercisesStore.filterExercisesByDate(finalDate))
  ) {
    nutritionStore.filterNutritonByDate(finalDate);
    exercisesStore.filterExercisesByDate(finalDate);
  }
};

const format = (date: string) => {
  var getDate = date.substring(0, 10);
  var finalDate = getDate.replaceAll('/', '-');
  console.log(finalDate);
  return finalDate;
};

var date = '';

onMounted(() => {
  nutritionStore.loadFinishedNutritions();
  exercisesStore.loadFinishedExercises();
});
</script>

<style></style>
