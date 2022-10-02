import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TypeService {
  private endpoint = "Type";

  constructor(private http: HttpClient) { }

  getTypeList() : Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiUrl}${this.endpoint}/ListTypes`);
  }
}
