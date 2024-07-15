import { Component, OnInit } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { MyDialogComponent } from './my-dialog/my-dialog.component';
import { PageEvent } from '@angular/material/paginator';

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
export class AppComponent implements OnInit {
  posts!: Observable<Post[]>;
  totalItems = 300;
  pageSize = 10;
  currentPage = 0;

  constructor(private apiService: ApiService, public dialog: MatDialog) {}

  ngOnInit() {
    this.posts = this.apiService.getPosts(this.currentPage, this.pageSize);
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(MyDialogComponent, {
      width: '500px',
      data: {},
    });
  }

  onPageChange(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex;
    this.posts = this.apiService.getPosts(this.currentPage, this.pageSize);
  }
}
