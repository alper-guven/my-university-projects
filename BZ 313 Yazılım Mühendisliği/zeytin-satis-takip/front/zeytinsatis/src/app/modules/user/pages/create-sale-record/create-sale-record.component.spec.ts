import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateSaleRecordComponent } from './create-sale-record.component';

describe('CreateSaleRecordComponent', () => {
  let component: CreateSaleRecordComponent;
  let fixture: ComponentFixture<CreateSaleRecordComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateSaleRecordComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateSaleRecordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
