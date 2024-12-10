<template>
  <div id="body">
  <div class="addForm">
    <div class="max-w-md w-full bigbox">
      <div class="text-center">
        <h1 class="font-bold">{{ title }}</h1>

        <div class="register">
          <div>
            <label for="firstname">Kasutajanimi</label>
            <input
              id="firstname"
              name="firstname"
              v-model="userToUpdate.username"
              class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            />
          </div>
          <div>
            <label for="firstname">parool</label>
            <input
              id="firstname"
              name="firstname"
              v-model="userToUpdate.password"
              type="password"
              class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            />
          </div>
          <div>
            <label for="firstname">Eesnimi</label>
            <input
              id="firstname"
              name="firstname"
              v-model="userToUpdate.firstName"
              class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            />
          </div>
          <div>
            <label for="lastName">Perenimi</label>
            <input
              id="lastName"
              name="lastName"
              v-model="userToUpdate.lastName"
              class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            />
          </div>

          <div class="text-right">
            <router-link to="/index">
              <button class="button">Tagasi</button>
            </router-link>

            <button class="button" @click="submitForm">Registreeri</button>
          </div>
          <!-- <div class="label">Sugu: {{ user?.gender }}</div> -->
        </div>
      </div>
    </div>
  </div>
  </div>
</template>

<script setup lang="ts">
import { useUsersStore } from '@/stores/usersStore';
import { storeToRefs } from 'pinia';
import { onMounted, ref, Ref } from 'vue';
import { User, Gender } from '@/modules/user';
import { useRouter } from 'vue-router';

defineProps<{ title: string }>();

const usersStore = useUsersStore();
const { user } = storeToRefs(usersStore);
const router = useRouter();

const userToUpdate: Ref<User> = ref({
  username: '',
  password: '',
  id: 0,
  firstName: '',
  lastName: '',
  height: 0,
  weight: 0,
  age: 0,
  gender: Gender.Female,
});

const submitForm = () => {
  usersStore.registerUser({ ...userToUpdate.value });

  userToUpdate.value.username = '';
  userToUpdate.value.password = '';
  userToUpdate.value.firstName = '';
  userToUpdate.value.lastName = '';
  // userToUpdate.value.gender= Gender.Other

  router.push({ name: 'Logi sisse' });
};
</script>

<style></style>
