import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TableModule } from 'primeng/table'; 
import { ConfirmDialogModule } from 'primeng/confirmdialog'; 
import { CalendarModule } from 'primeng/calendar';
import { ConfirmationService } from 'primeng/api';
import { HistoryReservationComponent } from './history-reservation.component';
import { HistoryReservationService } from '../../shared/services/historyReservation/history-reservation.service';

describe('HistoryReservationComponent', () => {
  let component: HistoryReservationComponent;
  let fixture: ComponentFixture<HistoryReservationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HistoryReservationComponent],
      imports: [
        FormsModule,
        HttpClientTestingModule,
        TableModule, 
        ConfirmDialogModule,
        CalendarModule, 
      ],
      providers: [
        HistoryReservationService,
        ConfirmationService,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(HistoryReservationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
