import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateFormRegistrationComponent } from './create-form-registration.component';

describe('CreateFormRegistrationComponent', () => {
  let component: CreateFormRegistrationComponent;
  let fixture: ComponentFixture<CreateFormRegistrationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateFormRegistrationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateFormRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
