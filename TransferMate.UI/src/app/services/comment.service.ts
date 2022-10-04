import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private endpoint = "Comment";

  constructor(private http: HttpClient) { }

  getCommentList() : Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiUrl}${this.endpoint}/ListCommentView`);
  }

  getCommentListWithId(id?: any) : Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiUrl}${this.endpoint}/ListCommentView?id=${id}`);
  }

  getCommentListWithTaskId(taskId?: any) : Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiUrl}${this.endpoint}/SelectCommentsByTaskIdView?id=${taskId}`);
  }

  addNewComment(comment?: any): Observable<any> {
    return this.http.post<any>(
        `${environment.apiUrl}${this.endpoint}/CreateComment`,
        comment);
  }

  updateComment(comment?: any) {
    return this.http.patch<any>(
        `${environment.apiUrl}${this.endpoint}/UpdateComment`,
        comment);
  }

  deleteComment(comment?: any) {
    return this.http.delete(
      `${environment.apiUrl}${this.endpoint}/DeleteComment?id=${comment.id}`,
      );
  }
}
