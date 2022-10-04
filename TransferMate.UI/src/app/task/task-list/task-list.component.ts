import { Component, OnInit } from '@angular/core';
import { TaskService } from 'src/app/services/task.service';
import { DatePipe } from '@angular/common';
import { Router } from '@angular/router';


@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {

  constructor(private taskService: TaskService, private datePipe: DatePipe,
    private router: Router) { }

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
    this.task = {
      task : {
        title: "",
        createdDate: "",
        requiredByDate: "",
        taskDescription: "",
        taskStatus: "",
        taskType: 0,
        assignedTo: 0,
        nextActionDate: this.datePipe.transform(new Date(),"yyyy-MM-dd")
      }

    };
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

  goToDetails($event: MouseEvent, id: any) {
    $event.preventDefault();
    this.router.navigate(
      ['/task'],
      { queryParams: { id: id } }
    );
  }

}
