import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ApiService } from '../api.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-my-dialog',
  templateUrl: './my-dialog.component.html',
  styleUrls: ['./my-dialog.component.css'],
})
export class MyDialogComponent implements OnInit, OnDestroy {
  postForm = new FormGroup({
    title: new FormControl('', [Validators.required]),
    url: new FormControl('', [Validators.required]),
  });
  subscriptions!: Subscription;

  constructor(
    public dialogRef: MatDialogRef<MyDialogComponent>,
    private apiService: ApiService
  ) {}

  ngOnInit(): void {}

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  onSave() {
    if (this.postForm.valid) {
      const newPost = this.postForm.value;
      this.subscriptions = this.apiService
        .addPost(newPost)
        .subscribe(() => this.dialogRef.close(newPost));
    }
  }
}
