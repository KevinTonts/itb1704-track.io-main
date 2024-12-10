import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import ExercisesVue from '@/views/Exercises.vue';
import AddExerciseVue from '@/views/AddExercise.vue';
import MealPlanVue from '@/views/MealPlan.vue';
import NutritionsVue from '@/views/Nutrition.vue';
import HistoryVue from '@/views/History.vue';
import AddNutritionVue from '@/views/AddNutrition.vue';
import WorkoutPlanVue from '@/views/WorkoutPlan.vue';
import IndexVue from '@/views/IndexView.vue'
import SelectedMealPlanVue from '@/views/SelectedMealPlan.vue';
import SelectedWorkoutPlanVue from '@/views/SelectedWorkoutPlan.vue';
import LoginVue from '@/views/Login.vue';
import ProfileVue from '@/views/Profile.vue';
import UpdateUserVue from '@/views/UpdateUser.vue';
import RegisterVue from '@/views/Register.vue';
import { useAuthStore } from '@/stores/authStore';


const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'Index',
    component: IndexVue,
  },
  {
    path: '/exercise',
    name: 'Harjutused',
    component: ExercisesVue,
    meta: {requiresAuth: true} 
  },
  {
    path: '/newexercise',
    name: 'Lisa harjutus',
    component: AddExerciseVue,
    // meta: {requiresAuth: true}
  },
  {
    path: '/nutrition',
    name: 'Toitumine',
    component: NutritionsVue,
  },
  {
    path: '/history',
    name: 'Ajalugu',
    component: HistoryVue,
  },
  {
    path: '/mealplan',
    name: 'Toidukava',
    component: MealPlanVue,
  },
  {
    path: '/newnutrition',
    name: 'Lisa toitumine',
    component: AddNutritionVue,
  },
  {
    path: '/workoutplan',
    name: 'Treeningkavad',
    component: WorkoutPlanVue,
  },
  {
    path: '/selectedMealplan',
    name: 'ValitudToiduKavad',
    component: SelectedMealPlanVue,
  },

  {
    path: '/selectedWorkoutplan',
    name: 'ValitudTreeningkava',
    component: SelectedWorkoutPlanVue,
  },
  {
    path: '/login',
    name: 'Logi sisse',
    component: LoginVue,
  },
  {
    path: '/profile',
    name: 'profiil',
    component: ProfileVue,
    meta: {requiresAuth: true}
  },
  {
    path: '/updateProfile',
    name: 'Muuda profiili',
    component: UpdateUserVue,
    meta: {requiresAuth: true}
  },
  {
    path: '/register',
    name: 'Registreeri',
    component: RegisterVue,
  }

];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  const useAuth = useAuthStore();
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    if (useAuth.isAuthenticated) {
      next();
      return;
    }
    next('/login');
  } else {
    next();
  }
});


export default router;
