<template>
  <div id="body">
    <div class="addForm">
      <div class="max-w-md w-full bigbox">
        <div>
          <label for="description">Toidukorra kirjeldus</label>
          <input
            id="description"
            name="description"
            v-model="nutrition.description"
            class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            placeholder="Kirjeldus"
          />
        </div>
        <div>
          <label for="amountInGrams">Kogus(g)</label>
          <input
            id="amountInGrams"
            name="amountInGrams"
            v-model="nutrition.amountInGrams"
            class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            placeholder="Kogus grammides"
          />
        </div>
        <div>
          <label for="carbohydrates">Carbs(g)</label>
          <input
            id="carbohydrates"
            name="carbohydrates"
            v-model="nutrition.carbohydrates"
            class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            placeholder="Carbs grammides"
          />
        </div>
        <div>
          <label for="fats">Rasvad(g)</label>
          <input
            id="fats"
            name="fats"
            v-model="nutrition.fats"
            class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            placeholder="Rasvad grammides"
          />
        </div>
        <div>
          <label for="proteins">Proteiin(g)</label>
          <input
            id="proteins"
            name="proteins"
            v-model="nutrition.proteins"
            class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            placeholder="Proteiin grammides"
          />
        </div>
        <div>
          <label for="totalCalories">Kalorite hulk(kcal)</label>
          <input
            id="totalCalories"
            name="totalCalories"
            v-model="nutrition.totalCalories"
            class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            placeholder="Kalorite hulk"
          />
        </div>
        <div>
          <label for="description">Päev</label>
          <br>
          <select name="day" v-model="nutrition.weekDay">
            <option v-for="day in WeekDay" v-bind:value="day">{{day}}</option>
        </select>
        </div>

        <div>
          <label for="description">Toidukord</label>
          <br>
          <select name="day" v-model="nutrition.mealTime">
            <option v-for="meal in MealTime" v-bind:value="meal">{{meal}}</option>
        </select>
        </div>
<!-- 
          <div>
            <label for="mealTime">Toidukord</label>
            <input
              id="mealTime"
              name="mealTime"
              v-model="nutrition.mealTime"
              class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
              placeholder="Toidukorra aeg"
            />
          </div> -->

        <div class="text-right">
          <router-link to="/nutrition">
            <button class="button">Tagasi</button>
          </router-link>

          <button class="button" @click="submitForm">Lisa Toidukord</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { MealTime, Nutrition, WeekDay } from '@/modules/nutrition';
import { useNutritionsStore } from '@/stores/nutritionsStore';
import { useMealPlansStore } from '@/stores/mealPlansStore';
import { ref, Ref } from 'vue';
import { useRouter } from 'vue-router';
import Datepicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css';

const nutrition: Ref<Nutrition> = ref({
  id: 0,
  description: '',
  amountInGrams: 0,
  carbohydrates: 0,
  fats: 0,
  proteins: 0,
  totalCalories: 0,
  date: '',
  mealTime: MealTime.Snack,
  nutritionFinished: true,
  weekDay: WeekDay.Monday,
});

const { addNutritionToSelectedMealPlan } = useMealPlansStore();
const { addNutrition } = useNutritionsStore();
const router = useRouter();

const submitForm = () => {
  addNutritionToSelectedMealPlan({ ...nutrition.value });

  nutrition.value.description = '';
  nutrition.value.amountInGrams = 0;
  nutrition.value.carbohydrates = 0;
  nutrition.value.fats = 0;
  nutrition.value.proteins = 0;
  nutrition.value.totalCalories = 0;
  nutrition.value.date = '';
  nutrition.value.mealTime = MealTime.Snack;
  nutrition.value.weekDay = WeekDay.Monday;

  router.push({ name: 'ValitudToiduKavad' });
};
</script>

<style></style>
