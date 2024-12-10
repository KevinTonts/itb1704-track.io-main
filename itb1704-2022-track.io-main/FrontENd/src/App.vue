This site has been acquired by Toptal (Attention! API endpoint has changed) Save
New Duplicate & Edit Just Text1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20
21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43 44 45 46 47
48 49 50 51 52 53 54 55 56 57 58 59 60 61 62 63 64 65 66 67 68 69 70 71 72 73 74
75 76 77 78 79 80 81 82 83 84 85 86 87 88 89 90 91 92 93 94 95 96 97 98 99 100
101 102 103 104 105 106 107 108 109 110 111 112 113 114 115 116 117 118 119 120
121 122 123 124 125 126 127 128 129 130 131 132 133 134 135 136 137 138 139 140
141 142 143 144 145 146 147 148 149 150 151 152 153 154 155 156 157 158 159 160
161 162 163 164 165 166 167 168 169 170 171 172 173 174 175 176 177 178 179 180
181 182 183 184 185 186 187 188 189 190 191 192 193 194 195 196 197 198 199 200
201 202 203 204 205 206 207 208 209 210 211 212 213 214 215 216 217 218 219 220
221 222 223 224 225 226 227 228 229 230 231 232 233 234 235 236 237 238 239 240
241 242 243 244 245 246 247 248 249 250 251 252 253 254 255 256 257 258 259 260
261 262 263 264 265 266 267 268 269 270 271 272 273 274 275 276 277 278 279 280
281 282 283 284 285 286 287 288 289 290 291 292 293 294 295 296 297 298 299 300
301 302 303 304 305 306 307 308 309
<template>
  <link
    rel="stylesheet"
    href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"
  />

  <!-- seda linki pole ekstra vaja lisada kusagile -->

  <input id="menu__toggle" type="checkbox" />
  <label class="menu__btn" for="menu__toggle">
    <span></span>
  </label>

  <ul class="menu__box">
    <router-link to="/">
      <div class="logo">
        <img src="/logo.png" style="width: 150px; margin: 0 auto" />
      </div>
    </router-link>

    <router-link to="/profile" class="menu__item" v-if="isAuthenticated"
      >Profiil</router-link
    >

    <router-link to="/workoutplan" class="menu__item"
      >Treeningkavad</router-link
    >

    <router-link to="/mealplan" class="menu__item">Toidukavad</router-link>


    <router-link to="/history" class="menu__item">Ajalugu</router-link>

    <router-link
      to="/register"
      class="menu__item"
      v-if="isAuthenticated == false"
      >Registreeri</router-link
    >

    <router-link to="/login" class="menu__item" v-if="isAuthenticated == false"
      >Logi sisse</router-link
    >

    <router-link
      to="/login"
      class="menu__item"
      v-if="isAuthenticated"
      @click="signOut"
      >Logi Välja</router-link
    >
  </ul>

  <router-view />
</template>

<script setup lang="ts">
import { def } from '.pnpm/@vue+shared@3.2.31/node_modules/@vue/shared';
import { METHODS } from 'http';
import { defineComponent } from 'vue';
import { storeToRefs } from 'pinia';
import router from './router';
import { useAuthStore } from './stores/authStore';

const authStore = useAuthStore();
const { logout } = authStore;
const { isAuthenticated } = storeToRefs(authStore);

const signOut = () => {
  logout();
  router.push({ name: 'Logi sisse' });
};

// var moveMain=true;

// function openNav(){
//   if(moveMain){
//     document.getElementById('body')!.style.marginLeft = '200px';
//     moveMain=false;
//   }
//   else{
//     document.getElementById('body')!.style.marginLeft = '0px';
//     moveMain=true;
//   }
// }

// function openMenuItem(){
//   document.getElementById('body')!.style.marginLeft = '200px';
// }
</script>

<style>
#menu__toggle {
  opacity: 0;
}

#menu__toggle:checked + .menu__btn > span {
  transform: rotate(45deg);
}

#menu__toggle:checked + .menu__btn > span::before {
  top: 0;
  transform: rotate(0deg);
}

#menu__toggle:checked + .menu__btn > span::after {
  top: 0;
  transform: rotate(90deg);
}

