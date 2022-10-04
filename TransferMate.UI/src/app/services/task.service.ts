import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private endpoint = "Task";

  constructor(private http: HttpClient) { }

  getTaskListView() : Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiUrl}${this.endpoint}/ListTasksView`);
  }

  getTaskListViewWithId(id?: any) : Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiUrl}${this.endpoint}/ListTasksView?id=${id}`);
  }

  addNewTask(task?: any): Observable<any> {
    return this.http.post<any>(
        `${environment.apiUrl}${this.endpoint}/CreateTask`,
      task);
  }

  updateTask(task?: any) {
    return this.http.patch<any>(
        `${environment.apiUrl}${this.endpoint}/UpdateTask`,
      task);
  }

  deleteTask(task?: any) {
    return this.http.delete(
      `${environment.apiUrl}${this.endpoint}/DeleteTask?id=${task.id}`,
      );
  }
}
