<template>
  <div id="body">
    <h1 class="text-center font-bold"></h1>
    <div class="bigbox py-12 px-4 sm:px-6">
      <div>
        <h2 class="text-center font-bold">Selle nädala tulemused</h2>
      </div>

      <div class="grid-container">
        <div class="grid-item">
          <div>Trennis põletatud kalorid</div>
          {{ totalCalsExercise }}
        </div>
        <div class="grid-item">
          <div>Tarbitud kalorid</div>
          {{ totalCalsNutrition }}
        </div>
        <div class="grid-item">
          <div>Tehtud treeningkorrad</div>
          {{}}/7
        </div>
      </div>

      <div>
        <router-link to="/selectedWorkoutplan">
          <button class="button">
            Käimas olev treeningkava<i class="fa fa-angle-right px-3"></i>
          </button>
        </router-link>
        <router-link to="/selectedMealplan">
          <button class="button">
            Käimas olev toidukava<i class="fa fa-angle-right px-3"></i>
          </button>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useExercisesStore } from '@/stores/exercisesStore';
import { useNutritionsStore } from '@/stores/nutritionsStore';
import { storeToRefs } from 'pinia';
import { onBeforeMount, onMounted, ref } from 'vue';

defineProps<{ title: string }>();

const exercisesStore = useExercisesStore();
const nutritionsStore = useNutritionsStore();
const { exercises } = storeToRefs(exercisesStore);

let totalCalsExercise = ref<Number>();
let totalCalsNutrition = ref<Number>();

const getTotalCalsExercise = async () => {
  totalCalsExercise.value = await exercisesStore.loadFinishedExerciseCals();
};

const getTotalCalsNutrition = async () => {
  totalCalsNutrition.value = await nutritionsStore.loadFinishedNutritionCals();
};

onBeforeMount(() => {
  getTotalCalsExercise();
  getTotalCalsNutrition();
});
</script>
