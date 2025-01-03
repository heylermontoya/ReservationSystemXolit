import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormUserComponent } from './form-user.component';
import { ReactiveFormsModule } from '@angular/forms';
import { DynamicDialogRef, DynamicDialogConfig } from 'primeng/dynamicdialog';
import { CustomerService } from '../../../../shared/services/customer/customer.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ButtonModule } from 'primeng/button'; 
import { of } from 'rxjs';

class MockCustomerService {
  createUser() {
    return of({});
  }
  updateUser() {
    return of({});
  }
}

describe('FormUserComponent', () => {
  let component: FormUserComponent;
  let fixture: ComponentFixture<FormUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FormUserComponent],
      imports: [
        ReactiveFormsModule,
        HttpClientTestingModule,
        ButtonModule
      ],
      providers: [
        { provide: CustomerService, useClass: MockCustomerService },
        { provide: DynamicDialogRef, useValue: {} },
        { provide: DynamicDialogConfig, useValue: { data: {} } }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FormUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
