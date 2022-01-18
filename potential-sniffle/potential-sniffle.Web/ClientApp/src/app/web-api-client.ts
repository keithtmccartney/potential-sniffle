import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

export interface ISearchClient {
  get(scrapeURL: string | undefined, searchURL: string | undefined, searchKeyword: string | undefined, searchLimit: string | undefined): Observable<Search[]>;
}

@Injectable({
  providedIn: 'root'
})
export class SearchClient implements ISearchClient {
  private http: HttpClient;
  private baseUrl: string;

  protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

  constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
    this.http = http;
    this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
  }

  get(scrapeURL: string | undefined, searchURL: string | undefined, searchKeyword: string | undefined, searchLimit: string | undefined): Observable<Search[]> {
    let url_ = this.baseUrl + "/api/Search?";

    if (scrapeURL === null) {
      throw new Error("The parameter 'scrapeURL' cannot be null.");
    } else if (scrapeURL !== undefined) {
      url_ += "scrapeURL=" + encodeURIComponent("" + scrapeURL) + "&";
    }

    if (searchURL === null) {
      throw new Error("The parameter 'searchURL' cannot be null.");
    } else if (searchURL !== undefined) {
      url_ += "searchURL=" + encodeURIComponent("" + searchURL) + "&";
    }

    if (searchKeyword === null) {
      throw new Error("The parameter 'searchKeyword' cannot be null.");
    } else if (searchKeyword !== undefined) {
      url_ += "searchKeyword=" + encodeURIComponent("" + searchKeyword) + "&";
    }

    if (searchLimit === null) {
      throw new Error("The parameter 'searchLimit' cannot be null.");
    } else if (searchLimit !== undefined) {
      url_ += "searchLimit=" + encodeURIComponent("" + searchLimit) + "&";
    }

    url_ = url_.replace(/[?&]$/, "");

    let options_ : any = {
      observe: "response",
      responseType: "blob",
      headers: new HttpHeaders({
        "Accept": "application/json"
      })
    };

    return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
      return this.processGet(response_);
    })).pipe(_observableCatch((response_: any) => {
      if (response_ instanceof HttpResponseBase) {
        try {
          return this.processGet(<any>response_);
        } catch (e) {
          return <Observable<Search[]>><any>_observableThrow(e);
        }
      } else return <Observable<Search[]>><any>_observableThrow(response_);
    }));
  }

  protected processGet(response: HttpResponseBase): Observable<Search[]> {
    const status = response.status;
    const responseBlob = response instanceof HttpResponse ? response.body : (<any>response).error instanceof Blob ? (<any>response).error : undefined;

    let _headers: any = {};

    if (response.headers) {
      for (let key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }

    if (status === 200) {
      return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
        let result200: any = null;
        let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);

        if (Array.isArray(resultData200)) {
          result200 = [] as any;

          for (let item of resultData200) {
            result200!.push(Search.fromJS(item));
          }
        }

        return _observableOf(result200);
      }));
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
      }));
    }

    /*return _observableOf<Search[]>(<any>null);*/ //Type 'Observable<Search>' is not assignable to type 'Observable<Search[]>'. Type 'Search' is missing the following properties from type 'Search[]': length, pop, push, concat, and 28 more.ts(2322)
    return <Observable<Search[]>><any>_observableThrow(response);
  }
}

export class Search implements ISearch {
  scrapeURL?: string | undefined;
  searchURL?: string | undefined;
  searchKeyword?: string | undefined;
  searchLimit?: string | undefined;
  citeMatch?: string | undefined;
  citeMatchPosition?: number;

  constructor(data?: ISearch) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property))
        (<any>this)[property] = (<any>data)[property];
      }
    }
  }

  init(_data?: any) {
    if (_data) {
      this.citeMatch = _data["citeMatch"];
      this.citeMatchPosition = _data["citeMatchPosition"];
    }
  }

  static fromJS(data: any): Search {
    data = typeof data === 'object' ? data : {};

    let result = new Search();

    result.init(data);

    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    data["citeMatch"] = this.citeMatch;
    data["citeMatchPosition"] = this.citeMatchPosition;

    return data;
  }
}

export interface ISearch {
  citeMatch?: string | undefined;
  citeMatchPosition?: number;
}

export class SwaggerException extends Error {
  override message: string;
  status: number;
  response: string;
  headers: { [key: string]: any; };
  result: any;

  constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
    super();

    this.message = message;
    this.status = status;
    this.response = response;
    this.headers = headers;
    this.result = result;
  }

  protected isSwaggerException = true;

  static isSwaggerException(obj: any): obj is SwaggerException {
    return obj.isSwaggerException === true;
  }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
  if (result !== null && result !== undefined) {
    return _observableThrow(result);
  } else {
    return _observableThrow(new SwaggerException(message, status, response, headers, null));
  }
}

function blobToText(blob: any): Observable<string> {
  return new Observable<string>((observer: any) => {
    if (!blob) {
      observer.next("");
      observer.complete();
    } else {
      let reader = new FileReader();

      reader.onload = event => {
        observer.next((<any>event.target).result);
        observer.complete();
      };

      reader.readAsText(blob);
    }
  });
}
