<template>
  <div id="body">
    <div class="bigbox py-12 px-4 sm:px-6">
      <div class="text-center">
        <h1 class="font-bold">{{ title }}</h1>
        <DataTable :value="workoutPlans">
          <Column field="name" header="Treeningkava nimi" />
          <Column field="description" header="Treeningkava kirjeldus" />
          <Column
            field="exercisePlanIntensity"
            header="Treeningkava intensiivsus"
          />
          <Column field="ageCategory" header="Vanusegrupp" />
          <Column field="totalCals" header="Nädalane kalorite põletamise hulk" />
          <Column>
            <template #body="{ data }">
              <button
                v-if="data.selected == true"
                class="button"
                @click="changeSelected(data)"
                style="float: right"
              >
                Eemalda treeningkava
              </button>
              <button
                v-if="data.selected == false"
                class="button"
                @click="changeSelected(data)"
                style="float: right"
              >
                Vali treeningkava
              </button>
              <button
                style="float: right"
                class="selectButton"
                @click="loadWorkoutPlanExercises(data)"
              >
                <i class="fa fa-caret-down"></i>
              </button>
            </template>
          </Column>
        </DataTable>

        <ul id="weekDays" class="hidden">
            <button
              v-for="days in DayOfTheWeek"
              v-on:click="filterWeekday(days)"
              type="button"
              class="button"
            >
              {{ days }}
            </button>
        </ul>

        <DataTable :value="exercisesByWeekday" id="exercises" class="hidden">
          <Column field="title" header="Harjutuse nimi" />
          <Column field="description" header="Kirjeldus" />
          <Column field="burnedCalories" header="Kalorid" />
          <Column field="weekDay" header="Nädalapäev" />
        </DataTable>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { Exercise, DayOfTheWeek } from '@/modules/exercise';
import { WorkoutPlan } from '@/modules/workoutPlan';
import { useExercisesStore } from '@/stores/exercisesStore';
import { useWorkoutPlansStore } from '@/stores/workoutPlansStore';
import { storeToRefs } from 'pinia';
import { onMounted } from 'vue';

defineProps<{ title: string }>();

const workoutPlansStore = useWorkoutPlansStore();
const exercisesStore = useExercisesStore();
const { workoutPlans, Exercises, planExercises, exercisesByWeekday } =
  storeToRefs(workoutPlansStore);

const changeSelected = (workout: WorkoutPlan) => {
  workoutPlansStore.changeSelected(workout);
  location.reload();
};

const loadWorkoutPlanExercises = (workoutPlan: WorkoutPlan) => {
  workoutPlansStore.load2(workoutPlan);
  document.getElementById('exercises')?.classList.toggle('hidden');
  document.getElementById('weekDays')?.classList.toggle('hidden');
};

const filterWeekday = (weekDay: DayOfTheWeek) => {
  workoutPlansStore.filterExercisesByWeekday(weekDay);
  //document.getElementById('nutritions')?.classList.toggle('hidden');
};

onMounted(() => {
  workoutPlansStore.load();
});
</script>

<style></style>
