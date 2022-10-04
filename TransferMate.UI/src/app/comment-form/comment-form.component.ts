import { Component, OnInit, Input } from '@angular/core';
import { CommentService } from '../services/comment.service';
import { CommentTypeService } from '../services/commentType.service';

@Component({
  selector: 'app-comment-form',
  templateUrl: './comment-form.component.html',
  styleUrls: ['./comment-form.component.css']
})
export class CommentFormComponent implements OnInit {

  constructor(private commentService: CommentService, private commentTypeService: CommentTypeService) { }

  errorMessage: any;
  @Input() selectedComment?: any;
  commentTypes: any = [];

  ngOnInit(): void {
    this.listCommentTypes();
  }

  addTask(item: any) {
    this.commentService.addNewComment(item)
      .subscribe({
        next: (v) => window.location.reload(),
        error: (e) => this.errorMessage = e.error
      })
  }

  updateTask(item: any) {
    this.commentService.updateComment(item)
      .subscribe({
        next: (v) => window.location.reload(),
        error: (e) => this.errorMessage = e.error
      })
  }

  deleteTask(item: any) {
    this.commentService.deleteComment(item)
      .subscribe({
        next: (v) => window.location.reload(),
        error: (e) => this.errorMessage = e.error
      });
  }

  
  listCommentTypes() {
    this.commentTypeService
    .getCommentTypeList()
    .subscribe((result: any[]) => this.commentTypes = result);
  }


}
