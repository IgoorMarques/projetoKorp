import { TestBed } from '@angular/core/testing';

import { AnuncioService } from './adocao.service';

describe('AdocaoService', () => {
  let service: AnuncioService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AnuncioService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
