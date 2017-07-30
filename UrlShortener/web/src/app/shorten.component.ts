import { Component } from '@angular/core';
import { UrlInfo } from './UrlInfo';
import { Router } from '@angular/router';
import { UrlsService } from './urls.service';

@Component({
    selector: 'shorten',
    templateUrl: './shorten.component.html',
    styles: [`
        #shorten-area {
            margin-bottom: 8px;
        }
    `]
})
export class ShortenComponent {
    /**
     * The original URL
     */
    original: string = '';

    constructor(
        private router: Router,
        private urlsService: UrlsService
    ) {

    }

    add(original) {
        original = original.trim();
        if (!original) {
            return;
        }

        let url = new UrlInfo();
        url.original = original;

        this.urlsService.shorten(url).then(() => {
            this.original = '';
            this.router.navigate(['/urls-list']);        
        });
    }
}
