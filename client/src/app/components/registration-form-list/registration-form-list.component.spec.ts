import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrationFormListComponent } from './registration-form-list.component';

describe('RegistrationFormListComponent', () => {
  let component: RegistrationFormListComponent;
  let fixture: ComponentFixture<RegistrationFormListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegistrationFormListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RegistrationFormListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
