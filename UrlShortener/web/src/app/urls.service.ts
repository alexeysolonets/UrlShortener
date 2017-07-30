import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';

import { UrlInfo } from './UrlInfo';

@Injectable()
export class UrlsService {
    private headers = new Headers({ 'Content-Type': 'application/json' });
    private apiUrl = 'api/urls';  // URL to web api

    constructor(private http: Http) { }

    getUrls(): Promise<UrlInfo[]> {
        return this.http.get(this.apiUrl)
            .toPromise()
            .then(response => response.json() as UrlInfo[])
            .catch(this.handleError);
    }

    shorten(url: UrlInfo): Promise<void> {
        return this.http.post(this.apiUrl, url, { headers: this.headers })
            .toPromise()
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occured', error);
        return Promise.reject(error.message || error);
    }
}
