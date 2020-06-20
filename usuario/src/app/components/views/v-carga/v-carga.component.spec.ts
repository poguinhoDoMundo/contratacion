import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VCargaComponent } from './v-carga.component';

describe('VCargaComponent', () => {
  let component: VCargaComponent;
  let fixture: ComponentFixture<VCargaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VCargaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VCargaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
