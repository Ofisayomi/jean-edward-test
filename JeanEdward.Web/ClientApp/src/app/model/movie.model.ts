export interface MovieModel {
  title: string
  searchResult: SearchResult
}

export interface SearchResult {
  title?: string
  year?: string
  rated?: string
  released?: string
  runtime?: string
  genre?: string
  director?: string
  writer?: string
  actors?: string
  plot?: string
  language?: string
  country?: string
  awards?: string
  poster?: string
  ratings?: Rating[]
  metascore?: string
  imdbRating?: string
  imdbVotes?: string
  imdbID?: string
  type?: string
  dvd?: string
  boxOffice?: string
  production?: string
  website?: string
  response?: boolean
}

export interface Rating {
  source: string
  value: string
}

export interface MovieQuery {
  year: number;
  plot: string;
}
