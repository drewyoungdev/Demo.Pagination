import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

interface Test {
  id: number;
  name: string;
}

interface PagedResult {
  currentPage: number;
  itemsPerPage: number;
  totalItems: number;
  pageCount: number;
  items: Test[]
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  private url = "http://localhost:5000/api/values/paginated";

  pagedResult: PagedResult;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.goTo(1);
  }

  goTo(pageNumber: number) {
    this.http.get<PagedResult>(`${this.url}/?currentPageNumber=${pageNumber}`)
      .subscribe(
        pagedResult => { this.pagedResult = pagedResult; }
      );
  }
}
