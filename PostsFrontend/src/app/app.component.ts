import { Component, OnDestroy, OnInit } from '@angular/core';
import { ApiService } from './api.service';
import { Subscriber, Subscription } from 'rxjs';

export interface Post {
  id: number;
  title: string;
  url: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit, OnDestroy {
  posts!: Post[];
  subscriptions!: Subscription;

  constructor(private apiService: ApiService) {}

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  ngOnInit() {
    this.subscriptions = this.apiService.getPosts().subscribe((posts) => {
      this.posts = posts;
    });
  }
}
