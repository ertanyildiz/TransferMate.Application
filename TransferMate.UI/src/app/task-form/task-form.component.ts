import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TaskService } from 'src/app/services/task.service';
import { StatusService } from '../services/status.service';
import { TypeService } from '../services/type.service';
import { UserService } from '../services/user.service';


@Component({
  selector: 'app-task-form',
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.css']
})
export class TaskFormComponent implements OnInit {

  constructor(private taskService: TaskService, private statusService: StatusService,
    private typeService: TypeService, private userService: UserService) { }

  @Input() inputTask?: any;
  @Output() refreshCustomerList = new EventEmitter<string>();
  errorMessage: any;
  status: any = [];
  types: any = [];
  users: any = [];

  ngOnInit(): void {
    this.listStatus();
    this.listTypes();
    this.listUser();
  }

  addTask(item: any) {
    this.taskService.addNewTask(item)
      .subscribe({
        next: (v) => window.location.reload(),
        error: (e) => this.errorMessage = e.error,
        complete: () => this.refreshCustomerList.emit()
      })
  }

  updateTask(item: any) {
    this.taskService.updateTask(item)
      .subscribe({
        next: (v) => window.location.reload(),
        error: (e) => this.errorMessage = e.error,
        complete: () => this.refreshCustomerList.emit()
      })
  }

  deleteTask(item: any) {
    this.taskService.deleteTask(item)
      .subscribe({
        next: (v) => window.location.reload(),
        error: (e) => this.errorMessage = e.error,
        complete: () => this.refreshCustomerList.emit()
      });
  }

  listStatus() {
    this.statusService
    .getTaskListView()
    .subscribe((result: any[]) => this.status = result);
  }

  listTypes() {
    this.typeService
    .getTypeList()
    .subscribe((result: any[]) => this.types = result);
  }

  listUser() {
    this.userService
    .getUserList()
    .subscribe((result: any[]) => this.users = result);
  }

}
