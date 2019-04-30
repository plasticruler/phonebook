<template>
<v-dialog max-width="600px" v-model="dialog">
    <v-btn v-if="IsUserAuthenticated" flat slot="activator" class="success">Add new Project</v-btn>
    <v-btn v-if="IsUserAuthenticated" flat slot="activator-contact" class="success">Add new Contact</v-btn>
    <v-card>
        <v-card-title>
            <h2>Add a new project</h2>
        </v-card-title>
        <v-card-text>
            <v-form class="px-3" ref="form">
                <v-text-field v-model="title" label="Title" prepend-icon="folder" :rules="inputRules"></v-text-field>
                <v-textarea v-model="content" label="Information" prepend-icon="edit" :rules="inputRules"></v-textarea>
                <v-menu v-model="menu" :close-on-content-click="false">
                <v-text-field slot="activator" :rules="inputRules" :value="formattedDate" clearable label="Due date" prepend-icon="date_range"></v-text-field>
                <v-date-picker v-model="due" @change="menu=false"></v-date-picker>
                </v-menu>

                <v-spacer></v-spacer>
 
                <v-btn  flat @click="submit" class="success mx-0 mt-3" :loading="loading">Add Project</v-btn>
            </v-form>
        </v-card-text>
    </v-card>
</v-dialog>
</template>
<script>

//import {fb} from '@/fb'; //surround with curly brace because fb in that file is not marked as default
export default {
    data(){
        return {
            title:'',
            content:'',
            due:null,
            menu:false,
            inputRules:[
                v => !!v||'This field is required.',
                v => v.length >= 3 || 'Minimum length of this field is 3 characters.'
            ],
            loading:false,
            dialog:false
        }
    },
    methods:{
        submit(){
            if (this.$refs.form.validate()){
                this.loading = true;
                console.log(this.$store.getters.user)
                const project = {
                    title:this.title,
                    content: this.content,
                    due:format(this.due, 'Do MMM YYYY'),
                    userid: this.$store.getters.user.uid,
                    email: this.$store.getters.user.email,
                    status: 'ongoing'
                }
                /*

                save you stuff here


                fb.collection('projects').add(project)
                .then(()=>{
                    this.loading = false;
                    this.dialog = false;
                    this.$emit('projectAdded');
                })*/
            }
        }
    },
    computed:{
        formattedDate(){ 
            return this.due?format(this.due, 'Do MMM YYYY'):''
        },
        IsUserAuthenticated(){
            return this.$store.getters.user !== null && this.$store.getters.user !== undefined
        }        
    }
}
</script>