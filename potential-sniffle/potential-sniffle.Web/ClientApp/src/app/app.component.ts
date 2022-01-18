import { Component, OnInit } from '@angular/core';
import { SearchClient, Search } from './web-api-client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'ClientApp';

  public searches: Search[];

  values: any = {};

  constructor(private client: SearchClient) {
    this.searches = new Array<Search>();
  }

  search(): void {
    let scrapeURL = (document.getElementById("scrapeURL") as HTMLTextAreaElement).value;
    let searchURL = (document.getElementById("searchURL") as HTMLTextAreaElement).value;
    let searchKeyword = (document.getElementById("searchKeyword") as HTMLTextAreaElement).value;
    let searchLimit = (document.getElementById("searchLimit") as HTMLTextAreaElement).value;

    this.client.get(scrapeURL, searchURL, searchKeyword, searchLimit).subscribe((result: any) => {
      this.searches = result;
    }, (error: any) => console.error(error));
  }

  ngOnInit(): void {
  }

}
