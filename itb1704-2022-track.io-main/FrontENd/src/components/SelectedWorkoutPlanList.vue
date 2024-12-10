<!-- Oleks vaja, et iga päeva kohta kuvatakse eraldi alam link, millele vajutades avaneb päevane kava -->
<template>
  <div id="body">
    <div id="app">
      <div class="bigbox py-12 px-4 sm:px-6">
        <div class="text-center">
          <div v-if="showError">
            <div class="h2">Treeningkava pole valitud</div>
            <br />
            <div class="text-right">
              <router-link to="/workoutplan">
                <button class="button">Vali treeningkava siit</button>
              </router-link>
            </div>
          </div>
          <div v-else>
            <h1 class="font-bold">{{ title }}</h1>

            <ul>
              <button
                v-for="days in DayOfTheWeek"
                v-on:click="filterWeekday(days)"
                type="button"
                class="button"
                id="day"
              >
                {{ days }}
              </button>
            </ul>

            <DataTable :value="exercisesByWeekday" id="exercises">
              <Column field="title" header="Nimi" />
              <Column field="description" header="Kirjeldus" />

              <Column field="burnedCalories" header="Poletatud kalorid" />
              <Column>
                <template #body="{ data }">
                  <input
                    id="check"
                    v-if="data.exerciseFinished == true"
                    type="checkbox"
                    @click="unFinishExercise(data)"
                    checked
                  />
                  <input
                    id="check"
                    v-if="data.exerciseFinished == false"
                    type="checkbox"
                    @click="finishExercise(data)"
                  />
                </template>
              </Column>
            <Column>
            <template #body="{ data }">
              <button class="deleteButton" @click="remove(data)">
                <i class="fa fa-trash-o"></i>
              </button>
            </template>
          </Column>
            </DataTable>
          </div>
        </div>
        <div class="text-right">
          <router-link to="/">
            <button class="button">Tagasi</button>
          </router-link>
          <router-link to="/newexercise">
            <button class="button">Lisa Harjutus</button>
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { Exercise, DayOfTheWeek } from '@/modules/exercise';
import { WorkoutPlan } from '@/modules/workoutPlan';
import { useWorkoutPlansStore } from '@/stores/workoutPlansStore';
import { useExercisesStore } from '@/stores/exercisesStore';
import { storeToRefs } from 'pinia';
import { onMounted, ref } from 'vue';

defineProps<{ title: string }>();

const exercisesStore = useExercisesStore();
const workoutPlansStore = useWorkoutPlansStore();
const { Exercises, exercisesByWeekday } = storeToRefs(workoutPlansStore);
const { exercises } = storeToRefs(exercisesStore);



const finishExercise = (exercise: Exercise) => {
  exercisesStore.finishExercise(exercise);
};

const unFinishExercise = (exercise: Exercise) => {
  exercisesStore.unFinishExercise(exercise);
};

const filterWeekday = (weekDay: DayOfTheWeek) => {
  workoutPlansStore.filterSelectedExercisesByWeekday(weekDay);

  var inputs = <HTMLInputElement>document.getElementById('check');
  
  exercisesByWeekday.value.forEach(element =>{
      if(element.exerciseFinished==true){
        inputs.checked=true;
      }
      if(element.exerciseFinished==false){
        inputs.checked=false;
      }

    });
};



let showError = ref(false);

const getPageStatus = async () => {
  showError.value = await workoutPlansStore.getStatus();
};

const remove = (exercise: Exercise) => {
  exercisesStore.deleteExercise(exercise);
  location.reload();
};


onMounted(() => {
  getPageStatus();
  workoutPlansStore.loadSelected();
});
</script>
