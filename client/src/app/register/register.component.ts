import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {  
  @Output() cancelRegister = new EventEmitter();
  registerForm: FormGroup = new FormGroup({});

  constructor (private accountService: AccountService, private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = this.formBuilder.group({
      userName: ['', [Validators.required, Validators.pattern('^[A-Za-z][A-Za-z0-9_]{5,11}$')]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]],
      displayName: ['', Validators.required]
    });
    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    });
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : {notMatching: true}
    }
  }

  register() {
     this.accountService.register(this.registerForm.value).subscribe({
      next: () => this.cancel()
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