#menu__toggle:checked ~ .menu__box {
  left: 0 !important;
}

#menu__toggle:checked ~ #body {
  margin-left: 200px;
}

.menu__btn {
  position: fixed;
  top: 15px;
  left: 20px;
  width: 30px;
  height: 30px;
  padding-top: 10px;
  cursor: pointer;
  z-index: 2;
}

.menu__btn > span,
.menu__btn > span::before,
.menu__btn > span::after {
  display: block;
  position: absolute;
  width: 100%;
  height: 2px;
  background-color: #616161;
  transition-duration: 0.25s;
}

.menu__btn > span::before {
  content: '';
  top: -8px;
}

.menu__btn > span::after {
  content: '';
  top: 8px;
}

.menu__box {
  display: block;
  position: fixed;
  top: 0;
  left: -100%;
  width: 200px;
  height: 100%;
  margin: 0;
  list-style: none;
  background-color: #ffffff;
  box-shadow: 2px 2px 6px rgba(0, 0, 0, 0.4);
  transition-duration: 0.25s;
  z-index: 1;
}
.menu__item {
  display: block;
  padding: 15px 24px;
  color: rgba(51, 51, 51, 0.585);
  font-family: 'montserrat', sans-serif;
  font-size: 18px;
  font-weight: 500;
  text-decoration: none;
  transition-duration: 0.25s;
  text-align: center;
}
.menu__item:hover {
  background-color: #cfd8dc;
}

#body {
  padding-top: 0px;
  max-height: 100%;
  transition-duration: 0.25s;
  margin-left: 0px;
}

.bigbox {
  background-color: #868686c1;
  padding: 2rem;
  border-radius: 20px;
  box-shadow: 2px 2px 3px rgba(0, 0, 0, 0.4);
}

.button {
  display: inline-block;
  padding: 0.35em 1.2em;
  border: 0.1em solid #ffffff;
  margin: 10px 0.3em 0.3em 0;
  border-radius: 20px;
  text-decoration: none;
  font-family: 'Roboto', sans-serif;
  background-color: #777777;
  font-weight: 300;
  color: #ffffff;
  text-align: center;
  transition: all 0.2s;
}

.button:hover {
  color: #ffffff;
  background-color: #b0b9b6;
}

.logo {
  padding-bottom: 40px;
  padding-top: 40px;
  text-align: center;
  font-size: x-large;
  color: whitesmoke;
  background: #181818;
}

body {
  background-image: url(https://cdn.wallpapersafari.com/9/19/gZkT1P.jpg);
  background-size: cover;
}


.addForm {
  display: flex;
  justify-content: center;
  padding-top: 5rem;
  padding-bottom: 3rem;
}

.deleteButton {
  padding: 7px;
  border-radius: 5px;
  font-size: x-large;
  transition: all 0.2s;
}

.deleteButton:hover {
  background-color: rgb(243, 99, 99);
}

.selectButton {
  padding: 7px;
  border-radius: 5px;
  font-size: 20px;
  transition: all 0.2s;
}

.selectButton:hover {
  background-color: #b0b9b6;
}

.h2 {
  color: #000000;
  font-family: 'Raleway', sans-serif;
  font-size: 30px;
  font-weight: 800;
  line-height: 36px;
  margin: 0 0 24px;
  text-align: center;
}

.h3 {
  color: black;
  font-family: 'Raleway', sans-serif;
  font-size: 20px;
  font-weight: 700;
  line-height: 36px;
  margin: 0 0 24px;
  text-align: center;
}

.grid-container {
  display: grid;
  grid-template-columns: auto auto auto;

  padding: 10px;
}

.grid-item {
  background-color: rgba(255, 255, 255, 0.8);
  border: 2px solid rgba(195, 221, 200, 0.385);
  padding: 0px;
  border-radius: 5px;
  text-align: center;
  margin: 50px;
}

.label {
  margin-top: 20px;
  text-align: left;
  margin-left: 30px;
}

.profile {
  background-color: rgb(203, 236, 234);
  border-radius: 10px;
  margin: auto;
  width: 50%;
  margin-top: 10px;
  padding-top: 2px;
  padding-bottom: 20px;
  max-width: 600px;
  align-self: center;
}
</style>
