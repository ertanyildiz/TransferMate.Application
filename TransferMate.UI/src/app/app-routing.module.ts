import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TaskListComponent } from './task/task-list/task-list.component';
import { TaskDetailComponent } from './task-detail/task-detail.component';



const routes: Routes = [
  {path:'tasks', component: TaskListComponent},
  {path:'task', component: TaskDetailComponent},
  { path: '**', component: TaskListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
