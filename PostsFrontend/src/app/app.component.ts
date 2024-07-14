import { Component, OnDestroy, OnInit } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { MyDialogComponent } from './my-dialog/my-dialog.component';

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
  posts!: Observable<Post[]>;

  constructor(private apiService: ApiService, public dialog: MatDialog) {}

  ngOnDestroy(): void {
  }

  ngOnInit() {
    this.posts = this.apiService.getPosts();
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(MyDialogComponent, {
      width: '500px',
      data: { /* данные для передачи в диалог */ },
    });
  }
}
