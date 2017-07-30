import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ShortenComponent }     from './shorten.component';
import { UrlsListComponent }    from './urls-list.component';

const routes: Routes = [
  { path: '', redirectTo: '/shorten', pathMatch: 'full' },
  { path: 'shorten',  component: ShortenComponent },
  { path: 'urls-list',     component: UrlsListComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
