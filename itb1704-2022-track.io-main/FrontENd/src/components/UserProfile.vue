<template>
  <div id="body">
    <div class="bigbox py-12 px-4 sm:px-6">
      <div class="text-center">
        <h1 class="font-bold">{{ title }}</h1>
        <div class="profile">
          <div class="label">Eesnimi: {{ user?.firstName }}</div>
          <div class="label">Perenimi: {{ user?.lastName }}</div>
          <div class="label">Pikkus: {{ user?.height }}</div>
          <div class="label">Kaal: {{ user?.weight }}</div>
          <div class="label">Vanus: {{ user?.age }}</div>
          <div class="label">Sugu: {{ user?.gender }}</div>
          <div class="label">
            Kehamassi indeks:
            {{ bmiValue }}
          </div>
        </div>
        <router-link to="/updateProfile">
          <button class="button">
            Muuda profiili<i class="fa fa-angle-right px-3"></i>
          </button>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { User } from '@/modules/user';
import { useUsersStore } from '@/stores/usersStore';
import { storeToRefs } from 'pinia';
import { onMounted, ref } from 'vue';

defineProps<{ title: string }>();

const usersStore = useUsersStore();
const { user, bmiValue } = storeToRefs(usersStore);

onMounted(() => {
  usersStore.load();
});
</script>

<style></style>
