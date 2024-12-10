<template>
  <div
    class="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8"
  >
    <div class="max-w-md w-full space-y-8">
      <form @submit.prevent="submit">
        <div class="rounded-md shadow-sm mt-8 space-y-6">
          <div>
            <label for="username">Kasutajanimi:</label>
            <input
              type="text"
              v-model="user.username"
              name="username"
              class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            />
          </div>
          <div>
            <label for="password">Parool:</label>
            <input
              type="password"
              v-model="user.password"
              name="password"
              class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            />
          </div>

          <div>
            <button class="button" type="submit">
              <span class="absolute left-0 inset-y-0 flex items-center pl-3">
              </span>
              Logi sisse
            </button>
          </div>
        </div>
      </form>
      <p v-if="showError" class="text-red-400">
        Vigane kasutajanimi või parool
      </p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { Gender, User } from '@/modules/user';
import router from '@/router';
import { useAuthStore } from '@/stores/authStore';
import { ref } from 'vue';

const auth = useAuthStore();
const user: User = {
  id: 0,
  username: '',
  password: '',
  firstName: '',
  lastName: '',
  weight: 0,
  height: 0,
  gender: Gender.Other,
  age: 0,
};

let showError = ref(false);

const submit = async () => {
  showError.value = !(await auth.login(user));

  router.push({ name: 'Index' });
};
</script>
