import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ApiService } from '../api.service';
import { Post } from '../app.component';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-my-dialog',
  templateUrl: './my-dialog.component.html',
  styleUrls: ['./my-dialog.component.css'],
})
export class MyDialogComponent implements OnInit {
  postForm = new FormGroup({
    title: new FormControl('', [Validators.required]),
    url: new FormControl('', [Validators.required]),
  });

  constructor(
    public dialogRef: MatDialogRef<MyDialogComponent>,
    private apiService: ApiService
  ) {}

  ngOnInit(): void {}

  onSave() {
    if (this.postForm.valid) {
      const newPost = this.postForm.value;
      this.apiService.addPost(newPost).subscribe();
      this.dialogRef.close(newPost);
    }
  }
}
