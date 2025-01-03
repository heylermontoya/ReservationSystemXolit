import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { HttpService } from '../http-service/http.service';

import { HistoryReservationService } from './history-reservation.service';

describe('HistoryReservationService', () => {
  let service: HistoryReservationService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [HttpService],
    });
    service = TestBed.inject(HistoryReservationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
