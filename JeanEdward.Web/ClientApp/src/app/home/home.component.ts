import { Component, OnInit } from '@angular/core';
import { HttpServicesService } from '../services/http-services.service';
import { MovieModel, MovieQuery, SearchResult } from '../model/movie.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls:['./home.component.scss']
})
export class HomeComponent implements OnInit{
  movies: MovieModel[] = [];
  title: string = '';
  movie: SearchResult | undefined = {}
  movieYears: number[] = [];
  movieYear: number = 0;

  constructor(private httpServices:HttpServicesService){

  }

  ngOnInit(): void {
    this.httpServices.getSearchResults().subscribe(x=>{
      this.movies = x as MovieModel[]
    }, (err)=>{
      
    });

    let thisYear = new Date().getFullYear();
    for(let count = thisYear; count >= 1900; count --){
      this.movieYears.push(count);
    }
  }

  search(){
    let query:Record<string, string> = {
      Plot: (document.getElementById('shortRadio') as HTMLInputElement).checked ? 'short' : 'full',
      Year: this.movieYear.toString()
    }

    this.httpServices.movieSearch(this.title, query).subscribe(x=>{
      this.movie = x;
    },(err)=>{

    }, ()=>{
      this.httpServices.getSearchResults().subscribe(x=>{
        this.movies = x as MovieModel[]
      }, (err)=>{
        
      });
    })
  }

  selectMovie(imdbID: string | undefined){
    this.movie = this.movies.find(x=> x.searchResult.imdbID == imdbID)?.searchResult;
  }
}
