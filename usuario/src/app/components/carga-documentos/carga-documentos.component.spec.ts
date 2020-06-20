import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CargaDocumentosComponent } from './carga-documentos.component';

describe('CargaDocumentosComponent', () => {
  let component: CargaDocumentosComponent;
  let fixture: ComponentFixture<CargaDocumentosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CargaDocumentosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CargaDocumentosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
