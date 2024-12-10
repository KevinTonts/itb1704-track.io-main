import useApi, {useApiRawRequest} from '@/modules/api';
import { defineStore } from 'pinia';
import { ref } from 'vue';
import { User } from '@/modules/user';
import { useAuthStore } from './authStore';

export const useUsersStore = defineStore('usersStore', () => {

    const authStore = useAuthStore();
    let users = ref<User[]>([]);
    let user = ref<User>();
    let bmiValue = ref<number>();

    const loadUsers = async () => {
        const apiGetUsers = useApi<User[]>('users', {
            headers: {Authorization: 'Bearer' + authStore.token},
        });
        await apiGetUsers.request();

        if (apiGetUsers.response.value) {
            return apiGetUsers.response.value!;
          }
      
          return [];
    }

    const load =async () => {
        users.value = await loadUsers();
        user.value = users.value.at(0);
        bmiValue.value = await calculateBMI(user.value?.weight, user.value?.height);
    }

    const updateUser =async (userToUpdate:User) => {
        const apiUpdateUser =useApi<User>(`users/${userToUpdate.id}`, {
            method: 'PUT',
            headers: {
                Authorization: 'Bearer ' + authStore.token,
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(userToUpdate),
        });
        await apiUpdateUser.request();
        if (apiUpdateUser.response.value) {
            user.value = apiUpdateUser.response.value!;
          }
    };

    const registerUser = async (userToReg:User) => {
        const apiRegisterUser = useApi<User>('users/register', {
            method: 'POST',
            headers: {
              Authorization: 'Bearer ' + authStore.token,
              Accept: 'application/json',
              'Content-Type': 'application/json',
            },
            body: JSON.stringify(userToReg),
          });

          await apiRegisterUser.request();
          if (apiRegisterUser.response.value) {
            users.value.push(apiRegisterUser.response.value!);
          }
    };

    const calculateBMI =async (weight?:number, height?: number) => {
        if(weight == 0 && height == 0){
            return 0
        }

        var bmi = weight! / Math.pow(height!, 2) * 10000
        return Math.round(bmi * 100) / 100;
    }

    return{ users, user, load,  updateUser, registerUser, bmiValue}
});