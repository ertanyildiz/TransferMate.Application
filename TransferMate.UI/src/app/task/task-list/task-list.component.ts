import { Component, OnInit } from '@angular/core';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {

  constructor(private taskService: TaskService) { }

  tasks: any = [];
  task: any;
  modalTitle: string = "";
  editMode: boolean = false;

  ngOnInit(): void {
    this.refreshTaskList();
  }

  refreshTaskList(){
    this.taskService
    .getTaskListView()
    .subscribe((result: any[]) => this.tasks = result);
  }

  addClick(){
    this.task = [];
    this.modalTitle = "Add new Task";
    this.editMode = true;
  }

  editClick(item: any) {
    this.task = item;
    this.modalTitle = "Edit Task";
    this.editMode = true;
  }

  deleteClick(item: any){
    this.task = item;
    this.modalTitle = "Delete Task";
    this.editMode = true;
  }

  closeClick(){
    this.editMode = false;
  }

}
