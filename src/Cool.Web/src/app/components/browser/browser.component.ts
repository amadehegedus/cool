import { Component, OnInit } from '@angular/core';
import { CaffDto, CaffService, CommentDto, TagDto } from 'src/app/api/app.generated';
import { UserManagementService } from 'src/app/services/user-management.service';
import { getDateString } from 'src/app/utils/dateTimeUtil';
import { faComment, faHashtag, faTrashAlt } from "@fortawesome/free-solid-svg-icons";

@Component({
  selector: 'app-browser',
  templateUrl: './browser.component.html',
  styleUrls: ['./browser.component.scss']
})
export class BrowserComponent implements OnInit {
  faComment = faComment;
  faTrashAlt = faTrashAlt;
  faHashTag = faHashtag;

  caffs: CaffDto[] = [];
  filterOptions: string = '';
  newComment: string = '';
  selectedForDeleteCaffId: number = -1;
  selectedForTagModifyCaffId: number = -1;
  selectedForMessageModifyCaffId: number = -1;
  modifyTags: TagDto[] = [];
  isLoading: boolean;

  constructor(private userManagement: UserManagementService, private api: CaffService) {
    this.isLoading = true;
    this.loadData();
  }

  loadData(): void {
    this.api.getAllCaffs().subscribe(result => {
      this.caffs = result;
      this.isLoading = false;
    }, err => {
      this.isLoading = false;
    });
  }

  filterCaffs(): CaffDto[] {
    if (this.filterOptions === '') {
      return this.caffs;
    }
    return this.caffs.filter(c => {
      return c.tags?.filter(ct => ct.text === this.filterOptions.toLowerCase() || ct.text?.startsWith(this.filterOptions.toLowerCase())).length !== 0
        || c.creator === this.filterOptions.toLowerCase() || c.creator?.startsWith(this.filterOptions.toLowerCase());
    });
  }

  isAdmin() {
    return this.userManagement.isAdmin();
  }

  getDateString(timeStamp: Date) {
    return getDateString(timeStamp);
  }


  addComment(caffId: number): void {
    this.api.addComment(caffId, this.newComment).subscribe(r => {
      this.loadData();
      this.newComment = '';
    });
  }

  deleteComment(commentId: number): void {
    this.api.removeComment(commentId).subscribe(r => {
      this.loadData();
    });
  }

  deleteCaff(caffId: number): void {
    this.api.deleteCaff(caffId).subscribe(r => {
      this.loadData();
    });
  }

  onAddTag(ev: any, caffId: number) {
    this.api.addTag(caffId, ev.text.toLowerCase()).subscribe(r => {
      this.loadData();
    });
  }

  onRemoveTag(ev: any) {
    this.api.removeTag(ev.id).subscribe(r => {
      this.loadData();
    });
  }

  tagsCaffClicked(caffId: number): void {
    this.selectedForTagModifyCaffId = caffId;
    this.modifyTags = Object.create(this.caffs.find(c => c.id === this.selectedForTagModifyCaffId)?.tags!);
  }

  getComments(): CommentDto[] {
    if (this.selectedForMessageModifyCaffId !== -1) {
      return this.caffs.find(c => c.id === this.selectedForMessageModifyCaffId)?.comments!;
    }
    return [];
  }
}
