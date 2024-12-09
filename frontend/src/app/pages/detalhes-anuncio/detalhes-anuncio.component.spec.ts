import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalhesAnuncioComponent } from './detalhes-anuncio.component';

describe('DetalhesAnuncioComponent', () => {
  let component: DetalhesAnuncioComponent;
  let fixture: ComponentFixture<DetalhesAnuncioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DetalhesAnuncioComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetalhesAnuncioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
