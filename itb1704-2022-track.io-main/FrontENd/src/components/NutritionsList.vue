<template>
  <div id="body">
    <div class="bigbox py-12 px-4 sm:px-6">
      <div class="text-center">
        <h1 class="font-bold">{{ title }}</h1>
        <DataTable :value="nutritions">
          <Column field="description" header="Kirjeldus" />
          <Column field="amountInGrams" header="Kogus(g)" />
          <Column field="carbohydrates" header="Süsivesikud" />
          <Column field="fats" header="Rasvad" />
          <Column field="proteins" header="Proteiin" />
          <Column field="totalCalories" header="Kokku kalorid" />
          <Column field="mealTime" header="Söögikord" />

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
        <router-link to="/newnutrition" class="button"
          >Lisa uus toidukord</router-link
        >
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { Nutrition } from '@/modules/nutrition';
import { useNutritionsStore } from '@/stores/nutritionsStore';
import { storeToRefs } from 'pinia';
import { onMounted } from 'vue';

defineProps<{ title: string }>();

const nutritionsStore = useNutritionsStore();
const { nutritions } = storeToRefs(nutritionsStore);

onMounted(() => {
  nutritionsStore.load();
});

const remove = (nutrition: Nutrition) => {
  nutritionsStore.deleteNutrition(nutrition);
  location.reload();
};
</script>

<style></style>
