<template>
  <div id="body">
    <div class="bigbox py-12 px-4 sm:px-6">
      <div class="text-center">
        <h1 class="font-bold">{{ title }}</h1>
        <DataTable :value="exercises">
          <Column field="title" header="Nimi" />
          <Column field="description" header="Kirjeldus" />
          <Column field="date" header="Kuupaev">
            <template #body="{ data }">{{
              new Date(data.date).toLocaleTimeString([], {
                year: 'numeric',
                month: 'numeric',
                day: 'numeric',
                hour: '2-digit',
                minute: '2-digit',
              })
            }}</template>
          </Column>
          <Column field="burnedCalories" header="Poletatud kalorid" />

          <Column>
            <template #body="{ data }">
              <button class="deleteButton" @click="remove(data)">
                <i class="fa fa-trash-o"></i>
              </button>
            </template>
          </Column>
        </DataTable>
      </div>
      <div class="text-right">
        <router-link to="/newexercise" class="button"
          >Lisa uus harjutus</router-link
        >
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { Exercise } from '@/modules/exercise';
import { useExercisesStore } from '@/stores/exercisesStore';
import { storeToRefs } from 'pinia';
import { onMounted } from 'vue';

defineProps<{ title: string }>();

const exercisesStore = useExercisesStore();
const { exercises } = storeToRefs(exercisesStore);

onMounted(() => {
  exercisesStore.load();
});
const remove = (exercise: Exercise) => {
  exercisesStore.deleteExercise(exercise);
  location.reload();
};
</script>

<style></style>
