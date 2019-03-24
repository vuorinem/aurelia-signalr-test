import { autoinject } from "aurelia-framework";
import { HttpClient, json } from "aurelia-fetch-client";

@autoinject
export class App {

  private newItem: string = "";

  constructor(private httpClient: HttpClient) {
    httpClient.baseUrl = 'https://localhost:5001';
  }

  private add(item: string) {
    this.httpClient.fetch('/items', {
      method: 'post',
      body: json(this.newItem),
    });
  }

}
