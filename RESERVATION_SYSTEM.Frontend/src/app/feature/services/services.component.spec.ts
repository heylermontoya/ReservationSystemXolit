import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ServicesComponent } from './services.component';
import { ServicesService } from '../../shared/services/Service/services.service';
import { DialogService, DynamicDialogModule } from 'primeng/dynamicdialog';
import { ConfirmationService } from 'primeng/api';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ButtonModule } from 'primeng/button'; 
import { DropdownModule } from 'primeng/dropdown'; 
import { CalendarModule } from 'primeng/calendar';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { ConfirmDialogModule } from 'primeng/confirmdialog';

describe('ServicesComponent', () => {
  let component: ServicesComponent;
  let fixture: ComponentFixture<ServicesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ServicesComponent],
      imports: [
        HttpClientTestingModule,
        ButtonModule, 
        DropdownModule, 
        CalendarModule,
        FormsModule,
        ReactiveFormsModule,
        DynamicDialogModule,
        TableModule,
        ConfirmDialogModule
      ],
      providers: [
        ServicesService, 
        DialogService,
        ConfirmationService
      ]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ServicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
