import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormReservationComponent } from './form-reservation.component';
import { ReactiveFormsModule } from '@angular/forms';
import { DynamicDialogModule, DynamicDialogRef, DynamicDialogConfig } from 'primeng/dynamicdialog';
import { ReservationService } from '../../../../shared/services/reservation/reservation.service';
import { CustomerService } from '../../../../shared/services/customer/customer.service';
import { ServicesService } from '../../../../shared/services/Service/services.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ButtonModule } from 'primeng/button'; 
import { DropdownModule } from 'primeng/dropdown'; 
import { CalendarModule } from 'primeng/calendar';
import { of } from 'rxjs';

describe('FormReservationComponent', () => {
  let component: FormReservationComponent;
  let fixture: ComponentFixture<FormReservationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FormReservationComponent],
      imports: [
        ReactiveFormsModule,
        DynamicDialogModule,
        HttpClientTestingModule,
        ButtonModule,  
        DropdownModule,
        CalendarModule
      ],
      providers: [
        ReservationService,
        CustomerService,
        ServicesService,
        { provide: DynamicDialogRef, useValue: {} },
        { provide: DynamicDialogConfig, useValue: { data: { action: 'create' } } }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FormReservationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
