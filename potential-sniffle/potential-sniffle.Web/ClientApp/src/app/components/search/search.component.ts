import { Component, OnInit } from '@angular/core';
import { SearchClient, Search } from '../../web-api-client';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {

  public searches: Search[];

  constructor(private client: SearchClient) {
    this.searches = new Array<Search>();

    client.get(undefined, undefined, undefined, undefined).subscribe((result: any) => {
      this.searches = result;
    }, (error: any) => console.error(error));
  }

  ngOnInit(): void {
  }

}
