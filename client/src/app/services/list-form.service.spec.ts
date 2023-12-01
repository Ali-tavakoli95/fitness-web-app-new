import { TestBed } from '@angular/core/testing';

import { ListFormService } from './list-form.service';

describe('ListFormService', () => {
  let service: ListFormService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ListFormService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
