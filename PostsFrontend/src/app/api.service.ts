import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private apiUrl = 'https://localhost:7094/api';

  constructor(private http: HttpClient) {}

  getPosts(pageNumber: number, pageSize: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/posts`, { pageNumber, pageSize });
  }

  getPostDetails(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/posts/${id}`);
  }

  addPost(postData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/posts/add`, postData);
  }
}
