import { UrlShortPage } from './app.po';

describe('url-short App', () => {
  let page: UrlShortPage;

  beforeEach(() => {
    page = new UrlShortPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
