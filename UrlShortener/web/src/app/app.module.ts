import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule }    from '@angular/http';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { ShortenComponent } from './shorten.component';
import { UrlsListComponent } from './urls-list.component';
import { UrlsService } from './urls.service';

@NgModule({
  declarations: [
    AppComponent,
    ShortenComponent,
    UrlsListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpModule
  ],
  providers: [UrlsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
