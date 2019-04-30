<template>
    <div class="projects">
    <h1 class="subheading grey--text">Projects</h1>  
    <v-container fluid class="my-5">
      <v-expansion-panel>
        <v-expansion-panel-content v-for="project in myProjects" :key="project.title">
          <div slot="header" class="py-1">{{ project.title }}</div>
          <v-card>
            <v-card-text class="px-4 grey--text">
              <div class="font-weight-bold">Due by {{ project.due }}</div>
              <div>{{ project.content }}</div>
            </v-card-text>
          </v-card>
        </v-expansion-panel-content>
      </v-expansion-panel>
    </v-container>
    </div>
</template>
<script>
import {store} from '@/store/'
export default {
  data() {
    return {
      projects: []
    };
  },
  computed:{
      user(){
          return this.$store.getters.user
      },
      myProjects(){
          return this.projects.filter(project=>{
              return project.person === this.$store.getters.user.id && project.status != 'complete'
          })
      }
  },
  created(){
      
  }
};
</script>