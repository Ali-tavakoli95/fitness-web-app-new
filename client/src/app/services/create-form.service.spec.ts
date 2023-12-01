import { TestBed } from '@angular/core/testing';

import { CreateFormService } from './create-form.service';

describe('CreateFormService', () => {
  let service: CreateFormService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CreateFormService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
