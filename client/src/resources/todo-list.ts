import { autoinject, ComponentAttached } from "aurelia-framework";
import { HttpClient } from "aurelia-fetch-client";

@autoinject
export class TodoList implements ComponentAttached {

  private items: Item[] = [];

  constructor(private httpClient: HttpClient) {
  }

  public async attached() {
    await this.update();
  }

  private async update() {
    const response = await this.httpClient.fetch('/items');
    this.items = await response.json();
  }

  private save(item: Item) {
    this.httpClient.fetch(`/items/${item.id}`, {
      method: 'put',
      body: json(item),
    });
  }

  private remove(item: Item) {
    this.httpClient.fetch(`/items/${item.id}`, {
      method: 'delete',
    });
  }

}
