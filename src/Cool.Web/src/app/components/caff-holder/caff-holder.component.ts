import { Component, Input, OnInit } from '@angular/core';
import { faComment, faHashtag, faSearch, faTrashAlt, faUser } from "@fortawesome/free-solid-svg-icons";
import { CaffDto, CaffService, CommentDto, TagDto } from "../../api/app.generated";
import { UserManagementService } from "../../services/user-management.service";
import { getDateString } from "../../utils/dateTimeUtil";

@Component({
  selector: 'app-caff-holder',
  templateUrl: './caff-holder.component.html',
  styleUrls: []
})
export class CaffHolderComponent implements OnInit {
  faComment = faComment;
  faTrashAlt = faTrashAlt;
  faHashTag = faHashtag;
  faSearch = faSearch;
  faUser = faUser;

  caffs: CaffDto[] = [];
  filterOptions: string = '';
  newComment: string = '';
  selectedForDeleteCaffId: number = -1;
  selectedForTagModifyCaffId: number = -1;
  selectedForMessageModifyCaffId: number = -1;
  modifyTags: TagDto[] = [];
  isLoading: boolean = true;

  @Input() ownCaffs: boolean = false;

  constructor(private userManagement: UserManagementService, private api: CaffService) {

  }

  loadData(): void {
    if (this.ownCaffs) {
      this.api.getOwnCaffs().subscribe(result => {
        this.caffs = result;
        this.isLoading = false;
      }, err => {
        this.isLoading = false;
      });
    } else {
      this.api.getAllCaffs().subscribe(result => {
        this.caffs = result;
        this.isLoading = false;
      }, err => {
        this.isLoading = false;
      });
    }

  }
  ngOnInit(): void {
    this.loadData();
  }

  filterCaffs(): CaffDto[] {
    if (this.filterOptions === '') {
      return this.caffs;
    }
    return this.caffs.filter(c => {
      return c.tags?.some(ct => ct.text === this.filterOptions.toLowerCase() || ct.text?.startsWith(this.filterOptions.toLowerCase()))
        || c.creator === this.filterOptions.toLowerCase() || c.creator?.startsWith(this.filterOptions.toLowerCase());
    });
  }

  isAdmin() {
    return this.userManagement.isAdmin();
  }

  isOwnComment(userName?: string): boolean {
    if (userName) {
      return this.userManagement.getUsername() === userName;
    }
    return false;
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
