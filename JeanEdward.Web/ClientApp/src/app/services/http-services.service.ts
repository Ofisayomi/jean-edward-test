import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HttpServicesService {

  constructor(private httpClient: HttpClient) { 

  }

  getSearchResults(){
    return this.httpGet('api/movies/results');
  }

  movieSearch(title: string, query: Record<string, string>){
    let queryString = new URLSearchParams(query).toString()
    return this.httpGet(`api/movies/search/${title}?${queryString}`);
  }

  private httpGet(endpoint: string){
    return this.httpClient.get(environment.baseUrl + endpoint);
  }

}
