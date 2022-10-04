import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CommentTypeService {
  private endpoint = "CommentType";

  constructor(private http: HttpClient) { }

  getCommentTypeList() : Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiUrl}${this.endpoint}/ListCommentTypes`);
  }
}
