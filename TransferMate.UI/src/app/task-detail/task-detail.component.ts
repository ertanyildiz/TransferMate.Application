import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommentService } from '../services/comment.service';
import { TaskService } from '../services/task.service';

@Component({
  selector: 'app-task-detail',
  templateUrl: './task-detail.component.html',
  styleUrls: ['./task-detail.component.css']
})
export class TaskDetailComponent implements OnInit {

  constructor(private taskService: TaskService,
     private commentService: CommentService, private route: ActivatedRoute,
     private datePipe: DatePipe) { }

  tasks: any = null;
  comments: any = null;
  queryStrings: any;
  selectedComment: any;
  editMode = false;
  modalTitle = "";
  searchText: any;


  ngOnInit(): void {
    this.route.queryParamMap
      .subscribe((params) => {
        this.queryStrings = { ...params.keys, ...params };
      }
      );
    this.loadSelectedTask();
    this.listCommentsWithTaskId();
  }

  loadSelectedTask() {
    this.taskService
      .getTaskListViewWithId(this.queryStrings.params.id)
      .subscribe((result: any[]) => this.tasks = result);
    console.log(this.tasks);
  }

  listCommentsWithTaskId(){
    this.commentService
      .getCommentListWithTaskId(this.queryStrings.params.id)
      .subscribe((result: any[]) => this.comments = result);
  }

  addNewComment(){
    this.selectedComment = {
      comment: {
        id: 0,
        createdDate: this.datePipe.transform(new Date(),"yyyy-MM-dd"),
        commentContent: "",
        commentType: 0,
        reminderDate: this.datePipe.transform(new Date(),"yyyy-MM-dd"),
        task: this.queryStrings.params.id
      }
    };
    this.editMode = true;
    this.modalTitle = "Add new comment";
  }

  editClick(item: any) {
    this.selectedComment = item;
    this.modalTitle = "Edit Comment";
    this.editMode = true;
  }

  deleteClick(item: any){
    this.selectedComment = item;
    this.modalTitle = "Delete Comment";
    this.editMode = true;
  }

  closeClick(){
    this.editMode = false;
  }

  objectKeys(obj: any) {
    return Object.keys(obj);
  }
}
