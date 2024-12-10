<template>
  <div id="body">
    <div class="bigbox py-12 px-4 sm:px-6">
      <div class="text-center">
        <h1 class="font-bold">{{ title }}</h1>

        <div class="profile">
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
          <div>
            <label for="height">Pikkus</label>
            <input
              id="height"
              name="height"
              v-model="userToUpdate.height"
              class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            />
          </div>
          <div>
            <label for="weight">Kaal</label>
            <input
              id="weight"
              name="weight"
              v-model="userToUpdate.weight"
              class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            />
          </div>
          <div>
            <label for="age">Vanus</label>
            <input
              id="age"
              name="age"
              v-model="userToUpdate.age"
              class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            />
          </div>

          <div class="text-right">
            <router-link to="/profile">
              <button class="button">Tagasi</button>
            </router-link>

            <button class="button" @click="updateProfile">Salvesta</button>
          </div>
          <!-- <div class="label">Sugu: {{ user?.gender }}</div> -->
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
  username: user.value!.username,
  password: user.value!.password,
  id: user.value!.id,
  firstName: '',
  lastName: '',
  height: 0,
  weight: 0,
  age: 0,
  gender: Gender.Female,
});

const updateProfile = () => {
  usersStore.updateUser({ ...userToUpdate.value });

  userToUpdate.value.firstName = '';
  userToUpdate.value.lastName = '';
  userToUpdate.value.height = 0;
  userToUpdate.value.weight = 0;
  userToUpdate.value.age = 0;
  // userToUpdate.value.gender= Gender.Other

  router.push({ name: 'profiil' });
};

onMounted(() => {
  usersStore.load();
});
</script>

<style></style>
