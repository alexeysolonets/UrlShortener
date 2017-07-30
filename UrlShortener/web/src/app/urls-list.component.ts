import { Component, OnInit } from '@angular/core';
import { UrlInfo } from './UrlInfo';
import { UrlsService } from './urls.service';

@Component({
    selector: 'urls-list',
    templateUrl: './urls-list.component.html',
    styles: [`
        .column-created {
            color: silver; font-size: small;
        }
    `]
})
export class UrlsListComponent implements OnInit {
    /**
     * Urls list
     */
    urls: UrlInfo[] = [];

    /**
     * Is loading flag
     */
    isLoading: boolean = false;

    constructor(
        private urlsService: UrlsService
    ) { }

    ngOnInit() {
        this.isLoading = true;
        
        setTimeout(() => {
            this.urlsService
                .getUrls()
                .then(urls => 
                    {
                        this.urls = urls;
                        this.isLoading = false;
                    })
                    .catch(() => {
                        this.isLoading = false;
                    });
        }, 1000);

    }
}
