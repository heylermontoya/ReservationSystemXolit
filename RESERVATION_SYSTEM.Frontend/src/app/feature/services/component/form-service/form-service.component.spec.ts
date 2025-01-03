import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormServiceComponent } from './form-service.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { DynamicDialogModule, DynamicDialogRef, DynamicDialogConfig } from 'primeng/dynamicdialog';
import { ServicesService } from '../../../../shared/services/Service/services.service';
import { of } from 'rxjs';
import { ButtonModule } from 'primeng/button';
import { ConfirmDialogModule } from 'primeng/confirmdialog';

describe('FormServiceComponent', () => {
  let component: FormServiceComponent;
  let fixture: ComponentFixture<FormServiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FormServiceComponent],
      imports: [
        ReactiveFormsModule,
        FormsModule,
        DynamicDialogModule,
        ButtonModule,
        ConfirmDialogModule,
      ],
      providers: [
        { provide: DynamicDialogRef, useValue: {} },
        { provide: DynamicDialogConfig, useValue: { data: { action: 'create', name: '' } } },
        { provide: ServicesService, useValue: { createService: () => of({}) } },
      ]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FormServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
