<template>
  <div id="body">
    <div class="addForm">
      <div class="max-w-md w-full bigbox">
        <div>
          <label for="name">Nimi</label>
          <input
            id="name"
            name="name"
            v-model="exercise.title"
            class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            placeholder="Nimi"
          />
        </div>
        <div>
          <label for="description">Kirjeldus</label>
          <input
            id="description"
            name="description"
            v-model="exercise.description"
            class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            placeholder="Kirjeldus"
          />
        </div>
        <div>
          <label for="description">Trennis põletatud kalorid</label>
          <input
            id="description"
            name="description"
            v-model="exercise.burnedCalories"
            class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            placeholder="Trennis poletatud kalorid"
          />
        </div>

        <div>
          <label for="description">Päev</label>
          <br />
          <select name="day" v-model="exercise.weekDay">
            <option v-for="days in DayOfTheWeek" v-bind:value="days">
              {{ days }}
            </option>
          </select>
        </div>

        <div class="text-right">
          <router-link to="/selectedWorkoutplan">
            <button class="button">Tagasi</button>
          </router-link>

          <button class="button" @click="submitForm">Lisa harjutus</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { DayOfTheWeek, Exercise } from '@/modules/exercise';
import { useExercisesStore } from '@/stores/exercisesStore';
import { useWorkoutPlansStore } from '@/stores/workoutPlansStore';
import { ref, Ref } from 'vue';
import { useRouter } from 'vue-router';
import Datepicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css';

const exercise: Ref<Exercise> = ref({
  id: 0,
  title: '',
  description: '',
  date: '',
  burnedCalories: 0,
  exerciseFinished: false,
  weekDay: DayOfTheWeek.Monday,
});

const { addExerciseToSelectedWorkoutplan } = useWorkoutPlansStore();
const { addExercise } = useExercisesStore();
const router = useRouter();

const submitForm = () => {
  addExerciseToSelectedWorkoutplan({ ...exercise.value });

  exercise.value.title = '';
  exercise.value.description = '';
  exercise.value.date = '';
  exercise.value.burnedCalories = 100;
  exercise.value.exerciseFinished = false;
  exercise.value.weekDay = DayOfTheWeek.Monday;

  router.push({ name: 'ValitudTreeningkava' });
};
</script>

<style></style>
