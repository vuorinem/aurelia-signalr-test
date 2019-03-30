import * as signalR from "@aspnet/signalr";
import { autoinject, ComponentAttached, ComponentDetached } from "aurelia-framework";
import { HttpClient, json } from "aurelia-fetch-client";

@autoinject
export class TodoList implements ComponentAttached, ComponentDetached {
  private connection: signalR.HubConnection;
  private items: Item[] = [];

  constructor(private httpClient: HttpClient) {
  }

  public async attached() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/hub')
      .build();

    this.connection.on("update", () => this.update());

    await this.connection.start();
    await this.update();
  }

  public detached() {
    this.connection.stop();
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
